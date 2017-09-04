using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Odbc;

namespace RateCard.ApiCore.Dao
{
   public class SqlDao
    {
        private static SqlDao _instance;
        private const string Connection_String = "RC_DB_CONN";
        private SqlDao()
        {

        }

        public static SqlDao GetInstance()
        {
            return _instance ?? (_instance = new SqlDao());
        }

        public void ExecuteProcedure(SqlOperation sqlOperation)
        {

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Connection_String].ConnectionString))
            using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                foreach (var param in sqlOperation.Parameters)
                {
                    command.Parameters.Add(param);
                }
                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation sqlOperation)
        {
            List<Dictionary<string, object>> lstResult = new List<Dictionary<string, object>>();

            using (var conn =
                new SqlConnection(ConfigurationManager.ConnectionStrings[Connection_String].ConnectionString))
            using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                foreach (var param in sqlOperation.Parameters)
                {
                    command.Parameters.Add(param);
                }

                conn.Open();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        for (var lp = 0; lp < reader.FieldCount; lp++)
                        {
                            dict.Add(reader.GetName(lp), reader.GetValue(lp));
                        }
                        lstResult.Add(dict);
                    }
                }
            }

            return lstResult;
        }
    }
}
