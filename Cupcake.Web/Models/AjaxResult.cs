using AutoMapper;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Web.Models
{
    public class AjaxResult
    {
        public AjaxResult()
        {
            Message = "";
        }
        public Result Result { get; set; }
        public string Message { get; set; }
    }
    public class TreeAjaxResult : AjaxResult
    {
        public string CurrentNodeId { get; set; }
    }
}