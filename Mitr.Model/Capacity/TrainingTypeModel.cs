using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Capacity
{
    public class TrainingTypeModel:BaseModel
    {
        public int IsGrid { get; set; }
        public int InputData { get; set; }
        public int id { get; set; }
        public string TrainingTypeName { get; set; }
        public string TrainingDesc { get; set; }
        public string ApplicableToId { get; set; }
        public string ApplicableDesignationId { get; set; }
        public string SkillId { get; set; }
        public bool SupervisorAssessmentReq { get; set; }
        public bool DeclarationReqforAttendies { get; set; }
        public bool BondReq { get; set; }
        public string ActualFileName { get; set; }
        public string NewFileName { get; set; }
        public string AttachmentPath { get; set; }

        public string ApplicableToCategory { get; set; }
        public string DesignName { get; set; }

        public int IsActive { get; set; }


    }
}
