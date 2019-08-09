using System;
using System.Collections.Generic;
using System.Text;

namespace SqlServerTeardown.Components
{
    public class Column
    {
        internal Column(Table table, string name)
        {
            Table = table;
            Name = name;
        }

        public Table Table { get; }
        public string Name { get; }
        public string _Type;
        public string Type
        {
            get => _Type;
            internal set
            {
                _Type = value;
            }
        }

        public bool IsNullable { get; internal set; }
        private string defaultDefinition;
        public string DefaultDefinition
        {
            get => defaultDefinition;
            set
            {
                defaultDefinition = value.Trim();
                HasDefaultValue = !string.IsNullOrEmpty(defaultDefinition);
            }
        }
        public bool HasDefaultValue { get; private set; }
        public bool IsIdentity { get; internal set; }

        public int? MaxLength { get; internal set; }
        public byte Precisions { get; internal set; }
        public byte Scale { get; internal set; }

        public bool IsComputed { get; internal set; }
        public string ComputingDefinition { get; internal set; }

        public bool IsRowGUID { get; set; }
        public bool IsNonSqlSubscribed { get; internal set; }
        public bool IsXmlDocumented { get; internal set; }

        private string checkClause;
        public string CheckClause
        {
            get => checkClause;
            internal set
            {
                checkClause = value.Trim();
                HasValidationRule = !string.IsNullOrEmpty(defaultDefinition);
            }
        }
        public bool HasValidationRule { get; private set; }

        public bool? IsAnsiPadded { get; internal set; }
        public bool? IsFileStream { get; internal set; }
        public bool? IsSparse { get; internal set; }
    }
}
