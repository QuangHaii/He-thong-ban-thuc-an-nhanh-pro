using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace He_thong_ban_thuc_an_nhanh
{
    internal class Connection
    {
        private static string stringConnection = @"Data Source=LAPTOP-HAINOOB\SQLEXPRESS;Initial Catalog=QLBANHANG;Integrated Security=True";
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(stringConnection);
        }
    }
}
