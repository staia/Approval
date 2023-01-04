using Dapper;
using System.Data;

namespace Approval.Models
{
    public static class AvtorizeLogic
    {
        public static BaseResponce<RegisterAtUserModel> UserModelRegister(this RegisterAtUserModel userdata, IDbConnection connect) //this вызывает Autorize
        {
            BaseResponce<RegisterAtUserModel> baseResponce = new BaseResponce<RegisterAtUserModel>();
            baseResponce.Status = StatusCode.Ok;
            baseResponce.ErrorMessage = "Data access";     

            try
            {
                using (IDbConnection database = connect)       
                {
                    baseResponce.Data = database.QuerySingle<RegisterAtUserModel>("SELECT * FROM Avtorize WHERE UserName = @UserName AND EnterPassword = @EnterPassword", userdata);
                }
            }
            catch(System.Exception ex)
            {
                baseResponce.Status = StatusCode.Error;
                baseResponce.ErrorMessage = ex.Message;
            }

            return baseResponce;

        }
    }
}
