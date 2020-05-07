using AutoMapper;
using Cupcake.Web.Models;
using Cupcake.Core.Domain;
using Cupcake.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cupcake.Web.Authorize;
using Cupcake.Core.Plugins;
using Cupcake.Core.Infrastructure;
using Cupcake.Core;
using Cupcake.Web.Framework;
using Cupcake.Core.Log4;

namespace Cupcake.Web.Controllers
{
    public class PluginController : BasePublicController
    {
        [HttpGet]
        public ActionResult Index()
        {
            PluginFinder pf = new PluginFinder();
            var list = pf.GetPluginDescriptors(LoadPluginsMode.All);
            ViewBag.Pager = new Paging() { PageIndex = 1, PageSize = 10 };
            return View(list);
        }


        public ActionResult Install(string systemName)
        {
            try
            {
                PluginFinder _pluginFinder = new PluginFinder();
                var pluginDescriptor = _pluginFinder.GetPluginDescriptorBySystemName(systemName, LoadPluginsMode.All);
                if (pluginDescriptor == null)
                    //No plugin found with the specified id
                    return RedirectToAction("List");

                //check whether plugin is not installed
                if (pluginDescriptor.Installed)
                    return RedirectToAction("List");

                //install plugin
                pluginDescriptor.Instance().Install();
                //SuccessNotification(_localizationService.GetResource("Admin.Configuration.Plugins.Installed"));

                //restart application
                //var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                WebHelper webHelper = new WebHelper();
                webHelper.RestartAppDomain();
            }
            catch (Exception exc)
            {
                //TODO:webHelper.RestartAppDomain();中HttpRuntime.UnloadAppDomain();会导致log4报错(未解析成员“log4net.Util.PropertiesDictionary...)
                //Log4Helper.Error(this.GetType(), exc, new Guid("8D2E6410-2B20-E711-8A80-B8AC6F19DFD9"));
                //return Json(new AjaxResult() { Result = Result.Error, Message = exc.Message });
            }

            return Json(new AjaxResult() { Result = Result.Success, Message = "安装成功" });
        }

        public ActionResult UnInstall(string systemName)
        {
            try
            {
                PluginFinder _pluginFinder = new PluginFinder();
                var pluginDescriptor = _pluginFinder.GetPluginDescriptorBySystemName(systemName, LoadPluginsMode.All);
                if (pluginDescriptor == null)
                    //No plugin found with the specified id
                    return RedirectToAction("List");

                //check whether plugin is not installed
                if (!pluginDescriptor.Installed)
                    return RedirectToAction("List");

                //install plugin
                pluginDescriptor.Instance().Uninstall();
                //SuccessNotification(_localizationService.GetResource("Admin.Configuration.Plugins.Installed"));

                //restart application
                //var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                WebHelper webHelper = new WebHelper();
                webHelper.RestartAppDomain();
            }
            catch (Exception exc)
            {
                //ErrorNotification(exc);
                throw exc;
            }

            return Json(new AjaxResult() { Result = Result.Success, Message = "卸载成功" });
        }

        [HttpPost]
        public ActionResult ReloadList()
        {
            WebHelper webHelper = new WebHelper();
            webHelper.RestartAppDomain();

            return Json(new AjaxResult() { Result = Result.Success, Message = "" });
        }
    }
}
