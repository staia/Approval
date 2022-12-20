using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Approval.Enums
{
    public enum CategoryEnums
    {
        [Display(Name = "Soft-/Hardware")]
        Hardware,

        Furniture,

        [Display(Name = "etc.")]
        etc
    }
}
