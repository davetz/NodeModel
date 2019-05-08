using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NodeModel
{
    public partial class Chef
    {
        internal IRepository Repository;

        internal void SaveToRepository()
        {
            if (Repository != null)
            {
                Repository.Write(this);
            }
        }
        internal void SaveToRepository(IRepository repository)
        {
            Repository = repository;
            SaveToRepository();
        }

        internal void AfterReadValidation()
        {
        }

        internal void CheckRepository(IRepository repository)
        {
            Repository = repository;

            if (repository == null)
                _newChefNumber = (_newChefCount += 1);
            else
                Repository.Read(this);
        }

        internal string GetFullRepositoryName() => (Repository == null) ? NullStorageFileName : Repository.FullName;
        internal string GetRepositoryName() => (Repository == null) ? NullStorageFileName : Repository.Name;
        private string NullStorageFileName => $"New Model #{_newChefNumber}";
        //private string NullStorageFileName => $"{_localize(GetNameKey(Trait.NewModel))} #{_newChefNumber}";

        #region GetItemIndex  =============================================
        internal List<Relation> GetRelationList()
        {
            var list = new List<Relation>(RelationXStore.Count + RelationStore.Count);
            list.Add(TableX_ColumnX);
            list.Add(TableX_ChildRelationX);
            list.Add(TableX_ParentRelationX);
            foreach (var rx in RelationXStore.Items) { list.Add(rx); }
            return list;
        }
        /// <summary>
        /// Get Item-to-Index dictionary specify min/max external indexes
        /// </summary>
        /// <returns></returns>
        internal (int minIndex, int maxIndex, Dictionary<Item, int> itemIndex) GetItemIndex()
        {
            const int firstExternalIndex = 21;

            // count items that can be referenced by index
            //=============================================
            int count = firstExternalIndex;

            foreach (var item in TableXStore.Items)
            {
                count += (item as TableX).Count; // RowX count
            }

            count += TableXStore.Count;
            count += ColumnXStore.Count;
            count += RelationXStore.Count;
            count += RelationStore.Count;   //internal infrastructure

            // allocate memory
            //=============================================
            var itemIndex = new Dictionary<Item, int>(count);


            // internally defined items
            //=============================================
            int j = 0;
            itemIndex.Add(Dummy, j++);
            itemIndex.Add(TableX_ColumnX, j++);
            itemIndex.Add(TableX_ChildRelationX, j++);
            itemIndex.Add(TableX_ParentRelationX, j++);

            // externally defined items
            //=============================================
            j = firstExternalIndex;

            foreach (var itm in TableXStore.Items)
            {
                itemIndex.Add(itm, j++);
            }
            foreach (var itm in ColumnXStore.Items)
            {
                itemIndex.Add(itm, j++);
            }
            foreach (var itm in RelationXStore.Items)
            {
                itemIndex.Add(itm, j++);
            }

            // put grandchild items at the end
            //=============================================
            foreach (var parent in TableXStore.Items)
            {
                foreach (var itm in parent.Items)
                {
                    var child = itm;
                    itemIndex.Add(child, j++);
                }
            }

            var externalIndexMinLimit = firstExternalIndex - 1;

            var externalIndexMaxLimit = j;

            return (externalIndexMinLimit, externalIndexMaxLimit, itemIndex);
        }
        #endregion
    }
}
