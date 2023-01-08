using Approval.Models;
using Dapper;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Approval.Services
{
    public  class AuthoriseLogic
    {
        DbConnection Database { get; set; }
        public AuthoriseLogic(DbConnection connection)
        {
            Database = connection;
        }
        public  async Task<BaseResponce<RegisterAtUserModel>> AutorizeAsync(AutorizeUserModel user)
        {
            BaseResponce<RegisterAtUserModel> baseResponce = new BaseResponce<RegisterAtUserModel>();
            baseResponce.Status = StatusCode.Ok;
        
            try
            {
                using (IDbConnection database = Database.Conneсt)
                {
                    baseResponce.Data = await database.QuerySingleAsync<RegisterAtUserModel>("Select * From Avtorize Where Username = @UserName AND EnterPassword = @EnterPassword", user);
                }
            }
            catch (Exception ex)
            {
                baseResponce.Status = StatusCode.Error;
                baseResponce.ErrorMessage = ex.Message;
            }

            return baseResponce;
        }

        public  BaseResponce<RegisterAtUserModel> UserModelRegister(RegisterAtUserModel userdata) 
        {
            BaseResponce<RegisterAtUserModel> baseResponce = new BaseResponce<RegisterAtUserModel>();
            baseResponce.Status = StatusCode.Ok;
            baseResponce.ErrorMessage = "Data accses";

            try
            {
                using (IDbConnection database = Database.Conneсt)
                {
                    var user = database.QueryFirstOrDefault<RegisterAtUserModel>("SELECT * FROM Avtorize WHERE Email = @Email", userdata);
                    if (user != null)
                    {
                        throw new Exception("User Exist");
                    }
                    userdata.IdUser = Guid.NewGuid();

                    database.Execute("INSERT INTO Tab VALUES(@UserName, @Password)");
                }
            }
            catch (Exception ex)
            {
                baseResponce.Status = StatusCode.Error;
                baseResponce.ErrorMessage = ex.Message;
            }

            return baseResponce;

        }

    }
}
