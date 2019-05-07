using System;

namespace NodeModel
{/*

 */
    public class PropertyOf<T1, T2> : Property, IValueStore<T2> where T1 : Item
    {
        internal Func<Item, T2> GetValFunc;
        internal Func<Item, T2, bool> SetValFunc;
        internal Type EnumType;

        #region Constructor  ==================================================
        internal PropertyOf(Store owner, Trait trait, Type enumType = null)
        {
            Owner = owner;
            Trait = trait;
            EnumType = enumType;

            owner.Add(this);
        }
        #endregion

        #region Property  =====================================================
        internal T1 Cast(Item item) { return item as T1; }

        internal bool Valid(Item item) { return (item is T1); }
        internal bool Invalid(Item item) { return !(item is T1); }

        #endregion

        #region IValueStore  ==================================================
        public int Count => 0;
        public void Clear() { }
        public bool IsSpecific(Item key) => true;
        public void Remove(Item key) { }

        public bool GetVal(Item key, out T2 val) { if (GetValFunc == null) return Value.NoValue(out val); val = GetValFunc(key); return true; }
        public bool SetVal(Item key, T2 value) => (SetValFunc is null) ? false : SetValFunc(key, value);
        #endregion
    }
}

