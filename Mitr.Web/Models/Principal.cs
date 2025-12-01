using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Principal
    {
        public long ID { get; set; }
        public long ProjectId { get; set; }
        public string proj_name { get; set; }
        public string costcenter_name { get; set; }
        public long travel_id { get; set; }
        public long Approved { get; set; }
        public string TravelDetails { get; set; }
        public string traveller_name { get; set; }
        public List<Principal> listprincipal { get; set; }
       
    }
}