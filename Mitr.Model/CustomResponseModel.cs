using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model
{
    public class CustomResponseModel
    {
        public bool IsSuccessStatusCode { get; set; }
        public String ErrorMessage { get; set; }
        public String CustomMessage { get; set; }
        public DataSet data { get; set; }
        public int ValidationInput { get; set; }
        public List<DropdownModel> CommomDropDownData { get; set; }
        public string CustumException { get; set; }
        public FileModel FileModel { get; set; }
        public String HtmlView { get; set; }

    }
    public class FileModel
    {
        
        public string ActualFileName { get; set; }
        public string NewFileName { get; set; }
        public string FileSize { get; set; }
        public string FileUrl { get; set; }
        public string AttachmentType { get; set; }
        public string FileName { get; set; }
        public int Id { get; set; }


    }
}
  