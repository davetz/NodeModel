using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace NodeModel
{
    public class NodeModelCreator
    {
        public static INodeModel Create() => A_NodeModel.Create();
    }

    public class A_NodeModel : A_Model, INodeModel
    {
        #region Create_NodeModel_X  ===========================================
        static public INodeModel Create() => new A_NodeModel();

        private A_NodeModel() : base(null) { } 
        #endregion

        #region NodeModelProperties  ==========================================
        public string ModelName
        {
            get { return ItemRef is null ? "<none>" : ChefRef.GetRepositoryName(); }
            set { Set(ref _modelName, value); }
        }
        private string _modelName;
        public string ModelFullName
        {
            get { return ItemRef is null ? "Try New or Open" : ChefRef.GetFullRepositoryName(); }
            set { Set(ref _modelFullName, value); }
        }
        private string _modelFullName;
        #endregion

        #region Refresh  ======================================================
        public void Refresh()
        {
            _modelName = string.Empty;
            _modelFullName = string.Empty;

            ModelName = "refresh";
            ModelFullName = "refresh";
        }
        #endregion

        #region CreateNodeType  ===============================================
        public INodeType CreateNodeType()
        {
            var nodeType = new A_NodeType(ChefRef.CreateTableX());
            AddNodeType(nodeType);
            return nodeType;
        }
        #endregion

        #region DeleteNodeType  ===============================================
        public bool DeleteNodeType(INodeType nodeType)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Load  =========================================================
        public bool Load(IRepository repository)
        {
            ItemRef = new Chef(repository);
            Refresh();
            return true;
        }
        #endregion

        #region Reload  =======================================================
        public bool Reload()
        {
            if (ItemRef is null) return false;
            if (ChefRef.Repository is null) return false;
            return false;
        }
        #endregion

        #region Save  =========================================================
        public bool Save()
        {
            if (ItemRef is null) return false;
            if (ChefRef.Repository is null) return false;
            ChefRef.SaveToRepository();
            return true;
        }
        #endregion

        #region SaveAs  =======================================================
        public bool SaveAs(IRepository repository)
        {
            if (ItemRef is null) return false;
            ChefRef.SaveToRepository(repository);
            return true;
        }
        #endregion

        #region AllNodes  =====================================================
        private ObservableCollection<INode> _allNodes = new ObservableCollection<INode>();
        private void AddNode(INode node)
        {
            _allNodes.Add(node);
        }
        private void RemoveNode(INode node)
        {
            _allNodes.Remove(node);
        }
        public ObservableCollection<INode> AllNodes => _allNodes;
        #endregion

        #region AllEdges  =====================================================
        private ObservableCollection<IEdge> _allEdges = new ObservableCollection<IEdge>();
        private void AddEdge(IEdge edge)
        {
            _allEdges.Add(edge);
        }
        private void RemoveEdge(IEdge edge)
        {
            _allEdges.Remove(edge);
        }
        public ObservableCollection<IEdge> AllEdges => _allEdges;
        #endregion

        #region AllNodeTypes  =================================================
        private ObservableCollection<INodeType> _allNodeTypes = new ObservableCollection<INodeType>();
        private void AddNodeType(INodeType nodeType)
        {
            _allNodeTypes.Add(nodeType);
        }
        private void RemoveNodeType(INodeType nodeType)
        {
            _allNodeTypes.Remove(nodeType);
        }

        public ObservableCollection<INodeType> AllNodeTypes => _allNodeTypes;
        #endregion
    }
}
