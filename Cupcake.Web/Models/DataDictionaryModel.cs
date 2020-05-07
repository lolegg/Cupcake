using AutoMapper;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cupcake.Web.Models
{
    public class DataDictionaryModel : BaseModel
    {
        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "值")]
        public string Value { get; set; }

        [Required]
        [Display(Name = "排序")]
        public int Sort { get; set; }
        
        [Display(Name = "提示信息")]
        public string Tips { get; set; }

        [Display(Name = "自定义类型")]
        public int? CustomType { get; set; }

        [Display(Name = "自定义属性")]
        public string CustomAttributes { get; set; }

        public Guid ParentId { get; set; }
    }
}