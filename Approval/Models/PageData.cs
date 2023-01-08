using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using System.Collections;
using Dapper;
using Approval.Services;

namespace Approval.Models
{
    public class PageData
    {
        public List<ListOrder> ListOrders { get; set; }
        public DbConnection Database { get; set; }
        public PageData(DbConnection connect)
        {
            Database = connect;
        }

    }
}
