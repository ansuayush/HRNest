using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Mitr.Models.AllEnum;

namespace Mitr.Models
{

    public class Donor
    {
        public class List
        {
            public long ID { set; get; }
            public string SourceType { get; set; }
            public long DonorTypeID { get; set; }
            public string DonorTypeName { get; set; }
            public string donor_name { get; set; }
            public string website { get; set; }
            public string ContactName { get; set; }
            public string Phone { get; set; }
            public int Priority { get; set; }
            public string createdby { get; set; }
            public string createdat { get; set; }
            public string modifiedby { get; set; }
            public string modifiedat { get; set; }
            public bool IsActive { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public long? ID { set; get; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public long DonorTypeID { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public string donor_name { get; set; }
            public long address_id { get; set; }
            public string website { get; set; }
            public bool IsActive { set; get; }
            public int? Priority { set; get; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
            public List<DropDownList> DonorTypeList { get; set; }
            public DonarAddress LocalAddress { get; set; }
            public DonarAddress PermanentAddress { get; set; }
            public List<DonorDetails> DonorDetailsList { get; set; }
            public Nationality? SourceType { get; set; }

            public List<DropDownList> CountryList { get; set; }
            public List<DropDownList> StateList { get; set; }
            public List<DropDownList> CityList { get; set; }

        }


        public class View
        {
            public long? ID { set; get; }
            public string DonorType { get; set; }
            public string donor_name { get; set; }
            public string website { get; set; }
           public string SourceType { get; set; }
            public DonarAddress LocalAddress { get; set; }
            public DonarAddress PermanentAddress { get; set; }
            public List<DonorDetails> DonorDetailsList { get; set; }
        }


        public class DonorDetails
        {
            public long? ID { get; set; }
            public long donor_id { set; get; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public string person_name { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public string designation { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field.")]
            public string location { get; set; }

            public string phone_no { get; set; }

            [EmailAddress]
            public string email { get; set; }
            public int? Priority { get; set; }
            public int? Isdefault { get; set; }
            public long LoginID { get; set; }
            public string IPAddress { get; set; }
        }

    }

}