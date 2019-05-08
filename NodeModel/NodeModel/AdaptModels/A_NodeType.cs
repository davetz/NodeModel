using System;
using System.Collections.ObjectModel;
using System.Numerics;

using Windows.Foundation;

namespace NodeModel
{
    public class A_NodeType : A_Model, INodeType
    {
        TableX TableXRef => ItemRef as TableX;

        internal A_NodeType(TableX itemRef) : base(itemRef) { }


        #region NodeTypeProperty  =============================================
        public string Name
        {
            get { return TableXRef.Name; }
            set { TableXRef.Name = value; }
        }
        public string ToolTip
        {
            get { return TableXRef.Summary; }
            set { TableXRef.Summary = value; }
        }
        public string Description
        {
            get { return TableXRef.Description; }
            set { TableXRef.Description = value; }
        }

        public Vector2 CenterPoint
        {
            get { return  TableXRef.CenterPoint; } 
            set { TableXRef.CenterPoint = value; } 
        }
        public Rect BoundingRect
        {
            get { return TableXRef.BoundingRect; }
            set { TableXRef.BoundingRect = value; }
        }
        #endregion


        #region CreateEdgeType  ===============================================
        public IEdgeType CreateEdgeType(INodeType destinationNodeType)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DeleteEdgeType  ===============================================
        public bool DeleteEdgeType(IEdgeType edgeType)
        {
            throw new NotImplementedException();
        }

        public IEdgeType CreatePropertyType(INodeType destinationNodeType)
        {
            throw new NotImplementedException();
        }

        public IEdgeType DeleteProperyType(IPropertyType properyType)
        {
            throw new NotImplementedException();
        }

        public IEdgeType CreateOutputEdgeType(INodeType to_NodeType)
        {
            throw new NotImplementedException();
        }

        public IEdgeType CreateInputEdgeType(INodeType to_NodeType)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region PropertyTypes  ================================================
        private ObservableCollection<IPropertyType> _propertyTypes = new ObservableCollection<IPropertyType>();
        public ObservableCollection<IPropertyType> PropertyTypes => _propertyTypes;
        #endregion

        #region InputEdgeTypes  ===============================================
        private ObservableCollection<IEdgeType> _inputEdgeTypes = new ObservableCollection<IEdgeType>();
        public ObservableCollection<IEdgeType> InputEdgeTypes => _inputEdgeTypes;
        #endregion

        #region OutputEdgeTypes  ==============================================
        private ObservableCollection<IEdgeType> _outputEdgeTypes = new ObservableCollection<IEdgeType>();
        public ObservableCollection<IEdgeType> OutputEdgeTypes => _outputEdgeTypes;
        #endregion
    }
}
