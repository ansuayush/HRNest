using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Country
    {
        public long CountryID { set; get; }
        [Required(ErrorMessage = "Country Name Can't Blank")]
        public string CountryName { get; set; }
        
        public string Description { get; set; }
        public int CountryHours { get; set; }
        public int CountryMinutes { get; set; }
        public bool IsActive { set; get; }
        public int Priority { set; get; }
        public int createdby { get; set; }
        public string createdat { get; set; }
        public int modifiedby { get; set; }
        public string modifiedat { get; set; }
        public int deletedby { get; set; }
        public string deletedat { get; set; }
        public int isdeleted { get; set; }
        public string IPAddress { get; set; }
    }
    public class City
    {
        public long CityID { set; get; }
        [Required(ErrorMessage = "City Can't Blank")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "State Can't Blank")]
        public long StateID { set; get; }
        public string StateName { get; set; }
        [Required(ErrorMessage = "Category Can't Blank")]
        public string Category { set; get; }
      
        public string Description { get; set; }

        public bool IsActive { set; get; }
        public int Priority { set; get; }
        public int createdby { get; set; }
        public string createdat { get; set; }
        public int modifiedby { get; set; }
        public string modifiedat { get; set; }
        public int deletedby { get; set; }
        public string deletedat { get; set; }
        public int isdeleted { get; set; }
        public string IPAddress { get; set; }
    }
    public class State
    {
        public long StateID { set; get; }
        [Required(ErrorMessage = "Name Can't Blank")]
        public string StateName { get; set; }
        [Required(ErrorMessage = "Country Can't Blank")]
        public long CountryID { get; set; }
        public string CountryName { get; set; }
        public string Description { get; set; }
        public bool IsActive { set; get; }
        public int Priority { set; get; }
        public int createdby { get; set; }
        public string createdat { get; set; }
        public int modifiedby { get; set; }
        public string modifiedat { get; set; }
        public int deletedby { get; set; }
        public string deletedat { get; set; }
        public int isdeleted { get; set; }
        public string IPAddress { get; set; }
    }
}