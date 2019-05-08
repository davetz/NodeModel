using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NodeModel
{
    public class A_Edge : IEdge
    {
        public Vector2 XYPin1
        {
            get { return _xyPin1; }
        }
        private Vector2 _xyPin1;

        public Vector2 XYPin2
        {
            get { return _xyPin2; }
            private set { Set(ref _xyPin2, value); }
        }
        private Vector2 _xyPin2;

        public INode RefNode
        {
            get { return _refNode; }
        }
        private INode _refNode;

        public INode OtherNode
        {
            get { return _otheNode; }
        }

        private INode _otheNode;

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
        #endregion
    }
}
