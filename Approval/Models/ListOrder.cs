using System;

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
}
