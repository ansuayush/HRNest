using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model
{
   public class TeamLeadMaster: BaseModel
    {
        public int ProjectId { get; set; }
        public int EoUserId { get; set; }
        public int Id { get; set; }
        public int IsActive { get; set; }
        public string IPAddress { get; set; }
        
    }
}
