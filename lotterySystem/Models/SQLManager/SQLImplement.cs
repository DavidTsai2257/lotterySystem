using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace lotterySystem.Models.SQLManager
{
    public class SQLImplement
    {
        //讀取連線設定
        private readonly string Connstr = ConfigurationManager.ConnectionStrings["lotterySystemConnectionString"].ConnectionString;
        public bool SqlExecuteNonQuery(string dml, SqlParameter[] sqlParameters)
        {
            bool result = true;
            int value;
            using (SqlConnection con = new SqlConnection(Connstr))
            using (SqlCommand com = con.CreateCommand())
            {
                try
                {
                    con.Open();
                    com.CommandText = dml;
                    com.Parameters.Clear();
                    com.Parameters.AddRange(sqlParameters);
                    value = com.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    result = false;
                    throw ex;
                }
            }
            return result;
        }
        public bool SqlExecuteNonQuery(string dml)
        {
            bool result = true;
            int value;
            using (SqlConnection con = new SqlConnection(Connstr))
            using (SqlCommand com = con.CreateCommand())
            {
                try
                {
                    con.Open();
                    com.CommandText = dml;
                    com.Parameters.Clear();
                    value = com.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    result = false;
                    throw ex;
                }
            }
            return result;
        }
    }
}