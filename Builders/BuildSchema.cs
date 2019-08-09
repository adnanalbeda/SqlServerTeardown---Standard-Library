using System.Collections.Generic;
using System.Data;
using SqlServerTeardown.Components;

namespace SqlServerTeardown.Builders
{
    static partial class BuilderHelper
    {
        internal static void BuildSchemas(IDbConnection con)
        {
            try
            {
                con = ConnectionHelper.OpenConnection(con);
                using (var cmd = con.CreateCommand())
                {
                    Schemas = new Dictionary<string, Schema>();
                    cmd.CommandText = Queries.SchemaInfoQuery;
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            Schemas.Add(reader.GetString(1),
                                new Schema(reader.GetString(0), reader.GetString(1)));
                        }
                }
            }
            finally
            {
                ConnectionHelper.CloseConnection(con);
            }
        }
        internal static Dictionary<string, Schema> Schemas;
    }
}
