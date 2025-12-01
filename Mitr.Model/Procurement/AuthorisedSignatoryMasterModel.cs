using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model.Procurement
{
    public class AuthorisedSignatoryMasterModel
    {
        public int ID { get; set; }
        public string ContractType { get; set; }
        public int EmpId { get; set; }
        public int Action { get; set; }

        public bool IsActive { get; set; }
        public string IPAddress { get; set; }

    }
    public class TermTemplate
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string IPAddress { get; set; }
        public string ContractType { get; set; }
        public int ProcureId { get; set; }
       


    }
}
