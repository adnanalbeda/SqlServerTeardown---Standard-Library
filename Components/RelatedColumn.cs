using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SqlServerTeardown.Components
{
    public partial class Relation
    {
        public class RelatedColumns : Tuple<Column, Column>
        {
            internal RelatedColumns(Column child, Column parent) : base(child, parent) { }

            public Column Child => this.Item1;
            public Column Parent => this.Item2;
        }
    }
}
