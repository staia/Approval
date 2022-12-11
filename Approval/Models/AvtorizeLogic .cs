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
            baseResponce.ErrorMessage = "Data accses";     

            try
            {
                using (IDbConnection database = connect)       
                {
                    baseResponce.Date = database.QuerySingle<RegisterAtUserModel>("SELECT * FROM Avtorize WHERE UserName = @UserName AND EnterPassword = @EnterPassword", userdata);
                }
            }
            catch(System.Exception ex)
            {
                baseResponce.Status = StatusCode.Error;
                baseResponce.ErrorMessage = ex.Message;
            }

            return baseResponce;

            //RegisterAtUserModel avtoUser;
            //using (IDbConnection database = connect)
            //{
            //    avtoUser = database.QuerySingle<RegisterAtUserModel>("SELECT * FROM Avtorize WHERE UserName = @UserName AND Email = @Email AND EnterPassword = @Password AND [Role] = @Role AND ConfirmPassword = @ConfirmPassword ", userdata);
            //}
            //if (avtoUser == null)
            //{
            //    baseResponce.Status = StatusCode.NotFound;
            //    baseResponce.ErrorMessege = "Нет такого пользователя";
            //}
            //  return baseResponce;
        }
    }
}
