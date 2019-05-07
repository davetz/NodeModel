using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public partial class Chef
    {
        internal void RemoveItem(Item target)
        {
            var reItems = new Dictionary<Relation, Dictionary<Item, List<Item>>>();
            var hitList = new List<Item>();

            FindDependents(target);
            hitList.Reverse();

            foreach (var item in hitList)
            {
                if (item is Relation r)
                {
                    var N = r.GetLinks(out List<Item> parents, out List<Item> children);

                    for (int i = 0; i < N; i++) { TryMarkItemUnlinked(r, parents[i], children[i]); }
                }
                if (TryGetParentRelations(item, out IList<Relation> relations))
                {
                    foreach (var rel in relations)
                    {
                        if (!rel.TryGetParents(item, out List<Item> parents)) continue;

                        foreach (var parent in parents) { TryMarkItemUnlinked(rel, parent, item); }
                    }
                }
                if (TryGetChildRelations(item, out relations))
                {
                    foreach (var rel in relations)
                    {
                        if (!rel.TryGetChildren(item, out List<Item> children)) continue;

                        foreach (var child in children) { TryMarkItemUnlinked(rel, item, child); }
                    }
                }
            }
            foreach (var e1 in reItems)
            {
                var rel = e1.Key as Relation;
                foreach (var e2 in e1.Value)
                {
                    var itm1 = e2.Key;
                    foreach (var itm2 in e2.Value)
                    {
                        rel.RemoveLink(itm1, itm2);
                    }
                }
            }
            foreach (var itm in hitList)
            {
                var sto = itm.Store;
                sto.Remove(itm);

                if (itm is ColumnX cx)
                {
                    cx.Value.Clear();
                }
            }

            #region PrivateMethods  ===========================================

            void FindDependents(Item target2)
            {
                hitList.Add(target2);
                if (target2 is Store store)
                {
                    var items = store.GetItems();
                    foreach (var item in items) FindDependents(item);
                }
                if (TryGetChildRelations(target2, out IList<Relation> relations))
                {
                    foreach (var rel in relations)
                    {
                        if (rel.IsRequired && rel.TryGetChildren(target2, out List<Item> children))
                        {
                            foreach (var child in children)
                            {
                                FindDependents(child);
                            }
                        }
                    }
                }
            }

            bool TryGetChildRelations(Item item, out IList<Relation> relations)
            {
                if (item.Owner is TableX tx)
                {
                    if (TableX_ChildRelationX.TryGetChildren(tx, out IList<RelationX> txRelations))
                    {
                        relations = new List<Relation>(txRelations);
                        return true;
                    }
                    relations = null;
                    return false;
                }
                return Store_ChildRelation.TryGetChildren(item.Owner, out relations);
            }

            bool TryGetParentRelations(Item item, out IList<Relation> relations)
            {
                if (item.Owner is TableX tx)
                {
                    if (TableX_ParentRelationX.TryGetChildren(tx, out IList<RelationX> txRelations))
                    {
                        relations = new List<Relation>(txRelations);
                        return true;
                    }
                    relations = null;
                    return false;
                }
                return Store_ParentRelation.TryGetChildren(item.Owner, out relations);
            }

            bool TryMarkItemUnlinked(Relation rel, Item item1, Item item2)
            {
                List<Item> items;

                if (reItems.TryGetValue(rel, out Dictionary<Item, List<Item>> itemItems))
                {
                    if (itemItems.TryGetValue(item1, out items))
                    {
                        if (items.Contains(item2)) return false;
                        items.Add(item2);
                    }
                    else
                    {
                        items = new List<Item>(2) { item2 };
                        itemItems.Add(item1, items);
                    }
                }
                else
                {
                    itemItems = new Dictionary<Item, List<Item>>(4);
                    items = new List<Item>(2) { item2 };
                    itemItems.Add(item1, items);
                    reItems.Add(rel, itemItems);
                }
                return true;
            }
            #endregion
        }
    }
}
