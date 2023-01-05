using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using System.Collections;
using Dapper;

namespace Approval.Models
{
    public class PageData
    {
        public List<ListOrder> ListOrders { get; set; }
        public PageData()
        {

        }
        public PageData(IDbConnection connect)
        {
            ListOrders = new List<ListOrder>();

            using(IDbConnection database = connect)
            {
                ListOrders = database.Query<ListOrder>("SELECT ID, Title, PRice, Status, Created, CreatedBy,  NumberFromERP FROM ListOrders").ToList();
            }
        }
    }
}
