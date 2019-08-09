using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SqlServerTeardown.Components
{
    public partial class Relation : IEnumerable<Relation.RelatedColumns>
    {
        internal Relation(string name, int columnsCount)
        {
            Name = name;
            Length = columnsCount;
            _RelatedColumns = new RelatedColumns[columnsCount];
        }

        public string Name { get; }
        public int Length { get; }

        private RelatedColumns[] _RelatedColumns;

        public RelatedColumns this[int i]
        {
            get
            {
                return _RelatedColumns[i];
            }
            private set
            {
                _RelatedColumns[i] = value;
            }
        }

        internal void AddColumn(int ind, Column child, Column parent)
        {
            this[ind] = new RelatedColumns(child, parent);
        }

        public IEnumerator<RelatedColumns> GetEnumerator()
        {
            for (int i = 0; i < Length; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
