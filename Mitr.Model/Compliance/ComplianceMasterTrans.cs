using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Compliance
{
    public class ComplianceMasterTransMoved
    {
        public int Id { get; set; }
    }
        public class ComplianceMasterTrans
    {
        public int Id { get; set; }
        public int IsSubmit { get; set; }
        public int FromAction { get; set; }
        public int ActionStatus { get; set; }
        public string Remark { get; set; }
        public int IsUpdated { get; set; }
        
        public List<ComplianceMasterSubTask> ComplianceMasterSubTaskList { get; set; }
        public List<ComplianceTranAttachment> ComplianceTranAttachmentList { get; set; }

    }
    public class ComplianceTranAttachment
    {
        public int Id { get; set; }
        public int Compliance_TransID { get; set; }
        public string Description { get; set; }
        public string ActualFileName { get; set; }
        public string NewFileName { get; set; }
        public string FileURL { get; set; }         

    }
}
