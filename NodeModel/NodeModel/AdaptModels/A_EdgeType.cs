using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace NodeModel
{
    public class A_EdgeType : IEdgeType
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ToolTip { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Piaring { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<string> PairingTypes => throw new NotImplementedException();


        public INodeType ChildNodeType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public INodeType ParentNodeType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public int ChildNodePinOffset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ParentNodePinOffset { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IConnectorPinType InputPinType => throw new NotImplementedException();

        public IConnectorPinType OutputPinType => throw new NotImplementedException();

        #region INotifyPropertyChanged  =======================================
        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public bool CanCreateInputPin(Vector2 location)
        {
            throw new NotImplementedException();
        }

        public IEdgeType CreateInputPin(Vector2 location)
        {
            throw new NotImplementedException();
        }

        public bool CanCreateOutputPin(Vector2 location)
        {
            throw new NotImplementedException();
        }

        public bool CreateOutputPin(Vector2 location)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
