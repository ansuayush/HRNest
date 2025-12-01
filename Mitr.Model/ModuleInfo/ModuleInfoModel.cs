using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.ModuleInfo
{
    public class ModuleInfoModel
    {
        public int ID { get; set; }
        
        public string ModuleName { get; set; }
        public int IsActive { get; set; }
        public int Priority { get; set; }
        public string IPAddress { get; set; }
       
    }
}
