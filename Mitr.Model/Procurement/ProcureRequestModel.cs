using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Procurement
{
    public class ProcureRequestModel : BaseModel
    {
        public bool IsMoreThanFiveLakh { get; set; }
        public string Requiredjustificationforwaiver { get; set; }
        public bool IsFollowRFPRFQ { get; set; }
        public int IsResubmit { get; set; }
        public bool FromAsset { get; set; }

        public string Reason { get; set; }
        public int Status { get; set; }
        public int ProcureId { get; set; }
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public string ContractType { get; set; }
        public int POC { get; set; }
        public string BudgetAmount { get; set; }
        public string RequiredAmount { get; set; }
        
         
        public List<ProcureRequestProjectDetailModel> ProcureRequestProjectDetailList { get; set; }

    }
    public class ProcureRequestProjectDetailModel
    {
        public string ProjectSubLineItem { get; set; }
        public int ProjectCode { get; set; }
        public string ProjectLineDesc { get; set; }
        public int Qty { get; set; }
        public string BudgetAmount { get; set; }
        public string RequiredAmount { get; set; }
        public int ProjectManager { get; set; }
        public string Remark { get; set; }
        public string TotalValue { get; set; }
        public int LineId { get; set; }

        public int SourceData { get; set; }
    }

    public class AmendmentProcess
    {
        public int Procure_Request_Id { get; set; }
        public float Req_No { get; set; }
        public string NatureOfAmemdment { get; set; }
        public string Remark { get; set; }
        public int ContractType { get; set; }
        
    }
    public class AmendmentProcurementData
    {
        public int Id { get; set; }
        public int RequiredAmount { get; set; }       
        public string Remark { get; set; }        

    }
}
