using System.Numerics;

using Windows.Foundation;

namespace NodeModel
{
    public class RowX : Item
    {
        internal (float X, float Y) Center;

        TableX TableXRef => Owner as TableX; 

        #region CalculatedValues  =============================================
        internal Vector2 CenterPoint { get { return GetCenter(); } set { SetCenter(value); } }
        internal Rect BoundingRect => TableXRef.GetRect(Center);

        private Vector2 GetCenter() => new Vector2(Center.X, Center.Y);
        private void SetCenter(Vector2 val) => Center = (val.X, val.Y);
        #endregion


        #region Constructors  =================================================
        internal RowX(TableX owner)
        {
            Owner = owner;
            Trait = Trait.RowX;
            AutoExpandRight = true;

            owner.Add(this);
        }
        #endregion

        #region Properies/Methods  ============================================
        internal TableX TableX => (Owner as TableX);
        #endregion
    }
}
