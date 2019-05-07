using System;
using System.Collections.Generic;
using System.Text;

namespace NodeModel
{
    public partial class Chef
    {
        internal TableX CreateTableX() => new TableX(TableXStore);

    }
}
