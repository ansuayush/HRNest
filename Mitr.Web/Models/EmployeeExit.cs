using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mitr.Models
{
    public class EmployeeExit
    {
        public class SignatoryMaster
        {
            public long Id { get; set; }
            [Required(ErrorMessage = "Please Select Employee name")]
            public long EMPId { get; set; }
            public string EMPName { get; set; }
            [Required(ErrorMessage = "Hey! You missed this field")]
            public string StartDate { get; set; }
            public bool IsActive { set; get; }
            public int createdby { get; set; }
            public string createdat { get; set; }
            public int modifiedby { get; set; }
            public string modifiedat { get; set; }
            public string IPAddress { get; set; }
            public long? Attachmentid { get; set; }
            public string AttachmentPath { get; set; }
            public string filename { get; set; }
            public HttpPostedFileBase UploadFile { get; set; }
         

        }
        public class ResignationRequest
        {
            public long lId { get; set; }
            public long RlEmpid { get; set; }
            public long lEmpid { get; set; }
            public string sRequestno { get; set; }
            public string sRequestdate { get; set; }
            [Required(ErrorMessage = "Hey, You missed this field!")]
            public long lReasonid { get; set; }
            
            public string sComment { get; set; }
            public int iNoticePeriod { get; set; }
            
            public int iNoticePeriodServe { get; set; }
          
            public string sReleivingdate { get; set; }
            public string sNReleivingdate { get; set; }
            public string sReleivingdateM { get; set; }
            public string sReleivingdateHR { get; set; }
            public int iRelievingDay { get; set; }
            public string sReasonNoticePeriod { get; set; }
            public string sCompanyCode { get; set; }
            public int iStatusflag { get; set; }
            public int iDealernoc { get; set; }
            public string sStatus { get; set; }
            public string sStatusDNOC { get; set; }
            public List<NocList> lstNocList { get; set; }
            public int iHistoryCount { get; set; }
            public string sLatestStatus { get; set; }
            public string sDealerNOCReq { get; set; }
            public string sReasonNPM { get; set; }
            public string sCommentM { get; set; }
            public string sCommentR { get; set; }
            public string sEmpcodeL1 { get; set; }
            public string sEmpcodeL2 { get; set; }
            public string sEmpcodeL3 { get; set; }
            public string sEmpcodeL4 { get; set; }
            public string sEmpNameL1 { get; set; }
            public string sEmpNameL2 { get; set; }
            public string sEmpNameL3 { get; set; }
            public string sEmpNameL4 { get; set; }
            public string sCommentL3 { get; set; }
            public string sCommentL4 { get; set; }
            public string sCommentHR { get; set; }
            public string DeginationName { get; set; }
            public string DeptName { get; set; }
            public string LocationName { get; set; }
            public List<Attachments> lstAttachmentsFF { get; set; }
        }
        public class NocList
        {
            public long lId { get; set; }
            public long lNocId { get; set; }
            public long lEmpid { get; set; }
            public string sDepartment { get; set; }
            public string sEmpname { get; set; }
            public string sEmpcode { get; set; }
            public string sNoc { get; set; }
            public string sRemark { get; set; }
            public string sRemarkRequester { get; set; }
        }
        public class HRList
        {
            
             public int RowNum { get; set; }
            public long ID { get; set; }
            public long EMPID { get; set; }
            public string EMPName { get; set; }
            public string DeptName { get; set; }
            public string DegName { get; set; }
            public string LocationName { get; set; }
            public string EMPCode { get; set; }
            public string ReqDate { get; set; }
            public string Reldate { get; set; }
            public long Dayleft { get; set; }
        }
    }
}