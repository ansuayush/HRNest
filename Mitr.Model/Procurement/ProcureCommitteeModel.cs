using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Procurement
{
    public class ProcureCommitteeModel:BaseModel
    {
        public int Id { get; set; }
        public int IsGrid { get; set; }
        public int  IsDelete { get; set; }        
        public int  MinProcureValue { get; set; }
        public int  MaxProcureValue { get; set; }
        public string EffectiveDate { get; set; }
        public List<ProcureCommitteeMemberModel> ProcureCommitteeMemberList { get; set; }
        public List<ProcureCommitteeLocation> ProcureCommitteeLocationList { get; set; }

        public List<ProcureCommitteeData> ProcureCommitteeDataList { get; set; }
        public string LocationData { get; set; }

    }
    public class ProcureCommitteeMemberModel
    {
        public int ProcureCommitteeId { get; set; }
        public int CommitteeMember { get; set; }      
        public string TenureStartDate { get; set; }
        public string TenureEndDate { get; set; }
         

    }

   public class ProcureCommitteeData
    {
        public int MinProcureValue { get; set; }
        public int MaxProcureValue { get; set; }
        public int LocationId { get; set; }
        public int MemberId { get; set; }
        
    }
    public class ProcureCommitteeLocation
    {      
        public int LocationId { get; set; }
        public string Remark { get; set; }

    }

    
}
