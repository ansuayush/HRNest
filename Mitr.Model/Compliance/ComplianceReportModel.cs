using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Compliance
{
    public class ComplianceReportModel
    {
        public int RowNumber { get; set; }
        public int SubCategoryId { get; set; }
        public int FYId { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Compliance { get; set; }
        public string FrequencyDesc { get; set; }
        public string Primary_User { get; set; }

        // Due Dates and Actual Dates for up to 12 months
        public DateTime? Duedate_1 { get; set; }
        public DateTime? ActualDate_1 { get; set; }
        public string ActualFileName_1 { get; set; }
        public string NewFileName_1 { get; set; }
        public string FileURL_1 { get; set; }

        public DateTime? Duedate_2 { get; set; }
        public DateTime? ActualDate_2 { get; set; }
        public string ActualFileName_2 { get; set; }
        public string NewFileName_2 { get; set; }
        public string FileURL_2 { get; set; }

        public DateTime? Duedate_3 { get; set; }
        public DateTime? ActualDate_3 { get; set; }
        public string ActualFileName_3 { get; set; }
        public string NewFileName_3 { get; set; }
        public string FileURL_3 { get; set; }

        public DateTime? Duedate_4 { get; set; }
        public DateTime? ActualDate_4 { get; set; }
        public string ActualFileName_4 { get; set; }
        public string NewFileName_4 { get; set; }
        public string FileURL_4 { get; set; }

        public DateTime? Duedate_5 { get; set; }
        public DateTime? ActualDate_5 { get; set; }
        public string ActualFileName_5 { get; set; }
        public string NewFileName_5 { get; set; }
        public string FileURL_5 { get; set; }

        public DateTime? Duedate_6 { get; set; }
        public DateTime? ActualDate_6 { get; set; }
        public string ActualFileName_6 { get; set; }
        public string NewFileName_6 { get; set; }
        public string FileURL_6 { get; set; }

        public DateTime? Duedate_7 { get; set; }
        public DateTime? ActualDate_7 { get; set; }
        public string ActualFileName_7 { get; set; }
        public string NewFileName_7 { get; set; }
        public string FileURL_7 { get; set; }

        public DateTime? Duedate_8 { get; set; }
        public DateTime? ActualDate_8 { get; set; }
        public string ActualFileName_8 { get; set; }
        public string NewFileName_8 { get; set; }
        public string FileURL_8 { get; set; }

        public DateTime? Duedate_9 { get; set; }
        public DateTime? ActualDate_9 { get; set; }
        public string ActualFileName_9 { get; set; }
        public string NewFileName_9 { get; set; }
        public string FileURL_9 { get; set; }

        public DateTime? Duedate_10 { get; set; }
        public DateTime? ActualDate_10 { get; set; }
        public string ActualFileName_10 { get; set; }
        public string NewFileName_10 { get; set; }
        public string FileURL_10 { get; set; }

        public DateTime? Duedate_11 { get; set; }
        public DateTime? ActualDate_11 { get; set; }
        public string ActualFileName_11 { get; set; }
        public string NewFileName_11 { get; set; }
        public string FileURL_11 { get; set; }

        public DateTime? Duedate_12 { get; set; }
        public DateTime? ActualDate_12 { get; set; }
        public string ActualFileName_12 { get; set; }
        public string NewFileName_12 { get; set; }
        public string FileURL_12 { get; set; }
    }
}
