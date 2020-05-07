using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.Models
{
    public class DataSelectSetting
    {
        public DataSelectSetting()
        {
            ShowMutileSelect = false;
            ShowPicSelect = false;
            DefaultIsSingleSelected = true;
            DefaultIsListView = true;
        }

        /// <summary>
        /// 是否显示多选按钮
        /// </summary>
        public bool ShowMutileSelect { get; set; }

        /// <summary>
        /// 默认是单选还是多选true: 单选, false:多选
        /// </summary>
        public bool DefaultIsSingleSelected { get; set; }

        /// <summary>
        /// 默认是列表还是图表,true: 列表, false:图表
        /// </summary>
        public bool DefaultIsListView { get; set; }

        /// <summary>
        /// 是否显示图文排班按钮(显示该按钮必须实现部分视图2)
        /// </summary>
        public bool ShowPicSelect { get; set; }
    }
}