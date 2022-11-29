using Approval.Models;
using Dapper;
using System.Data;

namespace Approval.Views.Home
{
    public class PageData
    {
    }

    public class UserData
    {
        public RegisterAtUser Avtorize { get; set; }
        public UserData(IDbConnection connect)
        {
            try
            {
                using (IDbConnection database = connect)
                {
                    Avtorize = database.QuerySingle<RegisterAtUser>("SELECT * FROM Avtorize WHERE ID = 10");
                }

            }
            catch
            {

            }

        }
    }
}
