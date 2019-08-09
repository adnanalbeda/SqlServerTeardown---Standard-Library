using System.Data;
using System.Collections.Generic;
using SqlServerTeardown.Components;

namespace SqlServerTeardown.Builders
{
    static partial class BuilderHelper
    {
        internal static void BuildTableColumns(IDbConnection con)
        {
            try
            {
                con = ConnectionHelper.OpenConnection(con);
                using (var cmd = con.CreateCommand())
                {
                    Columns = new HashSet<Column>();
                    cmd.CommandText = Queries.TablesColumnsQuery;
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            Column c;
                            try
                            {
                                c =
                                    new Column(Schemas[reader.GetString(0)][reader.GetString(1)],
                                    reader.GetString(2));
                                Columns.Add(c);
                                c.Table.AddColumn(c);
                                FillColumn(reader, c);
                            }
                            catch { }
                        }
                }
            }
            finally
            {
                ConnectionHelper.CloseConnection(con);
            }
        }

        internal static void BuildViewColumns(IDbConnection con)
        {
            try
            {
                con = ConnectionHelper.OpenConnection(con);
                using (var cmd = con.CreateCommand())
                {
                    Columns = new HashSet<Column>();
                    cmd.CommandText = Queries.ViewsColumnsQuery;
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            Column c;
                            try
                            {
                                c =
                                    new Column(Schemas[reader.GetString(0)][reader.GetString(1)],
                                    reader.GetString(2));
                                FillColumn(reader, c);
                                Columns.Add(c);
                                c.Table.AddColumn(c);
                            }
                            catch { }
                        }

                }
            }
            finally
            {
                ConnectionHelper.CloseConnection(con);
            }
        }

        private static void FillColumn(IDataReader reader, Column column)
        {
            column.Type = reader.GetString(3);
            column.IsNullable = reader.IsDBNull(4) ? true : reader.GetString(4) == "YES";
            column.DefaultDefinition = reader.IsDBNull(5) ? "" : reader.GetString(5);
            column.IsIdentity = reader.GetBoolean(6);
            if (reader.IsDBNull(7))
                column.MaxLength = null;
            else
                column.MaxLength = reader.GetInt32(7);
            column.Precisions = reader.IsDBNull(8) ? default : reader.GetByte(8);
            column.Scale = reader.IsDBNull(9) ? default : reader.GetByte(9);
            column.IsXmlDocumented = reader.GetBoolean(10);
            column.IsComputed = reader.GetBoolean(11);
            column.IsNonSqlSubscribed = reader.GetBoolean(12);
            column.IsRowGUID = reader.GetBoolean(13);
            column.CheckClause = reader.IsDBNull(14) ? "" : reader.GetString(14);
            column.ComputingDefinition = reader.IsDBNull(15) ? "" : reader.GetString(15);

            if (reader.IsDBNull(16))
                column.IsAnsiPadded = null;
            else
                column.IsAnsiPadded = reader.GetBoolean(16);
            if (reader.IsDBNull(17))
                column.IsFileStream = null;
            else
                column.IsFileStream = reader.GetBoolean(17);
            if (reader.IsDBNull(18))
                column.IsSparse = null;
            else
                column.IsSparse = reader.GetBoolean(18);
        }

        internal static HashSet<Column> Columns;
    }
}
