using Approval.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Approval.Services
{
    public class OrderServices
    {
        DbConnection Database { get; set; }
        public OrderServices(DbConnection connection)
        {
            Database= connection;
        }
        public BaseResponce<bool> CreateOrder(ListOrder order)
        {
            BaseResponce<bool> responce = new BaseResponce<bool>();
            responce.Status = StatusCode.Ok;
            responce.ErrorMessage = "OK";


            try
            {
                //write to DB
                using (IDbConnection database = Database.Conneсt)
                {
                    //order.Amount
                    database.Execute("INSERT INTO ListOrders ( Title, Category, GoalOfProcurement, UnitPrice, PlaceOfDelivery, Amount, Attachment, Price, DesireDate, PurchasingNotes, Created, HeadOfDepartment, GeneralManager, HeadOfIT, NumberFromERP) VALUES" +
                        "(@Title, @Category, @GoalOfProcurement, @UnitPrice, @PlaceOfDelivery, @Amount, @Attachment, @Price, @DesireDate, @PurchasingNotes, GetDate(), @HeadOfDepartment, @GeneralManager, @HeadOfIT, @NumberFromERP)", order);
                }
            }
            catch (System.Exception ex)
            {
                responce.Status = StatusCode.Error;
                responce.ErrorMessage = ex.Message;
            }
            return responce;
        }

        public ListOrder GetOrder(int idOrder)
        {
            ListOrder order = new ListOrder();
            using (IDbConnection database = Database.Conneсt)
            {
                order = database.QueryFirstOrDefault<ListOrder>("SELECT * FROM ListOrders WHERE Id = @Id", new { Id = idOrder });
            }
            return order;
        }

        public List<ListOrder> Search(string find,  string category = "all", string role = "user")
        {
            List<ListOrder> result = new List<ListOrder>();
            using(IDbConnection database = Database.Conneсt)
            {
                result= database.Query<ListOrder>("SELECT * FROM ListOrders " +
                    "WHERE Title LIKE @Search", new { Search = find + "%"}).ToList();
            }
            return result;
        }
    
    }
}
