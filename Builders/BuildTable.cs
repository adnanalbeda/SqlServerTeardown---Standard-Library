using System.Collections.Generic;
using System.Data;
using SqlServerTeardown.Components;

namespace SqlServerTeardown.Builders
{
    static partial class BuilderHelper
    {
        internal static void BuildTables(IDbConnection con)
        {
            try
            {
                con = ConnectionHelper.OpenConnection(con);
                using (var cmd = con.CreateCommand())
                {
                    Tables = new HashSet<Table>();
                    cmd.CommandText = Queries.TablesInfoQuery;
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var t =
                                new Table(Schemas[reader.GetString(0)], reader.GetString(1), reader.GetString(2));
                            Tables.Add(t);
                            t.Schema.AddTable(t);
                        }
                }
            }
            finally
            {
                ConnectionHelper.CloseConnection(con);
            }
        }
        internal static HashSet<Table> Tables;
    }
}
