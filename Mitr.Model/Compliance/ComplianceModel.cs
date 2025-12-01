using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Compliance
{
    public class ComplianceModel
    { 
        public ComplianceMasterModel ComplianceMasterModel { get; set;}

        public List<ComplianceMasterConditions> ComplianceMasterConditionsList { get; set;}

        public List<ComplianceMasterSubTask> complianceMasterSubTaskList { get; set;}

        public List<ComplianceMasterDocuments> complianceMasterDocumentsList { get; set;}

        public RemarkModel remarkModel { get; set;}

    }
}
