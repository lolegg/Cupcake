using Cupcake.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Cupcake.Plugin.PluginManagement.Models
{
    public class PluginsModel : BaseModel
    {
        public PluginsModel()
        {
            this.BigVersionList = new List<SelectListItem>();
            this.SmallVersionList = new List<SelectListItem>();
            for (int i = 1; i < 9; i++)
            {
                this.BigVersionList.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
                this.SmallVersionList.Add(new SelectListItem() { Text = (i - 1).ToString(), Value = (i - 1).ToString() });
            }
        }
        public string Group { get; set; }
        [Required]
        [Display(Name = "系统名")]
        public string SystemName { get; set; }
        [Required]
        [Display(Name = "名称")]
        public string FriendlyName { get; set; }
        [Required]
        [Display(Name = "大版本")]
        public int BigVersion { get; set; }
        public IList<SelectListItem> BigVersionList { get; set; }
        [Required]
        [Display(Name = "小版本")]
        public int SmallVersion { get; set; }
        public IList<SelectListItem> SmallVersionList { get; set; }
        [Required]
        [Display(Name = "作者")]
        public string Author { get; set; }
        [Required]
        [Display(Name = "描述")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "状态")]
        public Guid PluginType { get; set; }
        public string PluginTypeName { get; set; }
    }
}
