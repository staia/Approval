using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace Approval.Models
{
    public enum Status
    {
        Create,
        HeadOfDepartment,
        HeadOfITDepartment,
        GeneralManager,
        Orded
    }

    public class ListOrder
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string Category { get; set; }
        public string NumberFromERP { get; set; }
    }
    public class AnRegisterUser : RegisterAtUser
    { 
        public string Email { get; set; }

    public class OrderCreate
    {
        [Required]
        public string Title { get; set; }
        public string Category { get; set; }    
        public string GoalOfProcurement { get; set; }
        public double UnitPrice { get; set; }
        public string PlaceOfDelivery { get; set; } 
        public int Amount { get; set; }  
        public string Attachment {get; set;}
        public double Price { get; set; }         
        public DateTime DesireDate { get; set; }
        public string PurchasingNotes { get; set; }
        public string CreatedBy { get; set; }
        public string HeadOfDepartment { get; set; }
        public string GeneralManager { get; set; }
        public string HeadOfIT { get; set; }
        public string NumberFromERP { get; set; }
    }


}
