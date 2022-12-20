using System.ComponentModel.DataAnnotations;

namespace Approval.Enums
{
    public enum DeliveryEnum
    {
        Chicago,

        [Display(Name = "San Francisco")]
        SanFrancisco
    }
}
