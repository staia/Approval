using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Approval.Services
{
    public class DbConnection
    {
        string _connect;
        public IDbConnection Conneсt
        {
            get
            {
                return new SqlConnection(_connect);
            }
        }

        public DbConnection(string connect)
        {
           _connect = connect;
        }

    }
}
