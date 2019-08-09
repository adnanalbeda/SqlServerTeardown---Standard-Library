using System;
using System.Collections.Generic;
using System.Data;
using SqlServerTeardown.Components;
using SqlServerTeardown.Builders;

namespace SqlServerTeardown
{
    public static partial class TearDB
    {
        public static void Analyze(IDbConnection connection)
        {
            if (!connection.ToString().Contains("System.Data.SqlClient"))
            {
                throw new InvalidOperationException("Only Sql Server is supported.");
            }
            BuilderHelper.BuildSchemas(connection);
            BuilderHelper.BuildTables(connection);
            BuilderHelper.BuildTableColumns(connection);
            BuilderHelper.BuildViewColumns(connection);
            BuilderHelper.BuildRelation(connection);
        }

        public static IReadOnlyDictionary<string, Schema> Schemas => BuilderHelper.Schemas == null ? throw new ArgumentNullException() : BuilderHelper.Schemas;
        public static IReadOnlyCollection<Table> Tables => BuilderHelper.Tables == null ? throw new ArgumentNullException() : BuilderHelper.Tables;
        public static IReadOnlyCollection<Column> Columns => BuilderHelper.Columns == null ? throw new ArgumentNullException() : BuilderHelper.Columns;
        public static IReadOnlyDictionary<string, Relation> Relations => BuilderHelper.Relations == null ? throw new ArgumentNullException() : BuilderHelper.Relations;
    }
}
