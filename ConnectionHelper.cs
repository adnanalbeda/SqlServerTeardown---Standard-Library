using System;
using System.Data;
using System.Diagnostics;

namespace SqlServerTeardown
{
    internal static class ConnectionHelper
    {
        internal static IDbConnection OpenConnection(IDbConnection connection)
        {
            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception x)
                {
                    Trace.WriteLine("--------------------------------------------");
                    Trace.WriteLine("--------------------------------------------");
                    Trace.WriteLine("Error in opening connection: ");
                    Trace.WriteLine(x.Source);
                    Trace.WriteLine(x.Message);
                    Trace.WriteLine("Stack: ");
                    Trace.WriteLine(x.StackTrace);
                    Trace.WriteLine("Inner: ");
                    Trace.WriteLine(x.InnerException == null ? "" : x.InnerException.Message);
                    Trace.WriteLine("--------------------------------------------");
                    throw x;
                }
            }
            return connection;
        }

        internal static IDbConnection CloseConnection(IDbConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
            {
                try
                {
                    connection.Close();
                }
                catch (Exception x)
                {
                    Trace.WriteLine("--------------------------------------------");
                    Trace.WriteLine("--------------------------------------------");
                    Trace.WriteLine("Error in closing connection: ");
                    Trace.WriteLine(x.Source);
                    Trace.WriteLine(x.Message);
                    Trace.WriteLine("Stack: ");
                    Trace.WriteLine(x.StackTrace);
                    Trace.WriteLine("Inner: ");
                    Trace.WriteLine(x.InnerException == null ? "" : x.InnerException.Message);
                    Trace.WriteLine("--------------------------------------------");
                }
            }
            return connection;
        }
    }
}
