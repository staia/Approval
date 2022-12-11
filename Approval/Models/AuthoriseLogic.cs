using Dapper;
using System;
using System.Data;

namespace Approval.Models
{
    public static class AuthoriseLogic
    {
        public static BaseResponce<UserModel> Autorize(this UserModel user, IDbConnection connectUser)
        {
            BaseResponce<UserModel> baseResponce = new BaseResponce<UserModel>();
            baseResponce.Status = StatusCode.Ok;

            //UserModel autoUser=null;
            try
            {
                using (IDbConnection database = connectUser)
                {
                    user = database.QuerySingle<UserModel>("Select * From UserData Where Username = @UserName AND Password = @Password", user);
                }
            }
            catch(Exception ex)
            {
                baseResponce.Status = StatusCode.Error;
                baseResponce.ErrorMessage = "ex.Message";
            }
            baseResponce.Data = user;    
            //if(user == null)
            //{
            //    baseResponce.Status = StatusCode.NotFound;
            //    baseResponce.ErrorMessage = "Dont Exist";
            //}
            //user.Message = baseResponce.ErrorMessage;
            return baseResponce;
        }

    }
}
