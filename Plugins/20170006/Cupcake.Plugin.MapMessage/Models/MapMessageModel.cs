using AutoMapper;
using Cupcake.Core.Model;
using Cupcake.Plugin.MapMessage.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cupcake.Plugin.MapMessage.Models
{
    public class MapMessageModel : BaseModel
    {
        [Required]
        [Display(Name = "标题")]
        public string Name { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [Required]
        [Display(Name = "类型")]
        public Guid SortType { get; set; }
        [Required]
        [Display(Name = "成立日期")]
        public DateTime? EstablishDate { get; set; }
        [Required]
        [Display(Name = "成立机关")]
        public Guid RegisterOrganization { get; set; }
        /// <summary>
        /// 所属模块 
        /// </summary>
        [Required]
        [Display(Name = "所属模块")]
        public Guid SortModual { get; set; }
        [Required]
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "经度")]
        public string lng { get; set; }
        [Required]
        [Display(Name = "维度")]
        public string lat { get; set; }
    }
    public class MapMessageListModel : ListModel<MapMessageModel>
    {
        public string Name { get; set; }

     
        public Guid? SortModual { get; set; }

        public MapMessageListModel(IList<MapMessageInfo> MapMessageItemList)
        {
            List = new List<MapMessageModel>();
          
            foreach (var item in MapMessageItemList)
            {
               
            var mapmessagemodel = Mapper.Map<MapMessageInfo, MapMessageModel>(item);

                List.Add(mapmessagemodel);
            }
     
        }
    }
}
