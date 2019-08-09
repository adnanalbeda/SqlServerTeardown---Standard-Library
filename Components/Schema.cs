using System;
using System.Collections;
using System.Collections.Generic;

namespace SqlServerTeardown.Components
{
    public class Schema : IEnumerable<Table>
    {
        internal Schema(string parent, string name)
        {
            Name = name;
            ParentName = parent;
        }

        public string Name { get; }
        public string ParentName { get; }

        public Dictionary<string, Table> Tables = new Dictionary<string, Table>();

        public Table this[string v] => Tables[v];

        internal void AddTable(string name, string type)
        {
            Tables.Add(name, new Table(this, name, type));
        }

        internal void AddTable(Table t)
        {
            Tables.Add(t.Name, t);
        }

        #region IEnumerable Members

        public IEnumerator<Table> GetEnumerator()
        {
            return Tables.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Tables.Values.GetEnumerator();
        }

        #endregion
    }
}
