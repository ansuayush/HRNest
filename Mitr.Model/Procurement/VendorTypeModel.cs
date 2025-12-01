using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model
{
  public  class VendorTypeModel: BaseModel
    {
        public int ID { get; set; }        
        public string VendorType { get; set; }
        public string Status { get; set; }
        public int TableType { get; set; }
        public string Code { get; set; }
        public int IsGrid { get; set; }
        public int MasterTableId { get; set; }         
        public int IsActive { get; set; }
        public string IPAddress { get; set; }
    }
}
