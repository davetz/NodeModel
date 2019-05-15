using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI.Xaml;

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
        }
        private string _modelName;
        public string ModelFullName
        {
            get { return ItemRef is null ? "Try New or Open" : ChefRef.GetFullRepositoryName(); }
        }
        private string _modelFullName;
        #endregion

        #region Refresh  ======================================================
        public void Refresh()
        {
            PropertyChange(nameof(ModelName));
            PropertyChange(nameof(ModelFullName));
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

        public ISelector GetMetadataSelector() => new A_MetadataSelector(this, ItemRef);
        public ISelector GetModelingSelector() => new A_MetadataSelector(this, ItemRef);
    }
}
