using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Capacity
{
    public class TrainingCalenderModel:BaseModel
    {
        public int id { get; set; }
        public int TrainingTypeid { get; set; }
        public string TrainingDesc { get; set; }
        public string TrainingName { get; set; }
        public DateTime TentativeFromDate { get; set; }
        public DateTime TentativeToDate { get; set; }
        public string TrainingMode { get; set; }
        public int TrainingModeID { get; set; }
        public string TraininglinkorLocation { get; set; }
        public int IsActive { get; set; }


    }
}
