using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace QuanLyChiTieu.Classes
{
    public class DatabaseProcess
    {
        string connectString = ConfigurationManager.ConnectionStrings["QuanLyChiTieuConnection"].ConnectionString;
        SqlConnection conn = null;

        public void OpenConnect()
        {
            conn = new SqlConnection(connectString);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        public void CloseConnect()
        {
            if (conn != null && conn.State != ConnectionState.Closed)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public DataTable DataReader(string sqlSelect, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            OpenConnect();

            SqlCommand cmd = new SqlCommand(sqlSelect,conn);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);

            CloseConnect();
            return dt;
        }

        public void ChangeData(string sql, SqlParameter[] parameters = null)
        {
            OpenConnect();

            SqlCommand comm = new SqlCommand(sql,conn);

            if (parameters != null)
            {
                comm.Parameters.AddRange(parameters);
            }

            comm.ExecuteNonQuery();

            CloseConnect();
        }
    }
}
