using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Cupcake.Core.Domain;
using Cupcake.Web.Helper;
using Cupcake.Web.Framework;

namespace Cupcake.Web.Models
{
    public class SysSetFormModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "功能名")]
        public string FuntionName { get; set; }

        [Required]
        [Display(Name = "表名")]
        public string TableName { get; set; }

        [Required]
        [Display(Name = "列名")]
        public string Columns { get; set; }

        [Required]
        [Display(Name = "状态")]
        public PublishStatus Status { get; set; }
        public string StatusStr { get; set; }

    }

    public class SysSetFormListModel : ListModel<SysSetFormModel>
    {
        public SysSetFormListModel(IList<SysSetForm> setformList)
        {
            List = new List<SysSetFormModel>();
            foreach (var setform in setformList)
            {
                SysSetFormModel setformModel = Mapper.Map<SysSetFormModel>(setform);
                setformModel.StatusStr = EnumExtensions.GetDescription(setformModel.Status);
                List.Add(setformModel);
            }
        }

        public UserCondition condition { get; set; }

        public PublishStatus Status { get; set; }
    }
}