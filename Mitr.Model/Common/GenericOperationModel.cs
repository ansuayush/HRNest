using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mitr.Model.Common
{
    public class GenericOperationModel
    {        
        public String ScreenID { get; set; }
        public int UserID { get; set; }
        public int RoleId { get; set; }
        public String Operation { get; set; }         
        public String ModelData { get; set; }
        public Rows Rows { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }


    }

    [XmlRoot("root")]
    public class Rows
    {
        public GenericOperationModelData[] Data { get; set; }
    }


    public class GenericOperationModelData
    {
        public int RowIndex { get; set; }
        public string KeyName { get; set; }
        public string ValueData { get; set; }
    }

}
