using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Core.Plugins;
using Cupcake.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Services
{
    public static class BasePluginExtensions
    {
        /// <summary>
        /// 添加二级菜单至插件
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="name"></param>
        /// <param name="href"></param>
        public static void AddMenu(this BasePlugin plugin, string name, string href)
        {
            var service = new MenuService();
            var query = new Repository<Menu>().TableNoTracking;
            var root = query.FirstOrDefault(p => p.Name == "Root" && p.ParentId == null && p.IsDelete == false);
            if (root == null)
            {
                throw new NopException("Root菜单不存在");
            }
            var plugins = query.FirstOrDefault(p => p.Name == "插件" && p.ParentId == root.Id && p.IsDelete == false);
            if (plugins == null)
            {
                var newPlugins = new Menu() { Name = "插件", ParentId = root.Id, ClassName = "fa-cogs" };
                service.Add(newPlugins);
                service.Add(new Menu() { Name = name, ParentId = newPlugins.Id, Href = href });
            }
            else
            {
                if (service.AlreadyExists(name, plugins.Id, Guid.Empty))
                {

                }
                else
                {
                    service.Add(new Menu() { Name = name, ParentId = plugins.Id, Href = href });
                }
            }
        }
        //public static void AddMenu(this BasePlugin plugin, string parentName, string name, string href)
        //{
        //    var menuService = new MenuService();
        //    menuService.AddMenuAtRoot(name, href);
        //}
        //public static void RemoveMenuAtRoot(this BasePlugin plugin, string name, string href)
        //{
        //    var menuService = new MenuService();
        //    menuService.RemoveMenuAtRoot(name, href);
        //}
        public static void AddDataDictionary(this BasePlugin plugin, string name)
        {
            var query = new Repository<DataDictionary>().TableNoTracking;
            var root = query.FirstOrDefault(p => p.Name == "Root" && p.ParentId == null && p.IsDelete == false);
            if (root == null)
            {
                throw new NopException("Root数据字典不存在");
            }
            var service = new DataDictionaryService();
            if (service.AlreadyExists(name, root.Id, Guid.Empty))
            {
                //TODO:log
            }
            else
            {
                var entity = new DataDictionary() { Name = name, ParentId = root.Id };
                service.Add(entity);
            }
        }
        public static void AddDataDictionary(this BasePlugin plugin, string parentName, string name, string value = null)
        {
            var query = new Repository<DataDictionary>().TableNoTracking;
            var root = query.FirstOrDefault(p => p.Name == "Root" && p.ParentId == null && p.IsDelete == false);
            if (root == null)
            {
                throw new NopException("Root数据字典不存在");
            }
            var parent = query.FirstOrDefault(p => p.Name == parentName && p.ParentId == root.Id && p.IsDelete == false);
            if (parent == null)
            {
                throw new NopException(parentName + "数据字典不存在");
            }
            var service = new DataDictionaryService();
            if (service.AlreadyExists(name, parent.Id, Guid.Empty))
            {
                //TODO:log
            }
            else
            {
                var entity = new DataDictionary() { Name = name, ParentId = parent.Id, Value = value };
                service.Add(entity);
            }
        }
    }
}
