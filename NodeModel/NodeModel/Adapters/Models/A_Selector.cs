using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace NodeModel
{
    public abstract class A_Selector : A_Model, ISelector
    {
        internal A_NodeModel Model => _model;
        private readonly A_NodeModel _model;
        private readonly bool _isMetadata; // its one or the other metadata or modeling data

        internal A_Selector(A_NodeModel model, Item itemRef) : base(itemRef)
        {
            _model = model;
        }

        abstract public bool CreateNode();

        abstract public bool HitTest();
        abstract public bool HitVerify();

        abstract public void ShowPropertyPanel();
        abstract public void HidePropertyPanel();


        #region HitTest  ======================================================
        internal HitType Hit;

        internal RowX HitRowX;
        internal TableX HitTableX;
        internal RelationX HitRelationX;
        internal bool HitEdgeFarEnd;

        public string ToolTip_Text1 { get; set; }
        public string ToolTip_Text2 { get; set; }

        public Vector2 GridPoint1 { get; set; }
        public Vector2 GridPoint2 { get; set; }
        public Vector2 DrawPoint1 { get; set; }
        public Vector2 DrawPoint2 { get; set; }
        public Rect RegionRect { get; private set; }


        public bool IsAnyHit => Hit != 0;
        public bool IsHitPin => (Hit & HitType.Pin) != 0;
        public bool IsHitNode => (Hit & HitType.Node) != 0;
        public bool IsHitRegion => (Hit & HitType.Region) != 0;
        #endregion

        #region NodePanel  ====================================================
        internal void SetRowX(RowX rowX)
        {
            _rowX = rowX;
            NodePanel_Visible = (rowX is null) ? Visibility.Collapsed : Visibility.Visible;
        }
        protected RowX _rowX;

        public Visibility NodePanel_Visible
        {
            get => _nodePanelVisible;
            set { Set(ref _nodePanelVisible, value); }
        }
        protected Visibility _nodePanelVisible = Visibility.Collapsed;


        public string Node_Name
        {
            get => _tableX?.Name ?? "<null>";
            set { if (_rowX != null) Set(ref _rowX.Name, value); }
        }
        public string Node_ToolTip
        {
            get => _tableX?.ToolTip ?? "<null>";
            set { if (_rowX != null) Set(ref _rowX.ToolTip, value); }
        }
        public string Node_Description
        {
            get => _rowX?.TableX?.Description ?? "<null>";
        }
        #endregion

        #region NodeTypePanel  ================================================
        internal void SetTableX(TableX tableX)
        {
            _tableX = tableX;
            _nodeTypePanel_Visible = (tableX is null) ? Visibility.Collapsed : Visibility.Visible;
            PropertyChange(nameof(NodeTypePanel_Visible));
            PropertyChange(nameof(NodeType_Name));
            PropertyChange(nameof(NodeType_ToolTip));
            PropertyChange(nameof(NodeType_Description));
            PropertyChange(nameof(NodeType_MinWidth));
            PropertyChange(nameof(NodeType_MinHeight));
        }

        private TableX _tableX;

        public Visibility NodeTypePanel_Visible
        {
            get => _nodeTypePanel_Visible;
            set { Set(ref _nodeTypePanel_Visible, value); }
        }
        private Visibility _nodeTypePanel_Visible = Visibility.Collapsed;


        public string NodeType_Name
        {
            get => _tableX?.Name ?? "<null>";
            set { if (_tableX != null) Set(ref _tableX.Name, value); }
        }
        public string NodeType_ToolTip
        {
            get => _tableX?.ToolTip ?? "<null>";
            set { if (_tableX != null) Set(ref _tableX.ToolTip, value); }
        }
        public string NodeType_Description
        {
            get => _tableX?.Description ?? "<null>";
            set { if (_tableX != null) Set(ref _tableX.Description, value); }
        }

        public int NodeType_MinWidth
        {
            get => (_tableX is null) ? 0 : _tableX.MinWidth;
            set { if (_tableX != null) { var val = _tableX.MinWidth; _tableX.MinWidth = value; Set(ref val, value); } }
        }

        public int NodeType_MinHeight
        {
            get => (_tableX is null) ? 0 : _tableX.MinHeight;
            set { if (_tableX != null) { var val = _tableX.MinHeight; _tableX.MinHeight = value; Set(ref val, value); } }
        }

        #endregion


        #region CanvasDraw  ===================================================
        public IList<(Rect Rect, byte Width, (byte A, byte R, byte G, byte B) Color)> DrawRects => _drawRects;
        protected IList<(Rect Rect, byte Width, (byte A, byte R, byte G, byte B) Color)> _drawRects = new List<(Rect Rect, byte Width, (byte A, byte R, byte G, byte B) Color)>();

        public IList<(Vector2[] Points, bool IsDotted, byte Width, (byte A, byte R, byte G, byte B) Color)> DrawSplines => _drawSplines;
        protected IList<(Vector2[] Points, bool IsDotted, byte Width, (byte A, byte R, byte G, byte B) Color)> _drawSplines = new List<(Vector2[] Points, bool IsDotted, byte Width, (byte A, byte R, byte G, byte B) Color)>();

        public IList<(Vector2 TopLeft, string Text, (byte A, byte R, byte G, byte B) Color)> DrawText => _drawText;
        protected IList<(Vector2 TopLeft, string Text, (byte A, byte R, byte G, byte B) Color)> _drawText = new List<(Vector2 TopLeft, string Text, (byte A, byte R, byte G, byte B) Color)>();
        #endregion
    }
}
