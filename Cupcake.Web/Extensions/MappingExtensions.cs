using AutoMapper;
using Cupcake.Core.Domain;
using Cupcake.Web.Models;
using System;
using System.Linq;

namespace Cupcake.Web
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        #region Variables
        public static VariablesModel ToModel(this Variables entity)
        {
            return Mapper.Map<Variables, VariablesModel>(entity);
        }
        public static Variables ToEntity(this VariablesModel model)
        {
            return Mapper.Map<VariablesModel, Variables>(model);
        }
        public static Variables ToEntity(this VariablesModel model, Variables destination)
        {
            return model.MapTo(destination);
        }
        #endregion

        public static LogModel ToModel(this Log entity)
        {
            return Mapper.Map<Log, LogModel>(entity);
        }

        #region User
        public static UserModel ToModel(this User entity)
        {
            return Mapper.Map<User, UserModel>(entity);
        }
        public static User ToEntity(this UserModel model)
        {
            return Mapper.Map<UserModel, User>(model);
        }
        public static User ToEntity(this UserModel model, User destination)
        {
            return model.MapTo(destination);
        }
        #endregion

        #region Role
        public static RoleModel ToModel(this Role entity)
        {
            return Mapper.Map<Role, RoleModel>(entity);
        }
        public static Role ToEntity(this RoleModel model)
        {
            return Mapper.Map<RoleModel, Role>(model);
        }
        public static Role ToEntity(this RoleModel model, Role destination)
        {
            return model.MapTo(destination);
        }
        #endregion

        #region Menu
        public static MenuModel ToModel(this Menu entity)
        {
            return Mapper.Map<Menu, MenuModel>(entity);
        }
        public static Menu ToEntity(this MenuModel model)
        {
            return Mapper.Map<MenuModel, Menu>(model);
        }
        public static Menu ToEntity(this MenuModel model, Menu destination)
        {
            return model.MapTo(destination);
        }
        #endregion

        #region DataDictionary
        public static DataDictionaryModel ToModel(this DataDictionary entity)
        {
            return Mapper.Map<DataDictionary, DataDictionaryModel>(entity);
        }
        public static DataDictionary ToEntity(this DataDictionaryModel model)
        {
            return Mapper.Map<DataDictionaryModel, DataDictionary>(model);
        }
        public static DataDictionary ToEntity(this DataDictionaryModel model, DataDictionary destination)
        {
            return model.MapTo(destination);
        }
        #endregion

        #region CustomPermissions
        public static CustomPermissionsModel ToModel(this CustomPermissions entity)
        {
            return Mapper.Map<CustomPermissions, CustomPermissionsModel>(entity);
        }
        public static CustomPermissions ToEntity(this CustomPermissionsModel model)
        {
            return Mapper.Map<CustomPermissionsModel, CustomPermissions>(model);
        }
        public static CustomPermissions ToEntity(this CustomPermissionsModel model, CustomPermissions destination)
        {
            return model.MapTo(destination);
        }
        #endregion
    }
}