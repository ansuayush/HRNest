namespace Mitr.Model
{
    public class GetResponseModel
    {
        public long ID { get; set; }
        public long AdditionalID { get; set; }
        public long AdditionalID1 { get; set; }
        public string MultipleLoginID { get; set; }
        public int Approve { get; set; }
        public string Doctype { get; set; }
        public long LoginID { get; set; }
        public string IPAddress { get; set; }
        //public string TableName { get; set; }
        //public string IsActive { get; set; }
    }
    public class PostResponseModel
    {
        public string ViewAsString { get; set; }
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string SuccessMessage { get; set; }
        public string RedirectURL { get; set; }
        public long ID { get; set; }
        public long OtherID { get; set; }
        public string AdditionalMessage { get; set; }
    }
    
}