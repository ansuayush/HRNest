using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class OfficeListing
    {

        public long Id { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public string BranchOffice { set; get; }
        public string Description { set; get; }


        [Required(ErrorMessage = "Hey! You missed this field.")]
        public string LocationIds { set; get; }

        public string Location { set; get; }

   
        public bool IsActive { set; get; }
        public int Priority { set; get; }
        public int createdby { get; set; }
        public string createdat { get; set; }
        public int modifiedby { get; set; }
        public string modifiedat { get; set; }
        public string IPAddress { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public long Admin { get; set; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public long IT { set; get; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public long HR { set; get; }
        [Required(ErrorMessage = "Hey! You missed this field")]
        public long Finance { set; get; }
        public string LOcId { get; set; }
        public List <OfficeListing> officeListings { get; set; }
    }
    
}