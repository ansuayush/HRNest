using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class Qualification
    {
        public long QID { get; set; }
        public string Course { get; set; }
        public string University { get; set; }
        public string Location { get; set; }
        public string Year { get; set; }
        public string src { get; set; }
    }

    public class Flyer
    {
        public long ID { get; set; }
        public string AirlineName { get; set; }
        public string FlyerNumber { get; set; }
        public string src { get; set; }

    }
}