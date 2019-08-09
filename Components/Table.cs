using System.Collections;
using System.Collections.Generic;

namespace SqlServerTeardown.Components
{
    public class Table : IEnumerable<Column>
    {
        internal Table(Schema schema, string name, string type)
        {
            Schema = schema;
            Name = name;
            Type = type;
        }

        public Schema Schema { get; }
        public string Name { get; }
        public string Type { get; }

        public Dictionary<string, Column> Columns = new Dictionary<string, Column>();
        public HashSet<Table> ParentTables = new HashSet<Table>();
        public HashSet<Table> ChildTables = new HashSet<Table>();

        public Column this[string v] => Columns[v];

        internal void AddColumn(string name)
        {
            var c = new Column(this, name);
            Columns.Add(name, c);
        }
        internal void AddColumn(Column c)
        {
            Columns.Add(c.Name, c);
        }

        #region IEnumerable Members

        public IEnumerator<Column> GetEnumerator()
        {
            return Columns.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Columns.Values.GetEnumerator();
        }

        #endregion
    }
}
