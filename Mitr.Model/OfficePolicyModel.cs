using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model
{
    public class OfficePolicyModel
    {
        public int ID { get; set; }
        public string DocumentName { get; set; }
        public int Priority { get; set; }
        public int IsActive { get; set; }
        public string ActualFileName { get; set; }
        public string NewFileName { get; set; }
        public string FileUrl { get; set; }

    }
   
}  
