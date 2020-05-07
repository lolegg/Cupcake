using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Services;
using Cupcake.Web.Framework;
using Cupcake.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Cupcake.Web.Controllers
{
    public class DataDictionaryController : BaseTreeController<DataDictionaryCondition>, ISelect
    {
        private readonly DataDictionaryService dataDictionaryService;
        public DataDictionaryController()
        {
            dataDictionaryService = new DataDictionaryService();
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var menu = dataDictionaryService.Get(id);
            if (menu == null || menu.IsDelete == true)
            {
                throw new NopException("数据字典不存在");
            }
            var model = menu.ToModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DataDictionaryModel model)
        {
            if (ModelState.IsValid)
            {
                if (dataDictionaryService.AlreadyExists(model.Name, model.ParentId, model.Id))
                {
                    ModelState.AddModelError("Name", "键名重复");
                }
                else
                {
                    var entity = dataDictionaryService.Get(model.Id);
                    //忽略更新
                    model.CreateDate = entity.CreateDate;

                    entity = model.ToEntity(entity);
                    dataDictionaryService.Modify(entity);
                    return Json(new AjaxResult() { Result = Result.Success });
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var entity = dataDictionaryService.Get(id);
            if (entity == null || entity.IsDelete == true)
            {
                throw new NopException("数据不存在");
            }
            if (dataDictionaryService.HasChildren(id))
            {
                return Json(new AjaxResult() { Result = Result.Error, Message = "请先删除子节点" });
            }
            else
            {
                entity.IsDelete = true;
                entity.UpdateDate = DateTime.Now;
                dataDictionaryService.Modify(entity);

                return Json(new AjaxResult() { Result = Result.Success });
            }
        }


        public override JsonResult InsertTree()
        {
            Guid parentId = Guid.Empty;
            if (!Guid.TryParse(Request["pid"].ToString(), out parentId)
                || Request["name"] == null)
            {
                throw new NopException("参数不合法");
            }
            string name = Request["name"].ToString();
            if (dataDictionaryService.AlreadyExists(name, parentId, Guid.Empty))
            {
                return Json(new AjaxResult() { Result = Result.Error, Message = "键名重复" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var entity = new DataDictionary() { Name = name, ParentId = parentId, CreatorId = CurrentUser.Id };
                dataDictionaryService.Add(entity);
                return Json(new TreeAjaxResult() { Result = Result.Success, CurrentNodeId = entity.Id.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public override JsonResult EditTree()
        {
            throw new NotImplementedException();
        }

        public override JsonResult DeleteTree()
        {
            throw new NotImplementedException();
        }

        public override JsonResult CheckTree()
        {
            throw new NotImplementedException();
        }

        public override JsonResult LoadTree()
        {
            var treeNodes = new List<TreeNode>();
            var dataDictionarys = dataDictionaryService.GetAllDataDictionarysForTree();
            foreach (dynamic dataDictionary in dataDictionarys)
            {
                var treeNode = new TreeNode()
                {
                    showEdit = false,
                    showRemove = false
                };
                treeNode.id = dataDictionary.Id.ToString();
                treeNode.parentid = dataDictionary.ParentId == null ? "0" : dataDictionary.ParentId.ToString();
                treeNode.name = dataDictionary.Name;
                treeNode.isParent = dataDictionary.ChildrenNumber > 0 ? "true" : "false";
                treeNodes.Add(treeNode);
            }
            return Json(treeNodes, JsonRequestBehavior.AllowGet);
        }

        public override ActionResult NodeView(DataDictionaryCondition condition)
        {
            var dataDictionarys = dataDictionaryService.SearchDataDictionarys(condition);
            var models = new PagedList<DataDictionary>(dataDictionarys, dataDictionarys.Paging);

            ViewBag.DataDictionaryId = condition.NodeId.ToString();
            return View(models);
        }

        public override JsonResult GetTreeSetting()
        {
            TreeSetting treeSetting = new TreeSetting();
            treeSetting.ShowDelete = false;
            treeSetting.ShowCheck = false;
            //addHoverDom总开关, ShowEdit和ShowDelete无效果, 需要在TreeNode单独设置
            treeSetting.ShowAdd = true;
            treeSetting.ShowEdit = false;
            treeSetting.ShowParentTreeData = true;
            treeSetting.IsAsync = false;
            treeSetting.TreeName = "数据字典";

            return Json(treeSetting, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectList(SelectCondition condition)
        {
            var root = dataDictionaryService.GetRoot();
            if (root == null)
            {
                throw new NopException("Root根节点不存在");
            }
            var dataDictionarys = dataDictionaryService.SearchDataDictionarys(new DataDictionaryCondition() { Name = condition.Name, NodeId = root.Id, BeginDate = condition.BeginDate, EndDate = condition.EndDate, PageIndex = condition.PageIndex, PageSize = condition.PageSize });
            var selectList = new SelectList<DataDictionary>();            
            selectList.PageList = dataDictionarys;
            selectList.HeadFiled = "Name,Value,CreateDate";
            selectList.HeadText = "名称,值,创建日期";
            selectList.SelectTextFiled = "Name";
            selectList.SelectValueFiled = "Id";
            selectList.Paging = dataDictionarys.Paging;
            return Json(selectList);
        }
    }
}
