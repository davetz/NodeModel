using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

using Windows.Foundation;

namespace NodeModel
{
    public class A_Node : A_Model, INode 
    {
        internal A_Node(RowX itemRef) : base(itemRef)
        {
        }
        RowX RowXRef => ItemRef as RowX;

        public Vector2 CenterPoint
        {
            get { return RowXRef.CenterPoint; }
            set { RowXRef.CenterPoint = value; }
        }
        public Rect BoundingRect
        {
            get { return RowXRef.BoundingRect; }
        }


        public ObservableCollection<IEdge> InputEdges => throw new NotImplementedException();

        public ObservableCollection<IEdge> OutputEdges => throw new NotImplementedException();


        public ObservableCollection<IEdgeType> AllInputEdgeTypes => throw new NotImplementedException();

        public ObservableCollection<IEdgeType> AllOutputEdgeTypes => throw new NotImplementedException();


        public ObservableCollection<IEdgeType> UnusedInputEdgeTypes => throw new NotImplementedException();

        public ObservableCollection<IEdgeType> UnusedOutputEdgeTypes => throw new NotImplementedException();

        public INodeType NodeType => throw new NotImplementedException();


        public bool RemoveEdge(IEdge edge)
        {
            throw new NotImplementedException();
        }
    }
}
