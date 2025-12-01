using System;

namespace Mitr.Model.Compliance
{
    public class ComplianceMasterSubTask
    {

        public int Id { get; set; }
        public int ComplianceMasterID { get; set; }

        public string SubtaskName { get; set; }


        public string SubTaskType { get; set; }


        public bool IsMandatory { get; set; }
        public string SubTaskResponse1 { get; set; }
        public string SubTaskResponse2 { get; set; }
        public string SubTaskResponse3 { get; set; }
        public string Remark { get; set; }
        public bool IsChecked { get; set; }
        

    }
}