using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Models
{
    public class CarouselFigureDetailModel
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PictureUrl{ get; set; }

        /// <summary>
        /// 图片跳转路径
        /// </summary>
        public string PictureJumpPath { get; set; }

        /// <summary>
        /// 图片显示文字标题
        /// </summary>
        public string Title { get; set; }

    }
}