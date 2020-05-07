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
    public class VariablesModel : BaseModel
    {
        [Required]
        [Display(Name = "变量名")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "值")]
        public string Value { get; set; }
    }

    //public class ParameterListModel : ListModel<VariablesModel>
    //{
    //    //public UserListModel(IList<User> userList)
    //    //{
    //    //    List = new List<UserModel>();
    //    //    foreach (var user in userList)
    //    //    {
    //    //        UserModel userModel = Mapper.Map<UserModel>(user);
    //    //        List.Add(userModel);
    //    //    }
    //    //}

    //    public ParameterListModel(IList<Parameter> parameterList) 
    //    {
    //        List = new List<VariablesModel>();
    //        foreach (var parameter in parameterList) 
    //        {
    //            VariablesModel parameterModel = Mapper.Map<VariablesModel>(parameter);
    //            List.Add(parameterModel);
    //        }
    //    }


    //}
}