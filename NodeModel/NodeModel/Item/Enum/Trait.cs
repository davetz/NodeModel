using System;

namespace NodeModel
{
    [Flags]
    public enum Trait : ushort
    {/*
        Provides identity for item, enum, pair, store, model, relation, property, and commands. 
        It also is used as a key to locate resource strings.
        
        Resource string keys are of the form:
        xxxK - the item's Kind 
        xxxN - the item's Name
        xxxS - the item's Summary (tooltip text)
        xxxV - the item's Description
        where xxx are the three hex digits enumerated in this file
     */
        #region Flags  ========================================================
        Empty = 0,

        IsExternal = 0x8000, // Item, specify load/save to storage file

        IsCovert = 0x4000, // Property - don't include in model change log
        IsReadOnly = 0x2000, // Property
        CanMultiline = 0x1000, // Property

        GetStorageFile = 0x1000, // Command

        IsErrorAux1 = 0x1000, // Error, Model looks up errors using its Aux1 item
        IsErrorAux2 = 0x2000, // Error, Model looks up errors using both its Aux1 and Aux2 items
        IsErrorAux = 0x3000, // Error, Model looks up errors using one or both of its Aux items


        KeyMask = 0xFFF,
        FlagMask = 0xF000,
        EnumMask = 0x3F,
        IndexMask = 0xF,

        #endregion

        #region MainUI  ==============================================(000-01F)
        // resource string keys used by the main UI 
        // (not associated with any individual item, model or command)

        BlankName = 0x001,
        InvalidItem = 0x002,
        ModelGraphTitle = 0x003,
        AppRootModelTab = 0x004,
        ExpandLeft = 0x006,
        TotalCount = 0x007,
        FilterText = 0x008,
        FilterCount = 0X009,
        ExpandRight = 0x00A,
        FilterExpand = 0x00B,
        NewModel = 0x00C,
        EditSymbol = 0x00D,
        SortMode = 0x010,

        #endregion

        #region Command  =============================================(020-07F)

        NewCommand = 0x21,
        OpenCommand = 0x22 | GetStorageFile,
        SaveCommand = 0x23,
        SaveAsCommand = 0x24 | GetStorageFile,
        ReloadCommand = 0x25,
        CloseCommand = 0x26,

        EditCommand = 0x30,
        ViewCommand = 0x31,
        UndoCommand = 0x32,
        RedoCommand = 0x33,
        MergeCommand = 0x34,
        InsertCommand = 0x35,
        RemoveCommand = 0x36,
        CreateCommand = 0x37,
        RefreshCommand = 0x38,
        ExpandAllCommand = 0x39,
        MakeRootLinkCommand = 0x3A,
        MakePathHeadCommand = 0x3B,
        MakeGroupHeadCommand = 0x3C,
        MakeEgressHeadCommand = 0x3D,
        #endregion

        #region Store ================================================(0E0-0FF)
        // root level containers for the hierarchal item trees

        EnumXStore = 0x0E1,
        ViewXStore = 0x0E2,
        TableXStore = 0x0E3,
        GraphXStore = 0x0E4,
        QueryXStore = 0x0E5,
        ValueXStore = 0x0E6,
        SymbolXStore = 0x0E7,
        ColumnXStore = 0x0E8,
        ComputeXStore = 0x0E9,
        RelationXStore = 0x0EA,

        PrimeStore = 0x0F0, // exposes internal tables (metadata / configuration)
        EnumZStore = 0x0F1,
        ErrorStore = 0x0F2,
        GroupStore = 0x0F3,
        PropertyStore = 0x0F4,
        RelationStore = 0x0F5,
        RelationZStore = 0x0F6,

        #endregion

        #region Item  ================================================(100-1FF)

        //=========================================
        Dummy = 0x100,
        NodeParm = 0x101,
        ImportBinaryReader = 0x102,
        ExportBinaryWriter = 0x103,

        DataChef = 0x112,

        //=========================================
        ChangeRoot = 0x131,
        ChangeSet = 0x132,
        ItemUpdated = 0x133,
        ItemCreated = 0x134,
        ItemRemoved = 0x135,
        ItemLinked = 0x136,
        ItemUnlinked = 0x137,
        ItemMoved = 0x138,
        ItemChildMoved = 0x139,
        ItemParentMoved = 0x13A,

        //=========================================
        // External (user-defined) item classes
        RowX = 0x141 | IsExternal,
        PairX = 0x142 | IsExternal,
        EnumX = 0x143 | IsExternal,
        ViewX = 0x144 | IsExternal,
        TableX = 0x145 | IsExternal,
        GraphX = 0x146 | IsExternal,
        QueryX = 0x147 | IsExternal,
        SymbolX = 0x148 | IsExternal,
        ColumnX = 0x149 | IsExternal,
        ComputeX = 0x14A | IsExternal,
        CommandX = 0x14B | IsExternal,
        RelationX = 0x14C | IsExternal,


        //=========================================
        Graph = 0x1C1,
        Level = 0x1C2,
        Node = 0x1C3,
        Edge = 0x1C4,
        Open = 0x1C5,

        #endregion

        #region Relation  ============================================(300-3FF)

        Relation = 0x300,

        //=========================================
        EnumX_ColumnX = 0x311,
        TableX_ColumnX = 0x312,
        TableX_NameProperty = 0x313,
        TableX_SummaryProperty = 0x314,
        TableX_ChildRelationX = 0x315,
        TableX_ParentRelationX = 0x316,

        //=========================================
        TableChildRelationGroup = 0x321,
        TableParentRelationGroup = 0x322,
        TableReverseRelationGroup = 0x323,
        TableRelationGroupRelation = 0x324,
        ParentRelationGroupRelation = 0x325,
        ReverseRelationGroupRelation = 0x326,

        //=========================================
        Item_Error = 0x331,
        ViewX_ViewX = 0x332,
        ViewX_QueryX = 0x333,
        QueryX_ViewX = 0x334,
        Property_ViewX = 0x335,
        Relation_ViewX = 0x336,
        ViewX_Property = 0x337,
        QueryX_Property = 0x338,

        //=========================================
        GraphX_SymbolX = 0x341,
        SymbolX_QueryX = 0x342,
        GraphX_QueryX = 0x343,
        QueryX_QueryX = 0x344,
        GraphX_ColorColumnX = 0x345,
        GraphX_SymbolQueryX = 0x346,

        //=========================================
        Store_QueryX = 0x351,
        Relation_QueryX = 0x352,

        //=========================================
        Store_ComputeX = 0x361,
        ComputeX_QueryX = 0x362,

        //=========================================
        Store_Property = 0x3FD,
        Store_ChildRelation = 0x3FE,
        Store_ParentRelation = 0x3FF,

        #endregion

        #region Property  ============================================(400-5FF)

        Property = 0x400,

        //=========================================
        ViewName_P = 0x401,
        ViewSummary_P = 0x402,
        IncludeItemIdentityIndex_P = 0x403 | IsCovert,

        //=========================================
        EnumName_P = 0x411,
        EnumSummary_P = 0x412,
        EnumText_P = 0x413,
        EnumValue_P = 0x414,

        //=========================================
        TableName_P = 0x421,
        TableSummary_P = 0x422,

        //=========================================
        ColumnName_P = 0x431,
        ColumnSummary_P = 0x432,
        ColumnValueType_P = 0x433,
        ColumnAccess_P = 0x434,
        ColumnInitial_P = 0x435,
        ColumnIsChoice_P = 0x436,

        //=========================================
        RelationName_P = 0x441,
        RelationSummary_P = 0x442,
        RelationPairing_P = 0x443,
        RelationIsRequired_P = 0x444,
        RelationIsReference_P = 0x445,
        RelationMinOccurance_P = 0x446,
        RelationMaxOccurance_P = 0x447,

        //=========================================
        GraphName_P = 0x451,
        GraphSummary_P = 0x452,
        GraphTerminalLength_P = 0x453,
        GraphTerminalSpacing_P = 0x454,
        GraphTerminalStretch_P = 0x455,
        GraphSymbolSize_P = 0x456,

        //=========================================
        QueryXSelect_P = 0x460 | CanMultiline,
        QueryXWhere_P = 0x461 | CanMultiline,

        QueryXConnect1_P = 0x462,
        QueryXConnect2_P = 0x463,

        QueryXRelation_P = 0x466,
        QueryXIsReversed_P = 0x467,
        QueryXIsImmediate_P = 0x468,
        QueryXIsPersistent_P = 0x469,
        QueryXIsBreakPoint_P = 0x46A,
        QueryXExclusiveKey_P = 0x46B,
        QueryXAllowSelfLoop_P = 0x46C,
        QueryXIsPathReversed_P = 0x46D,
        QueryXIsFullTableRead_P = 0x46E,
        QueryXFacet1_P = 0x46F,
        QueryXFacet2_P = 0x470,
        ValueXWhere_P = 0x471 | CanMultiline,
        ValueXSelect_P = 0x472 | CanMultiline,
        ValueXIsReversed_P = 0x473,
        ValueXValueType_P = 0x474 | IsReadOnly,
        QueryXLineStyle_P = 0x475,
        QueryXDashStyle_P = 0x476,
        QueryXLineColor_P = 0x477,

        //=========================================
        SymbolXName_P = 0x481,
        SymbolXAttatch_P = 0x486,

        //=========================================
        NodeCenterXY_P = 0x491 | IsCovert,
        NodeSizeWH_P = 0x492 | IsCovert,
        NodeLabeling_P = 0x493 | IsCovert,
        NodeResizing_P = 0x494 | IsCovert,
        NodeBarWidth_P = 0x495 | IsCovert,
        NodeOrientation_P = 0x496 | IsCovert,
        NodeFlipRotate_P = 0x497 | IsCovert,

        //=========================================
        EdgeFace1_P = 0x4A1 | IsCovert,
        EdgeFace2_P = 0x4A2 | IsCovert,
        EdgeFacet1_P = 0x4A3 | IsCovert,
        EdgeFacet2_P = 0x4A4 | IsCovert,
        EdgeConnect1_P = 0x4A5 | IsCovert,
        EdgeConnect2_P = 0x4A6 | IsCovert,

        //=========================================
        ComputeXName_P = 0x4B1,
        ComputeXSummary_P = 0x4B2,
        ComputeXCompuType_P = 0x4B3,
        ComputeXWhere_P = 0x4B4,
        ComputeXSelect_P = 0x4B5,
        ComputeXSeparator_P = 0x4B6,
        ComputeXValueType_P = 0x4B7 | IsReadOnly,
        ComputeXNumericSet_P = 0x4B8,
        ComputeXResults_P = 0x4B9,
        ComputeXSorting_P = 0x4BA,
        ComputeXTakeSet_P = 0x4BB,
        ComputeXTakeLimit_P = 0x4BC,
        #endregion

        #region Model ================================================(600-7FF)

        //=====================================================================
        ParmDebugList_M = 0x600,
        S_601_M = 0x601,
        S_602_M = 0x602,
        S_603_M = 0x603,
        S_604_M = 0x604,
        S_605_M = 0x605,
        S_606_M = 0x606,
        S_607_M = 0x607,
        S_608_M = 0x608,
        S_609_M = 0x609,
        S_60A_M = 0x60A,
        S_60B_M = 0x60B,
        S_60C_M = 0x60C,
        S_60D_M = 0x60D,
        S_60E_M = 0x60E,
        S_60F_M = 0x60F,

        //=====================================================================
        S_610_M = 0x610,
        S_611_M = 0x611,
        DataChef_M = 0x612,
        S_613_M = 0x613,
        TextColumn_M = 0x614 | IsErrorAux1,
        CheckColumn_M = 0x615 | IsErrorAux1,
        ComboColumn_M = 0x616 | IsErrorAux1,
        TextProperty_M = 0x617 | IsErrorAux1,
        CheckProperty_M = 0x618 | IsErrorAux1,
        ComboProperty_M = 0x619 | IsErrorAux1,
        TextCompute_M = 0x61A | IsErrorAux1,
        S_61B_M = 0x61B,
        S_61C_M = 0x61C,
        S_61D_M = 0x61D,
        S_61E_M = 0x61E,
        S_61F_M = 0x61F,

        //=====================================================================
        ParmRoot_M = 0x620,
        ErrorRoot_M = 0x621,
        ChangeRoot_M = 0x622,
        MetadataRoot_M = 0x623,
        ModelingRoot_M = 0x624,
        MetaRelationList_M = 0x625,
        ErrorType_M = 0x626,
        ErrorText_M = 0x627,
        ChangeSet_M = 0x628,
        ItemChange_M = 0x629,
        S_62A_M = 0x62A,
        S_62B_M = 0x62B,
        S_62C_M = 0x62C,
        S_62D_M = 0x62D,
        MetadataSubRoot_M = 0x62E,
        ModelingSubRoot_M = 0x62F,

        //=====================================================================
        S_630_M = 0x630,
        MetaViewViewList_M = 0x631,
        MetaViewView_M = 0x632,
        MetaViewQuery_M = 0x633,
        MetaViewCommand_M = 0x634,
        MetaViewProperty_M = 0x635,
        S_636_M = 0x636,
        S_637_M = 0x637,
        S_638_M = 0x638,
        S_639_M = 0x639,
        ViewViewList_M = 0x63A,
        ViewView_M = 0x63B,
        ViewItem_M = 0x63C,
        ViewQuery_M = 0x63D,
        S_63E_M = 0x63E,
        S_63F_M = 0x63F,

        //=====================================================================
        MetaEnumList_M = 0x642,
        MetaTableList_M = 0x643,
        MetaGraphList_M = 0x644,
        MetaSymbolList_M = 0x645,
        MetaGraphParmList_M = 0x646,

        TableList_M = 0x647,
        GraphList_M = 0x648,

        //=====================================================================
        MetaPair_M = 0x652,
        MetaEnum_M = 0x653,
        MetaTable_M = 0x654,
        MetaGraph_M = 0x655,
        MetaSymbol_M = 0x656,
        MetaColumn_M = 0x657,
        MetaCompute_M = 0x658,
        SymbolEditor_M = 0x659,

        //=====================================================================
        MetaColumnList_M = 0x661,
        MetaChildRelationList_M = 0x662,
        MetaParentRelatationList_M = 0x663,
        MetaEnumPairList_M = 0x664,
        MetaEnumColumnList_M = 0x665,
        MetaComputeList_M = 0x666,
        MetaEnumRelatedColumn_M = 0x667,

        //=====================================================================
        MetaChildRelation_M = 0x671,
        MetaParentRelation_M = 0x672,
        MetaNameColumnRelation_M = 0x673,
        MetaSummaryColumnRelation_M = 0x674,
        MetaNameColumn_M = 0x675,
        MetaSummaryColumn_M = 0x676,

        //=====================================================================
        MetaGraphColoring_M = 0x681,
        MetaGraphRootList_M = 0x682,
        MetaGraphNodeList_M = 0x683,
        MetaGraphNode_M = 0x684,
        MetaGraphColorColumn_M = 0x685,

        //=====================================================================
        MetaGraphRoot_M = 0x691,
        MetaGraphLink_M = 0x692,
        MetaGraphPathHead_M = 0x693,
        MetaGraphPathLink_M = 0x694,
        MetaGraphGroupHead_M = 0x695,
        MetaGraphGroupLink_M = 0x696,
        MetaGraphEgressHead_M = 0x697,
        MetaGraphEgressLink_M = 0x698,
        MetaGraphNodeSymbol_M = 0x699,

        MetaValueHead_M = 0x69E,
        MetaValueLink_M = 0x69F,

        //=====================================================================
        Row_M = 0x6A1,
        Table_M = 0x6A4,
        Graph_M = 0x6A5,
        GraphRef_M = 0x6A6,
        RowChildRelation_M = 0x6A7,
        RowParentRelation_M = 0x6A8,
        RowRelatedChild_M = 0x6A9,
        RowRelatedParent_M = 0x6AA,

        //=====================================================================
        RowPropertyList_M = 0x6B1,
        RowChildRelationList_M = 0x6B2,
        RowParentRelationList_M = 0x6B3,
        RowDefaultPropertyList_M = 0x6B4,
        RowUnusedChildRelationList_M = 0x6B5,
        RowUnusedParentRelationList_M = 0x6B6,
        RowComputeList_M = 0x6B7,

        //=====================================================================
        QueryRootLink_M = 0x6C1,
        QueryPathHead_M = 0x6C2,
        QueryPathLink_M = 0x6C3,
        QueryGroupHead_M = 0x6C4,
        QueryGroupLink_M = 0x6C5,
        QueryEgressHead_M = 0x6C6,
        QueryEgressLink_M = 0x6C7,

        //=====================================================================
        QueryRootItem_M = 0x6D1,
        QueryPathStep_M = 0x6D2,
        QueryPathTail_M = 0x6D3,
        QueryGroupStep_M = 0x6D4,
        QueryGroupTail_M = 0x6D5,
        QueryEgressStep_M = 0x6D6,
        QueryEgressTail_M = 0x6D7,

        //=====================================================================
        GraphXRef_M = 0x6E1,
        GraphNodeList_M = 0x6E2,
        GraphEdgeList_M = 0x6E3,
        GraphRootList_M = 0x6E4,
        GraphLevelList_M = 0x6E5,

        GraphLevel_M = 0x6E6,
        GraphPath_M = 0x6E7,
        GraphRoot_M = 0x6E8,
        GraphNode_M = 0x6E9,
        GraphEdge_M = 0x6EA,

        GraphOpenList_M = 0x6EB,
        GraphOpen_M = 0x6EC,

        //=====================================================================
        PrimeCompute_M = 0x7D0,
        ComputeStore_M = 0x7D1,

        //=====================================================================
        InternalStoreList_M = 0x7F0,
        InternalStore_M = 0x7F1,

        StoreItem_M = 0x7F2,

        StoreItemItemList_M = 0x7F4,
        StoreRelationLinkList_M = 0x7F5,

        StoreChildRelationList_M = 0x7F6,
        StoreParentRelationList_M = 0x7F7,

        StoreItemItem_M = 0x7F8,
        StoreRelationLink_M = 0x7F9,

        StoreChildRelation_M = 0x7FA,
        StoreParentRelation_M = 0x7FB,

        StoreRelatedItem_M = 0x7FC,
        #endregion
    }
}
