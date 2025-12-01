using System.ComponentModel.DataAnnotations;

namespace Mitr.Model.Compliance
{
    public class ComplianceMasterConditions
    {
        [Required]
        public int ComplianceMasterID { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Condition { get; set; }

    }
}