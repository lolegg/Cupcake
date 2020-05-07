
using Cupcake.Web.Models;
using Cupcake.Core.Domain;

using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Cupcake.Web.Helper;
using Cupcake.Web.Framework;
using Cupcake.Core;

namespace Cupcake.Web.Controllers
{
    public class UploadifyController : Controller
    {
        public ActionResult Index(string fileType, string fileExts, int? fileLimit, string fileSizeLimit, string scopes, string thumbnail, string width, string height)
        {
            if (string.IsNullOrEmpty(fileType))
            {
                throw new NopException("参数不正确");
            }
            var dataDictionary = DataDictionaryHelper.Get(fileType);
            //var a = HttpContext.User;
            ViewBag.FileType = dataDictionary.Id;
            ViewBag.FileExts = dataDictionary.Value ?? fileExts;
            ViewBag.FileLimit = fileLimit ?? 0;
            ViewBag.FileSizeLimit = fileSizeLimit ?? "0";
            ViewBag.Scopes = scopes ?? "private";
            ViewBag.Thumbnail = thumbnail.ToLower() ?? "";
            ViewBag.Width = width ?? "";
            ViewBag.Height = height ?? "";
            return View();
        }

        public ActionResult Upload(Guid fileType, string scopes, string thumbnail, string width, string height, HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                var result = FileHelper.SaveFile(fileData);

                Image originalImage = null;
                string thumbnailRelativePath = "";
                if (IsImage(result.ExtensionName))
                {
                    originalImage = Image.FromFile(result.Path);
                    //系统默认缩略图                    
                    if (originalImage.Width > 138)
                    {
                        string thumbnailPhysicalPath = result.Path.Replace(result.ExtensionName, "") + "_d" + result.ExtensionName;                        
                        ThumbnailHelper.Thumbnail(originalImage, thumbnailPhysicalPath, 138, 0, "w");
                        thumbnailRelativePath = result.RelativePath.Replace(result.ExtensionName, "") + "_d" + result.ExtensionName;
                    }
                    else
                    {
                        thumbnailRelativePath = result.RelativePath;
                    }
                    //自定义缩略图
                    if (!string.IsNullOrEmpty(thumbnail))
                    {
                        string thumbnailPathSmall = result.Path.Replace(result.ExtensionName, "") + "_s" + result.ExtensionName;
                        ThumbnailHelper.Thumbnail(originalImage, thumbnailPathSmall, string.IsNullOrEmpty(width) ? 0 : Int32.Parse(width), string.IsNullOrEmpty(height) ? 0 : Int32.Parse(height), thumbnail);
                    }
                }
                else
                {
                    if (DataDictionaryHelper.GetValue("媒体类型>Office文件").Contains(result.ExtensionName))
                    {
                        if (".doc;.docx;.rtf;.dotx;.dot;".Contains(result.ExtensionName))
                        {
                            thumbnailRelativePath = "/Content/img/doc.jpg";
                        }
                        else if (".xls;.xlsx;.xlt;.xltx;".Contains(result.ExtensionName))
                        {
                            thumbnailRelativePath = "/Content/img/xls.jpg";
                        }
                        else
                        {
                            thumbnailRelativePath = "/Content/img/office.png";
                        }
                    }
                    else if (result.ExtensionName == ".txt")
                    {
                        thumbnailRelativePath = "/Content/img/txt.png";
                    }
                    else if (".rar;.zip".Contains(result.ExtensionName))
                    {
                        thumbnailRelativePath = "/Content/img/rar.png";
                    }
                    else
                    {
                        thumbnailRelativePath = "/Content/img/file.png";
                    }
                }

                //公共media会保存到数据
                //if (scopes.ToLower() == "public")
                //{
                var mediaService = new MediaService();
                var media = new Media()
                {
                    FileName = fileData.FileName,
                    ExtensionName = result.ExtensionName,
                    FilePath = urlconvertor(result.Path),
                    MediaType = fileType,
                    ThumbnailPath = thumbnailRelativePath
                };
                if (scopes.ToLower() == "public")
                {
                    media.IsPublic = true;
                }
                else
                {
                    media.IsPublic = false;
                }
                if (originalImage != null)
                {
                    media.Width = originalImage.Width;
                    media.Height = originalImage.Height;
                }
                mediaService.Add(media);
                //}
                if (originalImage != null)
                    originalImage.Dispose();                
                result.Id = media.Id.ToString();
                result.ThumbnailPath = thumbnailRelativePath;
                result.Width = media.Width == null ? "" : media.Width.GetValueOrDefault().ToString();
                result.Height = media.Height == null ? "" : media.Height.GetValueOrDefault().ToString();
                return Json(result);
            }
            else
            {
                return Json(new AjaxResult() { Result = Result.Error, Message = "未找到该文件流" });
            }
        }

        private string urlconvertor(string imagesurl1)
        {
            string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            string imagesurl2 = imagesurl1.Replace(tmpRootDir, ""); //转换成相对路径
            //imagesurl2 = imagesurl2.Replace(@"\", @"/");
            //imagesurl2 = imagesurl2.Replace(@"Aspx_Uc/", @"");
            return imagesurl2;
        }

        private FileResult SaveFileObject(HttpPostedFileBase filedata,string filepath, MediaClass? mediaclass)
        {
            bool noDB = mediaclass == null;
            HttpPostedFileBase file = filedata;
            //MediaTypeService typeService = new MediaTypeService();
            //MediaType type = null;
            string orgFilePath =filepath;
            FileResult result = null;
            if (!noDB)
            {
                string mediatype = HttpContext.Request.QueryString["type"];
                //type = typeService.Get(new Guid(mediatype));
                //if (mediaclass == MediaClass.图片)
                //{
                //    orgFilePath = "/upload/Picture/";
                //}
                //else
                //{
                //    orgFilePath = "/upload/Document/";
                //}
                //orgFilePath = orgFilePath + type.EnName + "/";
            }

            //获取文件的保存路径
            string uploadPath = HttpContext.Server.MapPath(orgFilePath);
            string extName = file.FileName.Substring(file.FileName.IndexOf("."));
            string guid = Guid.NewGuid().ToString();
            string fileSaveName = guid + extName;
            if (!System.IO.Directory.Exists(uploadPath))
            {
                System.IO.Directory.CreateDirectory(uploadPath);
            }
            string returnPath = orgFilePath + "\\" + fileSaveName;
            string newPath = uploadPath + fileSaveName;

            try
            {
                file.SaveAs(newPath);
                if (!noDB)
                {
                    MediaService service = new MediaService();
                    Media info = new Media();
                    info.FileName = file.FileName.Substring(0, file.FileName.LastIndexOf("."));
                    info.FilePath = orgFilePath + fileSaveName;
                    //info.ExtName = extName.Replace(".", "");
                    //info.MediaType_Id = type.Id;
                    //info.MediaClass = mediaclass.Value;
                    service.Add(info);
                }

                if (IsImage(extName))
                {
                    string thumbnailPathSmall = uploadPath + guid + "_32_32" + extName;
                    string thumbnailPathMiddle = uploadPath + guid + "_120_120" + extName;
                    //生成小图标
                    ThumbnailHelper.Thumbnail(newPath, thumbnailPathSmall, 32, 32, "W");
                    //中等图标
                    ThumbnailHelper.Thumbnail(newPath, thumbnailPathMiddle, 120, 120, "W");
                }

                result = new FileResult();
                result.name = file.FileName;
                result.path = returnPath;
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private FileResult SaveFile(MediaClass? mediaclass)
        {
            bool noDB = mediaclass == null;
            HttpPostedFileBase file = HttpContext.Request.Files["Filedata"];
            //MediaTypeService typeService = new MediaTypeService();
            //MediaType type = null;
            string orgFilePath = "";
            FileResult result=null;
            if (!noDB)
            {
                string mediatype = HttpContext.Request.QueryString["type"];
                //type = typeService.Get(new Guid(mediatype));
                //if (mediaclass == MediaClass.图片)
                //{
                //    orgFilePath = "/upload/Picture/";
                //}
                //else
                //{
                //    orgFilePath = "/upload/Document/";
                //}
                //orgFilePath = orgFilePath + type.EnName + "/";
            }
            else {
                orgFilePath = Request["folder"];
                
            }
            
            //获取文件的保存路径
            string uploadPath = HttpContext.Server.MapPath(orgFilePath);
            string extName = file.FileName.Substring(file.FileName.IndexOf("."));
            string guid = Guid.NewGuid().ToString();
            string fileSaveName = guid + extName;
            if (!System.IO.Directory.Exists(uploadPath))
            {
                System.IO.Directory.CreateDirectory(uploadPath);
            }
            string returnPath = orgFilePath + "\\" + fileSaveName;
            string newPath = uploadPath + fileSaveName;

            try
            {
                file.SaveAs(newPath);
                if (!noDB)
                {
                    MediaService service = new MediaService();
                    Media info = new Media();
                    info.FileName = file.FileName.Substring(0, file.FileName.LastIndexOf("."));
                    info.FilePath = orgFilePath + fileSaveName;
                    //info.ExtName = extName.Replace(".", "");
                    //info.MediaType_Id = type.Id;
                    //info.MediaClass = mediaclass.Value;
                    service.Add(info);
                }

                if (IsImage(extName))
                {
                    string thumbnailPathSmall = uploadPath + guid + "_32_32" + extName;
                    string thumbnailPathMiddle = uploadPath + guid + "_120_120" + extName;
                    //生成小图标
                    ThumbnailHelper.Thumbnail(newPath, thumbnailPathSmall, 32, 32, "W");
                    //中等图标
                    ThumbnailHelper.Thumbnail(newPath, thumbnailPathMiddle, 120, 120, "W");
                }

                result = new FileResult();
                result.name = file.FileName;
                result.path = returnPath;
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }


        private bool IsImage(string extName)
        {
            string imgInclude = "*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff;*.raw";
            return imgInclude.Contains(extName.ToLower());
        }
    }

    public class FileResult
    {
        public string name { get; set; }
        public string path { get; set; }
    }
}
