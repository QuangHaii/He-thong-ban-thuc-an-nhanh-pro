using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace He_thong_ban_thuc_an_nhanh
{
    internal class Modify
    {
        public Modify()
        {
        }
        SqlDataAdapter dataAdapter;
        SqlCommand sqlCommand;
        public DataTable Table(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = Connection.GetConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query,sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            return dataTable;
        }
        public void Command(string query)
        {
            using (SqlConnection sqlConnection = Connection.GetConnection())
            {
                sqlConnection.Open();
                sqlCommand= new SqlCommand(query,sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}
