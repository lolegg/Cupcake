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
    public class OrganizationModel:BaseModel
    {
        //public UserModel()
        //{
        //    //Mapper.Map<UserViewModel>(user);
        //}
        [Required]
        [Display(Name = "组织名称")]
        public string Name { get; set; }

        public string TypeDesc { get; set; }
        [Required]
        [Display(Name = "组织类型")]
        public OrganizationType Type { get; set; }


        public Organization ToOrganization()
        {
            return Mapper.Map<Organization>(this);
        }
        //public UserModel ToUserModel(User user)
        //{
        //    return Mapper.Map<UserModel>(user);
        //}        
    }

    public class OrganizationListModel : ListModel<OrganizationModel>
    {

        public Guid CurrentOrganizationId { get; set; }
        public string CurrentOrganizationName { get; set; }

        public OrganizationListModel(IList<Organization> list)
        {
            List = new List<OrganizationModel>();
            foreach (var item in list)
            {
                OrganizationModel model = Mapper.Map<OrganizationModel>(item);
                List.Add(model);
            }
        }


        public OrganizationListModel(IList<OrganizationExt> organizationList)
        {
            List = new List<OrganizationModel>();
            foreach (var organization in organizationList)
            {
                OrganizationModel organizationModel = Mapper.Map<OrganizationModel>(organization);
                List.Add(organizationModel);
            }
        }

        public OrganizationCondition condition { get; set; }

    }
}