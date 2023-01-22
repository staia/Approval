using Approval.Interfaces;
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
		IBotApproval Bot { get; set; }
		public OrderServices(DbConnection connection, IBotApproval bot)
		{
			Database = connection;
			Bot = bot;
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
                Bot.SendApproveOrder(840338962, "New Order #" + order.ID + " / " + order.Title);
				//Bot.SendMessageToUser("New Order #" + order.ID + " / " + order.Title);
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


        public void DeleteItem(int idRequest)
        {
            using (IDbConnection database = Database.Conneсt)
            {
                database.Execute("Delete from ListOrders where Id = @Id", new
                {
                    Id = idRequest,
                });
            }
        }
    }
}
