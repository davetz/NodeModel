﻿using System;

namespace NodeModel
{
    [Flags]
    public enum State : ushort
    {
        Empty = 0,
        Index = 0x7,
        //=================================================
        HasChoice = 0x1, // TableX
        IsChoice = 0x1, // ColumnX
        IsLimited = 0x1, // Relation
        IsUndone = 0x1, // ItemChange, ChangeSet

        IsVirgin = 0x2, // ChangeSet
        IsRequired = 0x2, // Relation

        IsCongealed = 0x4, // ChangeSet

        IsRoot = 0x8, // QueryX, Query

        //=================================================
        IsHead = 0x10, // QueryX, Query,

        IsTail = 0x20, // QueryX, Query, Path

        IsRadial = 0x40, // QueryX, Query, Path

        IsReversed = 0x80, // QueryX, Query, Path

        //=================================================
        IsBreakPoint = 0x100, // QueryX, Query

        IsPersistent = 0x200, // QueryX, Query

        B11_spare = 0x400, // currently unused

        B12_spare = 0x800, // currently unused
        //=================================================

        B13_spare = 0x1000, // currently unused

        B14_spare = 0x2000, // currently unused

        B15_spare = 0x4000, // currently unused

        NeedsRefresh = 0x8000, // Edge 
    }
}
