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
                ds.DrawRoundedRectangle(rect, 5, 5, Color.FromArgb(c.A, c.R, c.G, c.B), w);
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

        #region EditorCanavas_Loaded  =========================================
        private void EditorCanvas_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            EditorCanvas.Invalidate();
        }

        #endregion

        #region EditorCanvas_PointerMoved  ====================================
        private void EditorCanvas_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            SetDrawPoint2(e);
            e.Handled = true;

            if (_pointerIsPressed)
                Post(EventType.Moving);
            else
                Post(EventType.Hover);
        }
        private bool _pointerIsPressed;
        #endregion

        #region EditorCanvas_PointerPressed  ==================================
        private void EditorCanvas_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            _pointerIsPressed = true;
            SetDrawPoint1(e);
            e.Handled = true;

            Post(EventType.Begin);
        }
        #endregion

        #region EditorCanvas_PointerReleased  =================================
        private void EditorCanvas_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            _pointerIsPressed = false;
            SetDrawPoint2(e);
            e.Handled = true;

            Post(EventType.Begin);
        }
        #endregion

        #region HelperMethods  ================================================
        private void SetDrawPoint1(PointerRoutedEventArgs e) => _selector.DrawPoint1 = DrawPoint(e);
        private void SetDrawPoint2(PointerRoutedEventArgs e) => _selector.DrawPoint2 = DrawPoint(e);
        private Vector2 DrawPoint(PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint(EditorCanvas).Position;
            var x = (p.X - _offset.X) / _zoomFactor;
            var y = (p.Y - _offset.Y) / _zoomFactor;
            return new Vector2((float)x, (float)y);
        }
        #endregion


        #region Event/Mode/State/Action  ======================================
        enum EventType { Idle, Begin, End, Hover, Moving };
        enum StateType { None, OnVoid, OnPin, OnNode, OnRegion, BeginNodeMove, MoveNode, EndNodeMove, BeginRegionMove, MoveRegion, EndRegionMove, BeginLink, TraceLink, EndLink };

        StateType _state = StateType.None;
        Dictionary<EventType, Action> Event_Action = new Dictionary<EventType, Action>();

        void Post(EventType evt) { if (Event_Action.TryGetValue(evt, out Action action)) action(); }

        bool SetState(StateType state)
        {
            if (_state == state) return false;
            Debug.WriteLine($"State: {_state}");

            Event_Action.Clear();
            return true;
        }
        void SetEventAction(EventType evt, Action act)
        {
            Event_Action[evt] = act;
        }
        #endregion


        #region Mode_View  ====================================================
        void SetViewIdle()
        {
            if (SetState(StateType.OnVoid))
            {
                SetEventAction(EventType.Hover, ViewHitTest);
            }
        }
        async void ViewHitTest()
        {
            var anyHit = false;
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { anyHit = _selector.HitTest(); });
            if (anyHit)
                Debug.WriteLine("got hit ..");
            else
                Debug.WriteLine("no hit ..");
        }
        #endregion

        #region Mode_Move  ====================================================
        void SetMoveIdle()
        {
            if (SetState(StateType.OnVoid))
            {
                SetEventAction(EventType.Hover, MoveHitTest);
            }
        }
        void MoveHitTest()
        {
        }
        #endregion

        #region Mode_Create  ==================================================
        void SetCreateIdle()
        {
            if (SetState(StateType.OnVoid))
            {
                SetEventAction(EventType.Hover, CreateHitTest);
            }
        }
        void CreateHitTest()
        {
        }
        #endregion

        #region Mode_Link  ====================================================

        #endregion


        #region RadioButton_Events  ===========================================
        private void ViewSelect_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) => SetViewIdle();
        private void MoveSelect_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) => SetMoveIdle();
        private void LinkSelect_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) => SetCreateIdle();
        private void CopySelect_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { }
        private void CreateSelect_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { }
        private void OperateSelect_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e) { }
        #endregion

        #region ModelEditCanvas_Unloaded  =====================================
        private void ModelCanvas_Unloaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (EditorCanvas != null)
            {
                EditorCanvas.RemoveFromVisualTree();
                EditorCanvas = null;
            }
        }
        #endregion
    }
}
