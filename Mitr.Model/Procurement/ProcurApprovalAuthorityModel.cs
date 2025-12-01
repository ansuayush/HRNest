using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Procurement
{
    public class ProcurApprovalAuthorityModel:BaseModel
    {
        public int ID { get; set; }

        public int ApproverAuthId { get; set; }
        public string EffectiveDate { get; set; }  
        public string Code { get; set; }
        public int IsGrid { get; set; }
        public int MasterTableId { get; set; }
        public int IsActive { get; set; }
        public string IPAddress { get; set; }
    }

   public class ApproveRejectModel
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }      


    }
}
