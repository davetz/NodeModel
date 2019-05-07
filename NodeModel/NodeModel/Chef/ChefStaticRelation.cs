using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public partial class Chef
    {
        internal RelationOf<Store, Property> Store_Property { get; private set; }
        internal RelationOf<Store, Relation> Store_ChildRelation { get; private set; }
        internal RelationOf<Store, Relation> Store_ParentRelation { get; private set; }

        internal RelationOf<TableX, ColumnX> TableX_ColumnX { get; private set; }
        internal RelationOf<TableX, Property> TableX_NameProperty { get; private set; } // can be a ColumnX or a ComputeX
        internal RelationOf<TableX, Property> TableX_SummaryProperty { get; private set; } // can be a ColumnX or a ComputeX
        internal RelationOf<TableX, RelationX> TableX_ChildRelationX { get; private set; }
        internal RelationOf<TableX, RelationX> TableX_ParentRelationX { get; private set; }

        #region InitializeRelations  ==========================================
        private void InitializeRelations()
        {
            Store_Property = new RelationOf<Store, Property>(RelationZStore, Trait.Store_Property, Pairing.OneToMany, 40, 100, true);
            Store_ChildRelation = new RelationOf<Store, Relation>(RelationZStore, Trait.Store_ChildRelation, Pairing.ManyToMany, 25, 25, true);
            Store_ParentRelation = new RelationOf<Store, Relation>(RelationZStore, Trait.Store_ParentRelation, Pairing.ManyToMany, 25, 25, true);

            TableX_ColumnX = new RelationOf<TableX, ColumnX>(RelationStore, Trait.TableX_ColumnX, Pairing.OneToMany, 25, 25, true);
            AddIntegrityCheck(TableX_ColumnX, TableXStore, ColumnXStore);

            TableX_NameProperty = new RelationOf<TableX, Property>(RelationStore, Trait.TableX_NameProperty, Pairing.OneToOne, 25, 25);
            AddIntegrityCheck(TableX_NameProperty, TableXStore, ColumnXStore);

            TableX_SummaryProperty = new RelationOf<TableX, Property>(RelationStore, Trait.TableX_SummaryProperty, Pairing.OneToOne, 25, 25);
            AddIntegrityCheck(TableX_SummaryProperty, TableXStore, ColumnXStore);

            TableX_ChildRelationX = new RelationOf<TableX, RelationX>(RelationStore, Trait.TableX_ChildRelationX, Pairing.OneToMany, 25, 25, true);
            AddIntegrityCheck(TableX_ChildRelationX, TableXStore, RelationXStore);

            TableX_ParentRelationX = new RelationOf<TableX, RelationX>(RelationStore, Trait.TableX_ParentRelationX, Pairing.OneToMany, 25, 25, true);
            AddIntegrityCheck(TableX_ParentRelationX, TableXStore, RelationXStore);

        }
        /// <summary>
        /// Build dependency dictionaries used for maintaining relational integrity
        /// </summary>
        private void AddIntegrityCheck(Relation relation, Store parentStore, Store childStore)
        {
            if (!Store_ChildRelation.ContainsLink(parentStore, relation))
                Store_ChildRelation.SetLink(parentStore, relation);

            if (!Store_ParentRelation.ContainsLink(childStore, relation))
                Store_ParentRelation.SetLink(childStore, relation);
        }
        #endregion
    }
}
