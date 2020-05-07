using System;
using System.Configuration;
using System.Web;
using System.IO;
using Cupcake.Core.Domain;
using Cupcake.Core.Log4;

namespace Cupcake.Web.Framework
{
    public class FileHelper
    {
        public static UploadAjaxResult SaveFile(HttpPostedFileBase fileData, string uploadFilesPath = null)
        {
            // var a = HttpContext.Current.User;
            string extensionName = fileData.FileName.Substring(fileData.FileName.IndexOf("."));
            string uniqueIdentifier = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string newFileName = fileData.FileName.Substring(0, fileData.FileName.IndexOf(".")) + "_" + uniqueIdentifier + extensionName;

            if (uploadFilesPath == null)
            {
                uploadFilesPath = ConfigurationManager.AppSettings["UploadFilesPath"].ToString();
            }
            string physicalPath = "";
            //添加相对路径
            string relativePath = uploadFilesPath;
            if (GetFileSubfolder(extensionName) != "")
            {
                physicalPath = HttpContext.Current.Server.MapPath(uploadFilesPath) + "\\" + GetFileSubfolder(extensionName);
                relativePath = uploadFilesPath + "/" + GetFileSubfolder(extensionName);
            }
            else
            {
                physicalPath = HttpContext.Current.Server.MapPath(uploadFilesPath);
            }

            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }

            try
            {
                fileData.SaveAs(physicalPath + "\\" + newFileName);
            }
            catch (Exception ex)
            {
                //Log4Helper.Error(typeOf(FileHelper), ex, HttpContext.Current.User);

                return new UploadAjaxResult() { Result = Result.Error, Message = ex.Message };
            }
            return new UploadAjaxResult() { Result = Result.Success, Name = fileData.FileName, ExtensionName = extensionName, Path = physicalPath + "\\" + newFileName, RelativePath = relativePath + "/" + newFileName, ThumbnailPath = "" };
        }

        private static string GetFileSubfolder(string extensionName)
        {
            if (".doc;.docx;.rtf;.dotx;.dot;.xls;.xlsx;.xlt;.xltx;.ppt;.pptx;.pot;.potx;.infopathxml;.xsn;.vdw;.vdx;.vsd;.vsdx;.vst;.vstx;.mpp;.mpt;".Contains(extensionName.ToLower()))
            {
                return "Office";
            }
            else if (".jpg;.jpeg;.png;.bmp;.gif;.tiff;.raw;".Contains(extensionName.ToLower()))
            {
                return "Images";
            }
            else
            {
                return "";
            }
        }
    }
}
