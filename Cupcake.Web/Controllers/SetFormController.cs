using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using Cupcake.Core.Domain;
using Cupcake.Services;
using Cupcake.Web.Models;
using Cupcake.Web.Framework;

namespace Cupcake.Web.Controllers
{
    public class SetFormController : Controller
    {
        //
        // GET: /SetForm/

        public ActionResult Index(SysSetFormCondition condition)
        {
            SysSetFormService service = new SysSetFormService();

            Paging paging = new Paging(condition);
            var list = service.GetListByCondition(condition, ref paging);
            SysSetFormListModel model = new SysSetFormListModel(list);
            model.Paging = paging;
            
            ViewBag.condtion = condition;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateTable()
        {
            string strTableName = Request.Form["tablename"];
            string strColumn = Request.Form["column"];

            if (!string.IsNullOrEmpty(strTableName) && !string.IsNullOrEmpty(strColumn))
            {
                var Serializer = new JavaScriptSerializer();
                var collist = Serializer.Deserialize<List<Cupcake.Web.Models.ColumnModel>>(strColumn);
                if (collist != null && collist.Count > 0)
                {
                    CreateTableService service = new CreateTableService();
                    CreateTable data = new CreateTable();
                    data.ColumnModels = new List<Cupcake.Core.Domain.ColumnModel>();

                    data.TableName = strTableName;

                    for (int j = 0; j < collist.Count; j++)
                    {
                        Cupcake.Core.Domain.ColumnModel column = new Cupcake.Core.Domain.ColumnModel();

                        column.ColumnName = collist[j].ColumnName;
                        column.ColumnType = (ColumnType)collist[j].ColumnType;
                        column.ColumnLength = collist[j].ColumnLength;                        

                        data.ColumnModels.Add(column);
                    }

                    ReturnValue rv = service.Create(data);

                    if (rv.IsSuccess)
                    {
                        return Json(new { result = "success" });
                    }
                    else
                    {
                        return Json(new { result = "fail", msg = rv.Message });
                    }
                }
                else
                {
                    return Json(new { result = "fail", msg = "没有输入列信息！" });
                }
            }
            else
            {
                return Json(new { result = "fail", msg = "没有输入列信息！" });
            }
        }

        public ActionResult Create()
        {
            CreateTableModel model = new CreateTableModel();

            List<Cupcake.Web.Models.ColumnModel> list = new List<Cupcake.Web.Models.ColumnModel>();

            for (int i = 0; i < 10; i++)
            {
                list.Add(new Cupcake.Web.Models.ColumnModel());
            }

            model.ColumnListModel(list);

            return View(model);
        }

        [HttpPost]
        public ActionResult InputSet()
        {
            string strTableName = Request.Form["tablename"];
            string strFunctionName = Request.Form["functionname"];
            string strItems = Request.Form["items"];

            if (!string.IsNullOrEmpty(strTableName) && !string.IsNullOrEmpty(strFunctionName) && !string.IsNullOrEmpty(strItems))
            {
                var Serializer = new JavaScriptSerializer();
                var collist = Serializer.Deserialize<List<InputSetModel>>(strItems);
                if (collist != null && collist.Count > 0)
                {
                    SysSetFormService service = new SysSetFormService();
                    Cupcake.Core.Domain.SysSetForm data = new Cupcake.Core.Domain.SysSetForm();

                    data.TableName = strTableName;
                    data.FuntionName = strFunctionName;
                    data.Columns = strItems;
                    data.Status = Cupcake.Core.Domain.PublishStatus.NotPublished;

                    //for (int j = 0; j < collist.Count; j++)
                    //{
                    //    Reloadsoft.Framework.Model.ColumnModel column = new Model.ColumnModel();

                    //    column.ColumnName = collist[j].ColumnName;
                    //    column.ColumnType = (Model.ColumnType)collist[j].ColumnType;
                    //    column.ColumnLength = collist[j].ColumnLength;
                    //    column.PrimaryKey = collist[j].PrimaryKey;
                    //    column.IsNull = collist[j].IsNull;

                    //    data.ColumnModels.Add(column);
                    //}

                    int rv = service.Add(data);

                    if (rv > 0)
                    {
                        return Json(new { result = "success" });
                    }
                    else
                    {
                        return Json(new { result = "fail", msg = "设置失败！" });
                    }
                }
                else
                {
                    return Json(new { result = "fail", msg = "没有输入列信息！" });
                }
            }
            else
            {
                return Json(new { result = "fail", msg = "没有输入列信息！" });
            }
        }

        [HttpPost]
        public ActionResult QuerySet()
        {
            string strTableName = Request.Form["tablename"];
            string strItems = Request.Form["items"];

            if (!string.IsNullOrEmpty(strTableName) && !string.IsNullOrEmpty(strItems))
            {
                SysSetFormService service = new SysSetFormService();
                Cupcake.Core.Domain.SysSetForm data = service.GetAll().Where(m => m.TableName == strTableName).ToList().FirstOrDefault();

                if(data != null)
                {
                    data.QueryCriteria = strItems;
                    int rv = service.Modify(data);

                    if (rv > 0)
                    {
                        return Json(new { result = "success" });
                    }
                    else
                    {
                        return Json(new { result = "fail", msg = "设置失败！" });
                    }
                }
                else
                {
                    return Json(new { result = "fail", msg = "设置失败！" });
                }
            }
            else
            {
                return Json(new { result = "fail", msg = "没有选择查询条件！" });
            }
        }

        [HttpPost]
        public ActionResult Publish(Guid id)
        {
            SysSetFormService service = new SysSetFormService();
            var model = service.Get(id);
            model.Status = Cupcake.Core.Domain.PublishStatus.Published;

            int intRv = service.Modify(model);

            if(intRv > 0)
            {
                MenuService mservice = new MenuService();

                Cupcake.Core.Domain.Menu menu = new Cupcake.Core.Domain.Menu();

                menu.Name = model.FuntionName;
                menu.Sort = 9;
                menu.Href = "/ShowForm?t="+model.TableName;
                menu.ParentId = mservice.GetRootId();//new Guid("210D56FF-8EB0-E611-A9D4-00247EDEF854");
                menu.CreateDate = DateTime.Now;
                menu.UpdateDate = DateTime.Now;
                menu.IsDelete = false;

                mservice.Add(menu);
            }

            return Json(new { result = "success", msg = "发布成功！" });
        }

        [HttpPost]
        public ActionResult NoPublish(Guid id)
        {
            SysSetFormService service = new SysSetFormService();
            var model = service.Get(id);
            model.Status = Cupcake.Core.Domain.PublishStatus.NotPublished;

            int intRv = service.Modify(model);

            if(intRv > 0)
            {
                MenuService mservice = new MenuService();

                Cupcake.Core.Domain.Menu menu = mservice.GetAll().Where(m => (m.Href != null && m.Href.Contains(model.TableName))).ToList().FirstOrDefault();

                mservice.Remove(menu);
            }

            return Json(new { result = "success", msg = "撤回发布成功！" });
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            SysSetFormService service = new SysSetFormService();
            var model = service.Get(id);

            service.Remove(model);

            return Json(new AjaxResult() { Result = Cupcake.Core.Domain.Result.Success, Message = "" });
        }
    }
}
