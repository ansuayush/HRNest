using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitr.Model
{
  public  class ContentModel: BaseModel
	{
		public int Id { get; set; }
		public int Req_No { get; set; }
		public DateTime Req_Date { get; set; }
		public string Req_By { get; set; }
		public int CategoryId { get; set; }
		public int Sub_CategoryId { get; set; }
		public int Project_CodeId { get; set; }
		public string PlaceId { get; set; }
		public string [] PlaceIds { get; set; }
		public DateTime Upload_Date { get; set; }
		public string Report_No { get; set; }
		public string Author_CoordinatorId { get; set; }
		public string [] Author_Id { get; set; }
		public string Title { get; set; }
		public string Sub_Title { get; set; }
		public string Tag_Id { get; set; }
		public string Abstract_Summary { get; set; }
		public string Remark { get; set; }
		public string Document_Category { get; set; }
		public string Published { get; set; }
		public string Document_ID { get; set; }
		public string Proposal_No { get; set; }
		public string Accepted { get; set; }
		public string Copyright { get; set; }
		public string Contract_No { get; set; }
		public string Party_Name { get; set; }
		public string Effective_Date { get; set; }
		public string Expiry_RenewableDate { get; set; }
		public string Source { get; set; }
		public string Stage_Level { get; set; }
		public string Status { get; set; }
		public int Createdby { get; set; }
		public int Modifiedby { get; set; }
		public int Deletedby { get; set; }
		public DateTime Createdat { get; set; }
		public DateTime Modifiedat { get; set; }
		public DateTime Deletedat { get; set; }
		public bool Isdeleted { get; set; }
		public bool IsActive { get; set; }
		public string IPAddress { get; set; }
        public List<FileModel> DocumentList { get; set; }
		public string [] Tags { get; set; }
        public string Reason { get; set; }
        public int ContentStatus { get; set; }
        public int ApproverId { get; set; }        
        public int ForwaredTo { get; set; }
        public int ProjectLead { get; set; }
        public int IsSubmit { get; set; }
        public string ApprovarAuth { get; set; }
 

    }
}
