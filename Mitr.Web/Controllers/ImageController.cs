using Mitr.CommonClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mitr.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Download(long AttachmentID)
        {
            string path = clsApplicationSetting.GetPhysicalPath("Attachments") + "\\";
            string Name = "", content_type = "";
            try
            {
                if (AttachmentID != 0)
                {
                    DataSet ds = new DataSet();
                    ds = Common_SPU.fnGetAttachmentList(AttachmentID, "", "");
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        Name = item["filename"].ToString();
                        content_type = item["content_type"].ToString();
                    }

                }
                Name = (string.IsNullOrEmpty(Name) ? AttachmentID.ToString() : Name);
                path = path + AttachmentID + content_type;
                if (System.IO.File.Exists(path))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                    string fileName = Name + content_type;
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }

        public ActionResult DownloadFile(string AttachmentID, string ContentType, string FileName = "", string FolderName = "")
        {
            long Attach = 0;
            string path = clsApplicationSetting.GetPhysicalPath("Attachments") + "\\";

            try
            {
                FileName = (string.IsNullOrEmpty(FileName) ? AttachmentID : FileName);
                long.TryParse(AttachmentID, out Attach);
                path = path + Attach + ContentType;
                if (System.IO.File.Exists(path))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                    string fileName = FileName + ContentType;
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
        }

        public ActionResult GetImage(string AttachmentID, string ContentType, string FolderName)
        {
            long Attach = 0;
            string path = "";
            long.TryParse(AttachmentID, out Attach);
            path = (string.IsNullOrEmpty(FolderName) ? "/Attachments/" : "/" + FolderName + "/");
            path = path + Attach + ContentType;
            if (!System.IO.File.Exists(Server.MapPath(path)))
            {
                path = Server.MapPath("/assets/design/images/c3.png");
            }

            return base.File(path, "image/jpeg");
        }
    }
}