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
    public class A_Selector : A_Model, ISelector
    {
        internal A_NodeModel Model => _model;
        private readonly A_NodeModel _model;
        internal bool IsMetadata => _isMetadata;
        private readonly bool _isMetadata; // its one or the other metadata or modeling data

        internal A_Selector(A_NodeModel model, Item itemRef, bool isMetadata) : base(itemRef)
        {
            _model = model;
            _isMetadata = isMetadata;
        }

        #region HitTest  ======================================================
        internal HitType Hit;
        
        public Vector2 DrawPoint1 { get; set; }
        public Vector2 DrawPoint2 { get; set; }
        public Rect RegionRect { get; private set; }


        public bool IsAnyHit => Hit != 0;
        public bool IsHitPin => (Hit & HitType.Pin) != 0;
        public bool IsHitNode => (Hit & HitType.Node) != 0;
        public bool IsHitRegion => (Hit & HitType.Region) != 0;

        public bool HitTest()
        {
            if (ItemRef is null) return false;

            if (_isMetadata)
                return ChefRef.MetadataHitTest(this);
            else
                return ChefRef.MetadataHitTest(this);
        }
        #endregion


        #region NodePanel  ====================================================
        internal void SetRowX(RowX rowX)
        {
            _rowX = rowX;
            NodePanel_Visible = (rowX is null) ? Visibility.Collapsed : Visibility.Visible;
        }
        private RowX _rowX;

        public Visibility NodePanel_Visible
        {
            get => _nodePanelVisible;
            set { Set(ref _nodePanelVisible, value); }
        }
        private Visibility _nodePanelVisible = Visibility.Collapsed;


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
            NodeTypePanel_Visible = (tableX is null) ? Visibility.Collapsed : Visibility.Visible;
        }
        private TableX _tableX;

        public Visibility NodeTypePanel_Visible
        {
            get => _nodeTypePanelVisible;
            set { Set(ref _nodeTypePanelVisible, value); }
        }
        private Visibility _nodeTypePanelVisible = Visibility.Collapsed;


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
        #endregion


        #region CanvasData  ===================================================
        public IList<(Rect Rect, byte Width, (byte A, byte R, byte G, byte B) Color)> DrawRects => _drawRects;
        private IList<(Rect Rect, byte Width, (byte A, byte R, byte G, byte B) Color)> _drawRects = new List<(Rect Rect, byte Width, (byte A, byte R, byte G, byte B) Color)>();

        public IList<(Vector2[] Points, bool IsDotted, byte Width, (byte A, byte R, byte G, byte B) Color)> DrawSplines => _drawSplines;
        private IList<(Vector2[] Points, bool IsDotted, byte Width, (byte A, byte R, byte G, byte B) Color)> _drawSplines = new List<(Vector2[] Points, bool IsDotted, byte Width, (byte A, byte R, byte G, byte B) Color)>();

        public IList<(Vector2 TopLeft, string Text, (byte A, byte R, byte G, byte B) Color)> DrawText => _drawText;
        private IList<(Vector2 TopLeft, string Text, (byte A, byte R, byte G, byte B) Color)> _drawText = new List<(Vector2 TopLeft, string Text, (byte A, byte R, byte G, byte B) Color)>();
        #endregion
    }
}
