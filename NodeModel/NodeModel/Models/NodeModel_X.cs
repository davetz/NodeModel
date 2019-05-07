using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace NodeModel
{
    public class NodeModelCreator { public static INodeModel Create() => NodeModel_X.Create(); }

    public class NodeModel_X : Model_X, INodeModel
    {
        #region Create_NodeModel_X  ===========================================
        static public INodeModel Create() => new NodeModel_X();

        private NodeModel_X() : base(new Chef())
        {
        }
         #endregion

        #region CreateNodeType  ===============================================
        public INodeType CreateNodeType()
        {
            var nodeType = new NodeType_X(ChefRef.CreateTableX());
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
            throw new NotImplementedException();
        }
        #endregion

        #region Reload  =======================================================
        public bool Reload()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Save  =========================================================
        public bool Save()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region SaveAs  =======================================================
        public bool SaveAs(IRepository repository)
        {
            throw new NotImplementedException();
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
