using Cupcake.Web.Models;
using Cupcake.Core.Domain;

using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Cupcake.Web.Framework;

namespace Cupcake.Web.Controllers
{
    public class OrganizationController : BaseTreeController<CustomPermissionsCondition>
    {
        //
        // GET: /Organization/
        #region 重写
        public override JsonResult EditTree()
        {
            string id = Request["id"];
            string name = Request["name"];
            if (string.IsNullOrEmpty(name) || name.Length <= 2)
            {
                return Json(new TreeAjaxResult { Result = Result.Error, Message = "长度不能小于2" }, JsonRequestBehavior.AllowGet);
            }
            OrganizationService service = new OrganizationService();
            Guid g = Guid.Empty;
            if (Guid.TryParse(id, out g))
            {
                //系统组织
                service.Update(id, name);
            }
            else
            {
                //统一认证组织
                service.UpdateByUarcId(int.Parse(id), name);
            }
            return Json(new TreeAjaxResult { Result = Result.Success, Message = "操作成功", CurrentNodeId = id }, JsonRequestBehavior.AllowGet);
        }

        public override JsonResult DeleteTree()
        {
            string id = Request["id"];
            OrganizationService service = new OrganizationService();
            Guid g = Guid.Empty;
            if (Guid.TryParse(id, out g))
            {
                //系统组织
                service.Delete(id);
            }
            else
            {
                //统一认证组织
                service.DeleteByUarcId(id);
            }
            return Json(new TreeAjaxResult { Result = Result.Success, Message = "操作成功", CurrentNodeId = id }, JsonRequestBehavior.AllowGet);
        }

        public override JsonResult InsertTree()
        {
            string pid = Request["pid"];
            string name = Request["name"];
            if (string.IsNullOrEmpty(name) || name.Length <= 1) {
                return Json(new TreeAjaxResult { Result = Result.Error, Message = "长度不能小于1" }, JsonRequestBehavior.AllowGet);
            }


            OrganizationService service = new OrganizationService();
            Organization organization = new Organization();
            organization.Name = name;
            if (pid == "0")
            {
                //系统根组织
                organization.Pid = Guid.Empty;
                service.Insert(organization);
            }
            else {
                Guid g = Guid.Empty;
                if (Guid.TryParse(pid, out g))
                {
                    //系统子组织
                    organization.Pid = Guid.Parse(pid);
                    service.Insert(organization);
                }
                else
                {
                    //统一认证组织下挂系统自己组织
                    Organization parentOrg = service.GetByUarcId(int.Parse(pid));
                    organization.Name = name;
                    organization.Type = OrganizationType.Special;
                    organization.UARCPId = parentOrg.UARCId;
                    service.Insert(organization);
                }
            }
            return Json(new TreeAjaxResult { Result = Result.Success, Message = "操作成功", CurrentNodeId = organization.Id.ToString() }, JsonRequestBehavior.AllowGet);
        }


        public override JsonResult CheckTree()
        {
            //List<string> list = base.GetCheckNodeId();
            string roleid = Request["roleid"];
            return Json(new TreeAjaxResult { Result = Result.Success, Message = "操作成功" }, JsonRequestBehavior.AllowGet);
        }

        public override JsonResult LoadTree()
        {
            string parentid = Request["pid"];
            OrganizationService service = new OrganizationService();
            List<OrganizationExt> organization = new List<OrganizationExt>();
            organization = service.GetOrganizationById(parentid);
            List<TreeNode> list = new List<TreeNode>();
            TreeNode tm;
            foreach (OrganizationExt m in organization)
            {
                tm = new TreeNode();
                if (m.type == OrganizationType.Node)
                {
                    tm.id = m.Id.ToString();
                    tm.parentid = m.Pid.ToString();

                }
                else if (m.type == OrganizationType.Special)
                {
                    tm.id = m.Id.ToString();
                    tm.parentid = m.UARCPId.ToString();

                }
                else
                {
                    tm.font = "{'color':'#ccc'}";
                    tm.id = m.UARCId.ToString();
                    tm.parentid = m.UARCPId.ToString();
                    tm.showEdit = false;
                    tm.showRemove = false;
                }
                tm.name = m.Name;
                tm.isParent = m.SubMenuNums > 0 ? "true" : "false";

                list.Add(tm);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //重写设置，屏蔽删除按钮
        public override JsonResult GetTreeSetting()
        {
            TreeSetting ts = new TreeSetting();
            ts.ShowDelete = true;
            ts.ShowCheck = false;
            ts.ShowAdd = true;
            ts.ShowEdit = true;
            ts.ShowAddRoot = true;
            ts.ShowParentTreeData = true;
            ts.IsAsync = false;
            ts.TreeName = "组织架构";
            return Json(ts, JsonRequestBehavior.AllowGet);
        }

        public override ActionResult NodeView(CustomPermissionsCondition collections)
        {
            string menuid = Request["menuid"];
             UserListModel model;
             string name = Request["name"];
             string pageindex = Request["pageindex"];
             UserService us = new UserService();
             UserCondition uc = new UserCondition();
             uc.UserName = name;
             uc.PageSize = 6;
             uc.PageIndex = int.Parse(pageindex);
             Paging page = new Paging(uc);
             List<User> list = us.GetListUaRcallCondition(uc, ref page, menuid).ToList();

             model = new UserListModel(list);
             model.Paging = page;
             model.condition = uc;
             return PartialView("_Organization", model);
        }


        #endregion

    }
}
