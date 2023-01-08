using Approval.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Configuration;
using Dapper;

namespace Approval.Models
{
    public class ListOrder
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }
        public CategoryEnums Category { get; set; }
        public string GoalOfProcurement { get; set; }
        public double UnitPrice { get; set; }
        public DeliveryEnum PlaceOfDelivery { get; set; }
        public int Amount { get; set; }
        public string Attachment { get; set; }
        public double Price { get; set; }
        public DateTime DesireDate { get; set; }
        public string PurchasingNotes { get; set; }
        public string CreatedBy { get; set; }
        public string HeadOfDepartment { get; set; }
        public string GeneralManager { get; set; }
        public string HeadOfIT { get; set; }
        public string NumberFromERP { get; set; }
        public string WarehouseManager { get; set; }
    } 
}
