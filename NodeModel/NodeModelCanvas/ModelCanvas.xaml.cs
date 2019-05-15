using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using NodeModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace NodeModelCanvas
{
    public sealed partial class ModelCanvas : UserControl
    {
        public ModelCanvas()
        {
            this.InitializeComponent();
        }
        private CoreDispatcher _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

        private INodeModel _model;
        private ISelector _selector;

        private float _zoomFactor = 1; //scale the view extent so that it fits on the canvas
        private Vector2 _offset = new Vector2(); //complete offset need to exactly center the view extent on the canvas

        public void Initialize(INodeModel model, ISelector selector)
        {
            DataContext = _model = model;
            _selector = selector;
        }

        #region StrokeStyle  ==================================================
        private CanvasStrokeStyle StrokeStyle(bool isDotted)
        {
            var ss = _strokeStyle;
            ss.DashStyle = isDotted ? CanvasDashStyle.Dot : CanvasDashStyle.Solid;
            ss.StartCap = CanvasCapStyle.Flat;
            ss.EndCap = CanvasCapStyle.Flat;
            ss.DashCap = CanvasCapStyle.Round;
            ss.LineJoin = CanvasLineJoin.Round;
            return ss;
        }
        private CanvasStrokeStyle _strokeStyle = new CanvasStrokeStyle();
        #endregion

        #region EditorCanvas_Draw  ============================================
        private void EditorCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            var data = _selector;
            if (data is null) return;

            var ds = args.DrawingSession;

            foreach ((Rect rect, byte w, (byte A, byte R, byte G, byte B) c) in data.DrawRects)
            {
                ds.FillRoundedRectangle(rect, 5, 5, Color.FromArgb(c.A, c.R, c.G, c.B));
            }

            foreach ((Vector2[] points, bool isDotted, byte w, (byte A, byte R, byte G, byte B) c) in data.DrawSplines)
            {
                using (var pb = new CanvasPathBuilder(ds))
                {
                    pb.BeginFigure(points[0]);
                    var N = points.Length;
                    for (var i = 0; i < N - 2;)
                    {
                        pb.AddCubicBezier(points[i], points[++i], points[++i]);
                    }
                    pb.EndFigure(CanvasFigureLoop.Open);

                    using (var geo = CanvasGeometry.CreatePath(pb))
                    {
                        ds.DrawGeometry(geo, Color.FromArgb(c.A, c.R, c.G, c.B), w, StrokeStyle(isDotted));
                    }
                }
            }

            foreach ((Vector2 topLeft, string text, (byte A, byte R, byte G, byte B) c) in data.DrawText)
            {
                ds.DrawText(text, topLeft, Color.FromArgb(c.A, c.R, c.G, c.B));
            }
        }
        #endregion

        #region Canavas_Loaded  ===============================================
        private void EditorCanvas_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            EditorCanvas.Loaded -= EditorCanvas_Loaded;
            EditorCanvas.Invalidate();
            if (_isRootCanvasLoaded) SetViewIdle();
        }
        bool _isEditorCanvasLoaded;

        private void RootCanvas_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _isRootCanvasLoaded = true;
            RootCanvas.Loaded -= RootCanvas_Loaded;
            SetViewIdle();
            if (_isEditorCanvasLoaded) EditorCanvas.Invalidate();
        }
        bool _isRootCanvasLoaded;
        #endregion

        #region RootCanvas_PointerMoved  ======================================
        private void RootCanvas_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (_isRootCanvasLoaded)
            {
                SetGridPoint2(e);
                SetDrawPoint2(e);

                e.Handled = true;

                if (_pointerIsPressed)
                    Post(EventType.Drag);
                else
                    Post(EventType.Skim);
            }
        }
        private bool _pointerIsPressed;
        #endregion

        #region RootCanvas_PointerPressed  ====================================
        private void RootCanvas_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (_isRootCanvasLoaded)
            {
                _pointerIsPressed = true;
                SetGridPoint1(e);
                SetDrawPoint1(e);
                e.Handled = true;

                Post(EventType.Tap);
            }
        }
        #endregion

        #region RootCanvas_PointerReleased  ===================================
        private void RootCanvas_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (_isRootCanvasLoaded)
            {
                _pointerIsPressed = false;
                SetGridPoint2(e);
                SetDrawPoint2(e);
                e.Handled = true;

                Post(EventType.End);
            }
        }
        #endregion

        #region HelperMethods  ================================================
        private void SetGridPoint1(PointerRoutedEventArgs e) => _selector.GridPoint1 = GridPoint(e);
        private void SetGridPoint2(PointerRoutedEventArgs e) => _selector.GridPoint2 = GridPoint(e);
        private void SetDrawPoint1(PointerRoutedEventArgs e) => _selector.DrawPoint1 = DrawPoint(e);
        private void SetDrawPoint2(PointerRoutedEventArgs e) => _selector.DrawPoint2 = DrawPoint(e);
        private Vector2 GridPoint(PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint(RootGrid).Position;
            return new Vector2((float)p.X, (float)p.Y);
        }
        private Vector2 DrawPoint(PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint(EditorCanvas).Position;
            var x = (p.X - _offset.X) / _zoomFactor;
            var y = (p.Y - _offset.Y) / _zoomFactor;
            return new Vector2((float)x, (float)y);
        }
        #endregion


        #region Event/Mode/State/Action  ======================================
        enum EventType { Idle, Tap, End, Skim, Drag };
        private enum StateType
        {   Unknown,

            ViewIdle,
            ViewOnVoidTap, ViewOnVoidDrag,   //trace a new region

            ViewOnPinSkim, ViewOnNodeSkim, ViewOnRegionSkim,  //show tooltips
            ViewOnPinTap, ViewOnNodeTap, ViewOnRegionTap,     //show property sheet

            SizeIdle,
            SizeOnTopSkim, SizeOnLeftSkim, SizeOnRightSkim, SizeOnBottomSkim,
            SizeOnTopTap, SizeOnLeftTap, SizeOnRightTap, SizeOnBottomTap,
            SizeOnTopDrag, SizeOnLeftDrag, SizeOnRightDrag, SizeOnBottomDrag,

            MoveIdle,
            MoveOnNodeSkim, MoveOnRegionSkim,
            MovenNodeTap, MoveOnRegionTap,
            MovenNodeDrag, MoveOnRegionDrag,

            CopyIdle,
            CopyOnNodeSkim, CopyOnRegionSkim,
            CopyOnNodeTap, CopyOnRegionTap,
            CopyOnNodeDrag, CopyOnRegionDrag,

            LinkIdle,
            LinkOnPinSkim, LinkOnNodeSkim,
            LinkOnPinTap, LinkOnNodeTap,
            LinkOnPinDrag, LinkOnNodeDrag,

            CreateIdle, CreateTap, CreateOnNode,
        };

        StateType _state = StateType.Unknown;
        Dictionary<EventType, Action> Event_Action = new Dictionary<EventType, Action>();

        void Post(EventType evt) { if (Event_Action.TryGetValue(evt, out Action action)) action(); }

        bool SetState(StateType state)
        {
            if (_state == state) return false;
            _state = state;
            Debug.WriteLine($"State: {_state}");

            Event_Action.Clear();
            return true;
        }
        void SetEventAction(EventType evt, Action act)
        {
            Event_Action[evt] = act;
        }


        #endregion


        #region View_Mode  ====================================================

        #region SetViewIdel  ==================================================
        void SetViewIdle()
        {
            if (_isRootCanvasLoaded)
            {
                if (SetState(StateType.ViewIdle))
                {
                    HideRegionGrid();
                    SetEventAction(EventType.Tap, ViewIdle_TapHitTest);
                    SetEventAction(EventType.Skim, ViewIdle_SkimHitTest);
                }
            }
        }
        async void ViewIdle_SkimHitTest()
        {
            var anyHit = false;
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { anyHit = _selector.HitTest(); });
            if (anyHit && _selector.IsHitNode)
                ShowTooltip();
            else
                HideTootlip();
        }
        async void ViewIdle_TapHitTest()
        {
            var anyHit = false;
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { anyHit = _selector.HitTest(); });
            if (anyHit)
            {
                if (_selector.IsHitNode)
                    _selector.ShowPropertyPanel();
            }
            else
            {
                HideSelectorGrid();
                HideTootlip();
                SetViewOnVoidTap();
                _selector.HidePropertyPanel();
            }
        }

        void SetViewOnVoidTap()
        {
            if (SetState(StateType.ViewOnVoidTap))
            {
                SetEventAction(EventType.End, SetViewIdle);
                SetEventAction(EventType.Drag, SetViewOnVoidDrag);
            }
        }
        void SetViewOnVoidDrag()
        {
            if (SetState(StateType.ViewOnVoidDrag))
            {
                ShowRegionGrid();
                SetEventAction(EventType.End, RegionTraceEnd);
                SetEventAction(EventType.Drag, TracingRegion);
            }
        }
        void RegionTraceEnd()
        {
            ShowSelectorGrid();
            SetViewIdle();
        }
        void TracingRegion()
        {
            UpdateRegionGrid();
        }

        #endregion

        #region SetViewOnNodeSkim  ============================================
        void SetView_OnNode_Skim()
        {
            if (SetState(StateType.ViewOnNodeSkim))
            {
                SetEventAction(EventType.Skim, View_OnNode_SkimHitTest);
            }
        }
        async void View_OnNode_SkimHitTest()
        {
            var anyHit = false;
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { anyHit = _selector.HitVerify(); });
            if (anyHit && _selector.IsHitNode)
            {

            }
        }
        #endregion

        #endregion

        #region Mode_Move  ====================================================
        void SetMoveIdle()
        {
            if (SetState(StateType.MoveIdle))
            {
            }
        }
        #endregion

        #region Mode_Create  ==================================================
        void SetCreateIdle()
        {
            if (SetState(StateType.CreateTap))
            {
            }
        }
        #endregion

        #region Mode_Link  ====================================================

        #endregion


        #region RadioButton_Events  ===========================================
        private void ViewSelect_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) => SetViewIdle();
        private void MoveSelect_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) => SetMoveIdle();
        private void LinkSelect_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { }
        private void CopySelect_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { }
        private void CreateSelect_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) => SetCreateIdle();
        private void OperateSelect_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { }
        #endregion

        #region ModelEditCanvas_Unloaded  =====================================
        private void ModelCanvas_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Unloaded -= ModelCanvas_Unloaded;

            if (EditorCanvas != null)
            {
                EditorCanvas.RemoveFromVisualTree();
                EditorCanvas = null;
            }
        }
        #endregion

        #region SelectorGrid  =================================================
        #endregion
    }
}
