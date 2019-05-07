using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public partial class Chef
    {
        internal Dummy Dummy { get; private set; }
        internal StoreOf<TableX> TableXStore { get; private set; }
        internal StoreOf<ColumnX> ColumnXStore { get; private set; }
        internal StoreOf<RelationX> RelationXStore { get; private set; }

        internal StoreOf<Property> PropertyStore { get; private set; }
        internal StoreOf<Relation> RelationStore { get; private set; }
        internal StoreOf<Relation> RelationZStore { get; private set; }

        #region InitializeStores  =============================================
        private void InitializeStores()
        {
            Dummy = new Dummy(this);
            RelationZStore = new StoreOf<Relation>(this, Trait.RelationZStore, 10);

            PropertyStore = new StoreOf<Property>(this, Trait.PropertyStore, 100);
            RelationStore = new StoreOf<Relation>(this, Trait.RelationStore, 30);

            TableXStore = new StoreOf<TableX>(this, Trait.TableXStore, 30);
            ColumnXStore = new StoreOf<ColumnX>(this, Trait.ColumnXStore, 300);
            RelationXStore = new StoreOf<RelationX>(this, Trait.RelationXStore, 300);
        }
        #endregion
    }
}
