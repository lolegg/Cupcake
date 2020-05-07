using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Models
{
    public class CarouselFigureModel
    {
        /// <summary>
        /// 图片数量(默认显示全部)
        /// </summary>
        public int PictureCount { get; set; }

        /// <summary>
        /// 图片滚动毫秒数(1秒=1000)
        /// </summary>
        public int PictureScrollSeconds { get; set; }
        /// <summary>
        /// 图片显示高度
        /// </summary>
        public int PictureHeight { get; set; }

        /// <summary>
        /// 图片显示宽度
        /// </summary>
        public int PictureWidth { get; set; }

        /// <summary>
        /// 图片集合
        /// </summary>
        public List<CarouselFigureDetailModel> carouselFigureDetailModels { get; set; }
    }
}