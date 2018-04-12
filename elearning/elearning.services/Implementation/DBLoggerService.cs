using System;
using elearning.services.Interfaces;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace elearning.services.Implementation
{
    public class DbLoggerService : ILoggerService
    {
        private readonly string _sessionId;
        private readonly string _connString;

        public DbLoggerService(string sessionId, string connectionString)
        {
            _connString = connectionString;
            _sessionId = sessionId;
        }

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
