using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.ModuleInfo
{
    public class ModuleinfoTopicModel
    {
        public int ID { get; set; }

        public int ModuleId { get; set; }
        public string TopicName { get; set; }
        public int isActive { get; set; }
        public int Priority { get; set; }
        public string IPAddress { get; set; }

    }
}
