using AutoMapper;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.CreateDate = DateTime.Now;
            this.UpdateDate = DateTime.Now;
            IsDelete = false;
        }
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public string CreatorName { get; set; }
    }
}