using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Cupcake.Services;
using Cupcake.Core.Domain;
using Cupcake.Web.Models;
using Cupcake.Web.Framework;

namespace Cupcake.Web.Controllers
{
    public class ShowFormController : Controller
    {
        //
        // GET: /ShowForm/

        public ActionResult Index()
        {
            string tablename = Request.QueryString["t"];

            if (!string.IsNullOrEmpty(tablename))
            {
                SysSetFormService service = new SysSetFormService();
                SysSetForm model = service.GetAll().Where(m => m.TableName == tablename).ToList().FirstOrDefault();
                if(model != null)
                {
                    ViewBag.funtionname = model.FuntionName;
                }
                
            }
            ViewBag.tablename = tablename;

            return View();
        }

        [HttpPost]
        public ActionResult GetIndexData()
        {
            string strTableName = Request.Form["tablename"];

            if (!string.IsNullOrEmpty(strTableName))
            {
                SysSetFormService service = new SysSetFormService();
                List<SysSetForm> list = service.GetAll().Where(m => m.TableName == strTableName).ToList();

                if (list != null && list.Count > 0)
                {
                    SysSetForm model = list[0];

                    //获取数据
                    string strColumns = GetColumnNames(model.Columns);
                    string strDatas = service.SelectData(strTableName, strColumns);

                    return Json(new { result = "success", functionname = model.FuntionName,querycriteria = model.QueryCriteria, columns = model.Columns,datas = strDatas });
                }
                else
                {
                    return Json(new { result = "fail", msg = "没有相应的表单设置！" });
                }
            }
            else
            {
                return Json(new { result = "fail", msg = "参数传递错误！" });
            }
        }

        private string GetColumnNames(string columns)
        {
            StringBuilder sb = new StringBuilder();

            var Serializer = new JavaScriptSerializer();
            var collist = Serializer.Deserialize<List<Cupcake.Web.Models.ColumnModel>>(columns);
            if (collist != null && collist.Count > 0)
            {
                sb.Append("Id,");
                foreach(Cupcake.Web.Models.ColumnModel model in collist)
                {
                    sb.Append(model.ColumnName + ",");
                }
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        public ActionResult Create(string tablename)
        {
            ShowFormModel model = new ShowFormModel();
            model.TableName = tablename;

            return View(model);
        }

        [HttpPost]
        public ActionResult GetCreateData()
        {
            string strTableName = Request.Form["tablename"];

            if (!string.IsNullOrEmpty(strTableName))
            {
                SysSetFormService service = new SysSetFormService();
                List<SysSetForm> list = service.GetAll().Where(m => m.TableName == strTableName).ToList();

                if (list != null && list.Count > 0)
                {
                    SysSetForm model = list[0];

                    return Json(new { result = "success", functionname = model.FuntionName,columns = model.Columns });
                }
                else
                {
                    return Json(new { result = "fail", msg = "没有相应的表单设置！" });
                }
            }
            else
            {
                return Json(new { result = "fail", msg = "参数传递错误！" });
            }
        }

        [HttpPost]
        public ActionResult CreateCommit()
        {
            string strTableName = Request.Form["tablename"];
            string strData = Request.Form["data"];

            string[] arr = strData.Split(',');

            SysSetFormService service = new SysSetFormService();

            ReturnValue rv = service.InsertData(strTableName,arr);

            if(rv.IsSuccess)
            {
                return Json(new { result = "success", msg = "新增成功！" });
            }
            else
            {
                return Json(new { result = "fail", msg = rv.Message });
            }
            
        }

        public ActionResult Edit()
        {
            string strTableName = Request.QueryString["tablename"];
            string strId = Request.QueryString["id"];

            ShowFormModel model = new ShowFormModel();
            model.TableName = strTableName;
            model.Id = strId;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditCommit()
        {
            string strTableName = Request.Form["tablename"];
            string strId = Request.Form["id"];
            string strData = Request.Form["data"];

            string[] arr = strData.Split(',');

            SysSetFormService service = new SysSetFormService();

            ReturnValue rv = service.UpdateData(strTableName, strId, arr);

            if (rv.IsSuccess)
            {
                return Json(new { result = "success", msg = "修改成功！" });
            }
            else
            {
                return Json(new { result = "fail", msg = rv.Message });
            }

        }

        [HttpPost]
        public ActionResult GetEditData()
        {
            string strTableName = Request.Form["tablename"];
            string strId = Request.Form["id"];

            if (!string.IsNullOrEmpty(strTableName))
            {
                SysSetFormService service = new SysSetFormService();
                List<SysSetForm> list = service.GetAll().Where(m => m.TableName == strTableName).ToList();

                if (list != null && list.Count > 0)
                {
                    SysSetForm model = list[0];

                    //获取数据
                    string strColumns = GetColumnNames(model.Columns);
                    string strDatas = service.SelectData(strTableName, strColumns,strId);

                    return Json(new { result = "success", functionname = model.FuntionName, columns = model.Columns,datas = strDatas });
                }
                else
                {
                    return Json(new { result = "fail", msg = "没有相应的表单设置！" });
                }
            }
            else
            {
                return Json(new { result = "fail", msg = "参数传递错误！" });
            }
        }

        [HttpPost]
        public ActionResult Delete()
        {
            string strTableName = Request.QueryString["tablename"];
            string strId = Request.QueryString["id"];

            SysSetFormService service = new SysSetFormService();
            ReturnValue rv = service.Delete(strTableName, strId);

            if (rv.IsSuccess)
            {
                return Json(new AjaxResult() { Result = Result.Success });
            }
            else
            {
                return Json(new AjaxResult() { Result = Result.Error, Message = rv.Message });
            }
        }

        [HttpPost]
        public ActionResult QueryCommit()
        {
            string strTableName = Request.Form["tablename"];
            string strData = Request.Form["data"];

            if (!string.IsNullOrEmpty(strTableName))
            {
                SysSetFormService service = new SysSetFormService();
                List<SysSetForm> list = service.GetAll().Where(m => m.TableName == strTableName).ToList();

                if (list != null && list.Count > 0)
                {
                    SysSetForm model = list[0];

                    string[] arrArgs = strData.Split(',');

                    //获取数据
                    string strColumns = GetColumnNames(model.Columns);
                    string strDatas = service.SelectData(strTableName, strColumns, arrArgs);

                    return Json(new { result = "success", functionname = model.FuntionName, querycriteria = model.QueryCriteria, columns = model.Columns, datas = strDatas });
                }
                else
                {
                    return Json(new { result = "fail", msg = "没有相应的表单设置！" });
                }
            }
            else
            {
                return Json(new { result = "fail", msg = "参数传递错误！" });
            }
        }
    }
}
