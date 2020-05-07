using AutoMapper;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Web.Models
{
    public class RoleHasPermissionsModel : BaseModel
    {
        public string CustomPermissionsName { get; set; }
        public string CustomPermissionsDesc { get; set; }
        public Guid MenuId { get; set; }
        public bool HasPermission { get; set; }
    }
}