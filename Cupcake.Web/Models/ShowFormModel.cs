using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Web.Models
{
    public class ShowFormModel
    {
        [Display(Name = "编码")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "表名")]
        public string TableName { get; set; }
    }
}