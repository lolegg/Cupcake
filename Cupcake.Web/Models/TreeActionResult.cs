using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Models
{

    public enum ActionStatus { 
        成功=0,
        失败
    }
    public class TreeActionResult {
        public ActionStatus Status { get; set; }

        public string Msg { get; set; }

        /// <summary>
        /// 当前操作节点ID,新增节点时需要赋值
        /// </summary>
        public string Id { get; set; }
    }
}