using System;
using System.Data.SqlClient;
using System.Data;
using CMCommon.Logging.Interfaces;

namespace CMCommon.Logging.Implementation
{
    /// <summary>
    /// Generic database logging connection string is needed and stored proc LogItem and table
    /// following table fields (message, sessionid, and createdate)
    /// </summary>
    public class DbLoggerService : ILoggerService
    {
        private readonly string _sessionId;
        private readonly string _connString;

        public DbLoggerService(string sessionId, string connectionString)
        {
            _connString = connectionString;
            _sessionId = sessionId;
        }
        /// <summary>
        /// youwill need to implement the following stored procedures LogItem in your DB
        /// also create table with the following fields (message, sessionid, and createdate)
        /// </summary>
        /// <param name="message"></param>
        public void LogItem(string message)
        {
            using(var conn = new SqlConnection(_connString))
            {
                var cmd = new SqlCommand("LogItem", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parameters =
                    {
                      new SqlParameter("@message", message),
                      new SqlParameter("@sessionid", _sessionId),
                      new SqlParameter("@createdate", DateTime.Now)
                    };

                cmd.Parameters.AddRange(parameters);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }             
    }
}
