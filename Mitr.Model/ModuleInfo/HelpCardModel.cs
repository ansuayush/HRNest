using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.ModuleInfo
{
    public class HelpCardModel
    {
        public int ID { get; set; }
        public int TopicCount { get; set; }
        public string ModuleName { get; set; }

    }
    public class HelpCardTopicModel
    {
        public int ID { get; set; }
          public int Topic_id { get; set; }
        public int ModuleId { get; set; }
        public int PageCount { get; set; }
     
        public string TopicName { get; set; }
    }

    public class HelpCardPAGEModel
    {
        public int ID { get; set; }
        public int Topic_id { get; set; }
        public int Module_id { get; set; }
        public string TopicName { get; set; }
        public string PageName { get; set; }
        public string Video_link { get; set; }
        public string Description { get; set; }
         public string ModuleName { get; set; }
    }
}
