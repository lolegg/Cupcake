using Cupcake.Plugin.PluginManagement.Domain;
using Cupcake.Plugin.PluginManagement.Models;
using Cupcake.Web.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.PluginManagement
{
    public static class MappingExtensions
    {
        public static PluginsModel ToModel(this Plugins entity)
        {
            var model = new PluginsModel();
            model.Id = entity.Id;
            model.Group = entity.Group;
            model.SystemName = entity.SystemName;
            model.FriendlyName = entity.FriendlyName;
            model.BigVersion = entity.BigVersion;
            model.SmallVersion = entity.SmallVersion;
            model.Author = entity.Author;
            model.Description = entity.Description;
            model.PluginType = entity.PluginType;
            model.CreateDate = entity.CreateDate;
            model.PluginTypeName = DataDictionaryHelper.GetName(entity.PluginType);

            return model;
        }
        public static Plugins ToEntity(this PluginsModel model)
        {
            var entity = new Plugins();
            entity.Id = model.Id;
            entity.Group = model.Group;
            entity.SystemName = model.SystemName;
            entity.FriendlyName = model.FriendlyName;
            entity.BigVersion = model.BigVersion;
            entity.SmallVersion = model.SmallVersion;
            entity.Author = model.Author;
            entity.Description = model.Description;
            entity.PluginType = model.PluginType;
            
            return entity;
        }
    }
}
