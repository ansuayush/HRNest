using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model
{
    public class SearchModel:BaseModel
    {
        
       public int AttachmentId { get; set; }
        public int Id { get; set; }
        public string SearchText { get; set; }
        public int Subcategory { get; set; }
        public int Category { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public List<ProjectSearch> ProjectList { get; set; }
        public List<TagSearch> TagSearchList { get; set; }
        public List<YearSearch> YearSearchList { get; set; }
        public List<ThematicSearch> ThematicSearchList { get; set; }
        public List<DonarSearch> DonarSearchList { get; set; }
        public List<LocationSearch> LocationSearchList { get; set; }

       


    }

    public class ProjectSearch
    {
        public int Id { get; set; }
    }
    public class TagSearch
    {
        public int Id { get; set; }
    }
    public class YearSearch
    {
        public int Id { get; set; }
    }
    public class ThematicSearch
    {
        public int Id { get; set; }
    }
    public class DonarSearch
    {
        public int Id { get; set; }
    }
    public class LocationSearch
    {
        public int Id { get; set; }
    }
    public class ContentApproval : BaseModel
    {
        public int Id { get; set; }
        public int ApprovarId { get; set; }
        public int RequesterId { get; set; }
        public bool IsRequester { get; set; }
        public int IsApproved { get; set; }
        public string IPAddress { get; set; }
        public string Reason { get; set; }
        public int RequestId { get; set; }
        
    }

    public class ShareContentReport
    {
      
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Category { get; set; }
        public string DocumentType { get; set; }
        public string SubCategory { get; set; }
        public string ProjectCode { get; set; }
        public string Location { get; set; }
        public string RequesterLocation { get; set; }

    }

}
