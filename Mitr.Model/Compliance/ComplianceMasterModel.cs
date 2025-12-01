using System;


namespace Mitr.Model.Compliance
{
    public class ComplianceMasterModel
    {

        public int id { get; set; }
        public int YearId { get; set; }

        public decimal Doc_No { get; set; }


        public DateTime DocDate { get; set; }

        public int SubCategary { get; set; }


        public int Categary { get; set; }


        public int ComplianceType { get; set; }

        public string SubSubCategary { get; set; }

        public int RiskType { get; set; }

        public string ComplianceName { get; set; }

        public int Frequency { get; set; }

        public int? DayOF { get; set; }

        public string FrequencyCont { get; set; }

        public int? LeadDay { get; set; }

        public int? LeadHour { get; set; }

        public int? LeadMin { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool IsApproval { get; set; }
        public bool IsEscalation { get; set; }

        public int Doer { get; set; }

        public int Department { get; set; }

        public int? SecDoer { get; set; }

        public int ProcessController1 { get; set; }

        public int ProcessController2 { get; set; }

        public bool IsChecklist { get; set; }


        public int ClosureType { get; set; }

        public string EscalateTo { get; set; }


        public int EscalationTime { get; set; }


        public int ReminderTime { get; set; }

     
        public int Type { get; set; }

        public bool? isDeleted { get; set; }

        public bool? isActive { get; set; }

        public int Version { get; set; }

        public DateTime ? DueDate { get; set; }
    }
}
