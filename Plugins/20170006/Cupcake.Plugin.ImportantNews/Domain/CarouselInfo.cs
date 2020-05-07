using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupcake.Plugin.ImportantNews.Domain
{
    public class CarouselInfo : PluginBaseEntity
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PictureUrl { get; set; }

        /// <summary>
        /// 图片跳转路径
        /// </summary>
        public string PictureJumpPath { get; set; }

        /// <summary>
        /// 图片显示文字标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 轮播图所属模块
        /// </summary>
        public Guid CarouselType { get; set; }

        /// <summary>
        /// 详情所属模块
        /// </summary>
        public Guid? DetailsType { get; set; }

        /// <summary>
        /// 详情路径
        /// </summary>
        public string LuJing { get; set; }
    }
    public class CarouselCondition : Condition
    {
        public string Title { get; set; }

    }
}
