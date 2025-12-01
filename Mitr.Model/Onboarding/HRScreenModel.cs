using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model
{
    public class HRScreenModel:BaseModel
    {
        public int Id { get; set; }
        public int IsGrid { get; set; }
        public string Source { get; set; }
        public int ManagerId { get; set; }
        public string RequestNo { get; set; }
        public int ProjectNo { get; set; }
        public DateTime RequestDate { get; set; }
        public string JobTitle { get; set; }
        public string JobDesc { get; set; }
        public int Thematicarea { get; set; }
        public int Noofvacancy { get; set; }
        public int LocationId { get; set; }
        public string Qilification { get; set; }

    }
}
