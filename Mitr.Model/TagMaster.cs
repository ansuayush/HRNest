using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model
{
  public  class TagMaster: BaseModel
    {
        public int Id { get; set; }
        public int Thematic_id { get; set; }
        public string Tag { get; set; }
        public int IsActive { get; set; }
        public string IPAdress { get; set; }
        public string SharedUserId { get; set; }

    }
}
