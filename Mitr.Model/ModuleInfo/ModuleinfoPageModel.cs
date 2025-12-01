using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.ModuleInfo
{
    public class ModuleinfoPageModel
    {
        public int ID { get; set; }
        public int Module_id { get; set; }
        public int Topic_id { get; set; }
        public string PageName { get; set; }
        public string Video_link { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int IsActive { get; set; }
        public string IPAddress { get; set; }
    }
}
