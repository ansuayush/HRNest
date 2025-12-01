using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model
{
    public class OnboardingStatus: BaseModel
    {
        public int Id { get; set; }
        public int IsGrid { get; set; }
        public int RowNum { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
}
