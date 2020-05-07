using System;
using System.Collections.Generic;

namespace Cupcake.Core.Domain
{
    public class CreateTable
    {
        public string TableName { get; set; }

        public virtual IList<ColumnModel> ColumnModels { get; set; }
    }

    public class ColumnModel
    {
        public string ColumnName { get; set; }

        public ColumnType ColumnType { get; set; }

        public Int32 ColumnLength { get; set; }
    }
}
