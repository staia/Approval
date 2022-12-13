using Approval.Models;
using Dapper;
using System.Data;

namespace Approval.Services
{
    public static class OrderServices
    {
        public static BaseResponce<bool> CreateOrder(this OrderCreate order, IDbConnection connect)
        {
            BaseResponce<bool> responce = new BaseResponce<bool>();
            responce.Status = StatusCode.Ok;
            responce.ErrorMessage = "OK";


            try
            {
                //write to DB
                using (IDbConnection database = connect)
                {
                    //order.Amount
                    database.Execute("INSERT INTO ListOrders ( Title, Category, GoalOfProcurement, UnitPrice, PlaceOfDelivery, Amount, Attachment, Price, DesireDate, PurchasingNotes, Created, HeadOfDepartment, GeneralManager, HeadOfIT, NumberFromERP) VALUES" +
                        "(@Title, @Category, @GoalOfProcurement, @UnitPrice, @PlaceOfDelivery, @Amount, @Attachment, @Price, @DesireDate, @PurchasingNotes, GetDate(), @HeadOfDepartment, @GeneralManager, @HeadOfIT, @NumberFromERP)", order);
                }
            }
            catch (System.Exception ex)
            {
                responce.Status = StatusCode.Error;
                responce.ErrorMessage = ex.Message ;
            }
             return responce;
        }
    }
}
