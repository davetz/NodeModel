using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public partial class Chef
    {
        #region RepositoryError  ==============================================

        public bool A_ReadError;

        public void AddRepositorReadError(string text)
        {
            A_ReadError = true;
            var str = text;
            //_itemError[ImportBinaryReader] = new ErrorOne(ErrorStore, this, Trait.ImportError, text);
        }
        public void AddRepositorWriteError(string text)
        {
            var str = text;
            //_itemError[ExportBinaryWriter] = new ErrorOne(ErrorStore, this, Trait.ExportError, text);
        }
        #endregion
    }
}
