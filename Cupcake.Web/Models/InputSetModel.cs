using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Web.Models
{
    public class InputSetModel
    {
        [Required]
        [Display(Name = "序号")]
        public string Index { get; set; }

        [Required]
        [Display(Name = "列名")]
        public string ColumnName { get; set; }

        [Required]
        [Display(Name = "项目名")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "控件类型")]
        public string InputType { get; set; }
    }
}