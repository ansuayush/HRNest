using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Mitr.DAL
{
    public class clsDataBaseHelper
    {
        private static string ConnectionString;
        //private static string connectionstring_EWAMS;
        static clsDataBaseHelper()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        }

        public static DataSet ExecuteDataSet(string sql)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da;
            da = new SqlDataAdapter(sql, ConnectionString);
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(ds);
            return ds;
        }


        public static DataSet ExecuteDataSet(string sql, SqlParameter[] @params)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, ConnectionString);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = 0;
            for (int i = 0; i <= @params.Length - 1; i++)
            {
                da.SelectCommand.Parameters.AddWithValue(@params[i].ParameterName, @params[i].Value);
            }
            da.Fill(ds);
            return ds;

        }
        public static void ExecuteSp(string spName, SqlParameter[] oParam)
        {
            using (SqlConnection con = new SqlConnection())
            {
                SqlCommand Com = new SqlCommand();
                SqlTransaction Tran = default(SqlTransaction);
                //con = getConnection();

                con.Open();
                Tran = con.BeginTransaction();
                try
                {
                    Com.Connection = con;
                    Com.Transaction = Tran;
                    Com.CommandText = spName;
                    Com.CommandType = CommandType.StoredProcedure;
                    CollectInputParams(ref Com, oParam);
                    Com.ExecuteNonQuery();
                    Tran.Commit();
                }
                catch (Exception ex)
                {
                    Tran.Rollback();
                    throw ex;
                }
                finally
                {
                    Com.Dispose();
                    Tran.Dispose();
                    con.Close();
                    con.Dispose();
                }
            }
        }

        private static void CollectInputParams(ref SqlCommand oCommand, SqlParameter[] oParam)
        {
            int ic = 0;
            for (ic = 0; ic <= oParam.Length - 1; ic++)
            {
                if (oParam[ic] == null)
                {
                }
                else
                {
                    oCommand.Parameters.Add(oParam[ic]);
                }
            }
        }

    }
}
