using AutoMapper;
using Cupcake.Core.Domain;
using Cupcake.Web.Framework;
using Cupcake.Web.Helper;
using Cupcake.Web.Models;
using System;

namespace Cupcake.Web
{
    public class AutoMapperConfig
    {
        public static void RegisterObjects()
        {
            Mapper.CreateMap<UserType, string>().ConvertUsing(new UserTypeConverter());
            Mapper.CreateMap<ObjectStatus, string>().ConvertUsing(new ObjectStatusConverter());

            Mapper.CreateMap<User, UserModel>()
                .ForMember(d => d.UserTypeDesc, opt => opt.MapFrom(s => s.UserType))
                .ForMember(d => d.StatusDesc, opt => opt.MapFrom(s => s.Status));
            Mapper.CreateMap<UserModel, User>();

            Mapper.CreateMap<Role, RoleModel>();
            Mapper.CreateMap<RoleModel, Role>();

            Mapper.CreateMap<Menu, MenuModel>();
            Mapper.CreateMap<MenuModel, Menu>();

            Mapper.CreateMap<Variables, VariablesModel>();
            Mapper.CreateMap<VariablesModel, Variables>();

            Mapper.CreateMap<MediaInfoModel, Media>();
            Mapper.CreateMap<Media, MediaInfoModel>();

            Mapper.CreateMap<DataDictionaryModel, DataDictionary>();
            Mapper.CreateMap<DataDictionary, DataDictionaryModel>();

            Mapper.CreateMap<Log, LogModel>()
                .ForMember(d => d.CreatorName, opt => opt.MapFrom(s => s.Creator.UserName));
            Mapper.CreateMap<LogModel, Log>();


            Mapper.CreateMap<CustomPermissions, CustomPermissionsModel>();
            Mapper.CreateMap<CustomPermissionsModel, CustomPermissions>();


            Mapper.CreateMap<BasePermissionExt, CustomPermissionsModel>();
            Mapper.CreateMap<CustomPermissionsModel, BasePermissionExt>();

            //Mapper.CreateMap<LogInfo, LogInfoModel>();
            //Mapper.CreateMap<LogInfoModel, LogInfo>();

            Mapper.CreateMap<SysSetForm, SysSetFormModel>();
            Mapper.CreateMap<SysSetFormModel, SysSetForm>();
        }
    }

    public class UserTypeConverter : ITypeConverter<UserType, string>
    {
        public string Convert(ResolutionContext context)
        {
            return EnumExtensions.ParseEnumName<UserType>(context.SourceValue.ToString()).GetDescription();
        }
    }
    public class ObjectStatusConverter : ITypeConverter<ObjectStatus, string>
    {
        public string Convert(ResolutionContext context)
        {
            return EnumExtensions.ParseEnumName<ObjectStatus>(context.SourceValue.ToString()).GetDescription();
        }
    }
}