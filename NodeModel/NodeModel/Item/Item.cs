using System;

namespace NodeModel
{
    public abstract class Item
    {
        internal Item Owner;

        internal Trait Trait;       //identity, static flags, and resource string key
        private State _state;       //bit flags specific to each item type
        private byte _flags;        //IsNew, IsDeleted, AutoExpandLeft, AutoExpandRight,..

        internal byte ModelDelta;   //incremented when a property or relation is changed
        internal byte ChildDelta;   //incremented when list of child items is changed

        #region Trait  ========================================================
        internal bool IsExternal => (Trait & Trait.IsExternal) != 0;
        internal bool IsDataChef => (Trait == Trait.DataChef);
        internal bool IsViewX => (Trait == Trait.ViewX);
        internal bool IsPairX => (Trait == Trait.PairX);
        internal bool IsRowX => (Trait == Trait.RowX);
        internal bool IsEnumX => (Trait == Trait.EnumX);
        internal bool IsTableX => (Trait == Trait.TableX);
        internal bool IsGraphX => (Trait == Trait.GraphX);
        internal bool IsQueryX => (Trait == Trait.QueryX);
        internal bool IsSymbolX => (Trait == Trait.SymbolX);
        internal bool IsColumnX => (Trait == Trait.ColumnX);
        internal bool IsComputeX => (Trait == Trait.ComputeX);
        //internal bool IsCommandX => (Trait == Trait.CommandX);
        internal bool IsRelationX => (Trait == Trait.RelationX);
        internal bool IsGraph => (Trait == Trait.Graph);
        internal bool IsNode => (Trait == Trait.Node);
        internal bool IsEdge => (Trait == Trait.Edge);

        internal bool IsItemMoved => Trait == Trait.ItemMoved;
        internal bool IsItemCreated => Trait == Trait.ItemCreated;
        internal bool IsItemUpdated => Trait == Trait.ItemUpdated;
        internal bool IsItemRemoved => Trait == Trait.ItemRemoved;
        internal bool IsItemLinked => Trait == Trait.ItemLinked;
        internal bool IsItemUnlinked => Trait == Trait.ItemUnlinked;
        internal bool IsItemLinkMoved => Trait == Trait.ItemChildMoved;


        internal bool IsCovert => (Trait & Trait.IsCovert) != 0;
        internal bool IsReadOnly => (Trait & Trait.IsReadOnly) != 0;
        internal bool CanMultiline => (Trait & Trait.CanMultiline) != 0;

        internal byte TraitIndex => (byte)(Trait & Trait.IndexMask);
        internal byte TraitIndexOf(Trait trait) => (byte)(trait & Trait.IndexMask);
        internal bool IsErrorAux => (Trait & Trait.IsErrorAux) != 0;
        internal bool IsErrorAux1 => (Trait & Trait.IsErrorAux1) != 0;
        internal bool IsErrorAux2 => (Trait & Trait.IsErrorAux2) != 0;
        #endregion

        #region State  ========================================================
        private bool GetFlag(State flag) => (_state & flag) != 0;
        private void SetFlag(State flag, bool value = true) { if (value) _state |= flag; else _state &= ~flag; }

        internal bool HasState() => _state != 0;
        internal ushort GetState() => (ushort)_state;
        internal void SetState(ushort state) => _state = (State)state;


        internal bool IsHead { get { return GetFlag(State.IsHead); } set { SetFlag(State.IsHead, value); } }
        internal bool IsTail { get { return GetFlag(State.IsTail); } set { SetFlag(State.IsTail, value); } }
        internal bool IsRoot { get { return GetFlag(State.IsRoot); } set { SetFlag(State.IsRoot, value); } }
        internal bool IsReversed { get { return GetFlag(State.IsReversed); } set { SetFlag(State.IsReversed, value); } }
        internal bool IsRadial { get { return GetFlag(State.IsRadial); } set { SetFlag(State.IsRadial, value); } }

        internal bool IsBreakPoint { get { return GetFlag(State.IsBreakPoint); } set { SetFlag(State.IsBreakPoint, value); } }
        internal bool IsPersistent { get { return GetFlag(State.IsPersistent); } set { SetFlag(State.IsPersistent, value); } }

        internal bool IsLimited { get { return GetFlag(State.IsLimited); } set { SetFlag(State.IsLimited, value); } }
        internal bool IsRequired { get { return GetFlag(State.IsRequired); } set { SetFlag(State.IsRequired, value); } }

        internal bool IsUndone { get { return GetFlag(State.IsUndone); } set { SetFlag(State.IsUndone, value); } }
        internal bool IsVirgin { get { return GetFlag(State.IsVirgin); } set { SetFlag(State.IsVirgin, value); } }
        internal bool IsCongealed { get { return GetFlag(State.IsCongealed); } set { SetFlag(State.IsCongealed, value); } }

        internal bool IsChoice { get { return GetFlag(State.IsChoice); } set { SetFlag(State.IsChoice, value); } }
        internal bool NeedsRefresh { get { return GetFlag(State.NeedsRefresh); } set { SetFlag(State.NeedsRefresh, value); } }

        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 

        internal bool IsNew { get { return (_flags & B1) != 0; } set { _flags = value ? (byte)(_flags | B1) : (byte)(_flags & ~B1); } }
        const byte B1 = 0x1;
        internal bool IsDeleted { get { return (_flags & B2) != 0; } set { _flags = value ? (byte)(_flags | B2) : (byte)(_flags & ~B2); } }
        const byte B2 = 0x2;
        internal bool AutoExpandLeft { get { return (_flags & B3) != 0; } set { _flags = value ? (byte)(_flags | B3) : (byte)(_flags & ~B3); } }
        const byte B3 = 0x4;
        internal bool AutoExpandRight { get { return (_flags & B4) != 0; } set { _flags = value ? (byte)(_flags | B4) : (byte)(_flags & ~B4); } }
        const byte B4 = 0x8;
        #endregion

        #region Property/Methods ==============================================
        internal Store Store => Owner as Store;

        internal int Index => (Owner is Store st) ? st.IndexOf(this) : -1;
        internal bool IsInvalid => IsDeleted;
        internal bool IsValid => !IsInvalid;

        /// <summary>
        /// Walk up item tree hierachy to find the parent Chef
        /// </summary>
        internal Chef GetChef()
        {
            var item = this;
            while (item != null) { if (item is Chef chef) return chef; item = item.Owner; }
            throw new Exception("Corrupted item hierarchy"); // I seriously hope this never happens
        }
        #endregion
    }
}
