using System.ComponentModel.DataAnnotations;

namespace Mitr.Model.Compliance
{
    public class ComplianceMasterDocuments
    {
        [Required]
        public int ComplianceMasterID { get; set; }

        [Required]
        public string ActualFileName { get; set; }

        [Required]
        public string NewFileName { get; set; }

        [Required]
        public string FileURL { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Description { get; set; }

    }
}