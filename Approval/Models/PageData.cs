﻿using System.Collections.Generic;
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

        public PageData(IDbConnection connect)
        {
            ListOrders = new List<ListOrder>();

            using(IDbConnection database = Database.Conneсt)
            {
                ListOrders = database.Query<ListOrder>("SELECT * FROM ListOrders").ToList();
            }
        }
    }
}
