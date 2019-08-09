using System.Collections.Generic;
using System.Data;
using SqlServerTeardown.Components;

namespace SqlServerTeardown.Builders
{
    static partial class BuilderHelper
    {
        internal static void BuildRelation(IDbConnection con)
        {
            try
            {
                con = ConnectionHelper.OpenConnection(con);
                using (var cmd = con.CreateCommand())
                {
                    Relations = new Dictionary<string, Relation>();
                    cmd.CommandText = Queries.RelationsQuery;
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            if (!Relations.ContainsKey(reader.GetString(0)))
                            {
                                var re = new Relation(
                                reader.GetString(0),
                                reader.GetInt32(7));
                                Relations.Add(re.Name, re);
                            }
                            var r = Relations[reader.GetString(0)];
                            for (int i = 0; i < r.Length; i++)
                            {
                                if (r[i] == null)
                                {
                                    r.AddColumn(i,
                                        Schemas[reader.GetString(1)][reader.GetString(2)][reader.GetString(8)],
                                        Schemas[reader.GetString(4)][reader.GetString(5)][reader.GetString(10)]);
                                    r[i].Child.Table.ParentTables.Add(r[i].Parent.Table);
                                    r[i].Parent.Table.ChildTables.Add(r[i].Child.Table);
                                    break;
                                }
                            }
                        }
                }
            }
            finally
            {
                ConnectionHelper.CloseConnection(con);
            }
        }
        internal static Dictionary<string, Relation> Relations;
    }
}
