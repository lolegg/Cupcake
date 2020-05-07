using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Cupcake.Core.Domain;

namespace Cupcake.Web.Models
{
    public class CreateTableModel : ListModel<ColumnModel>
    {
        [Required]
        [Display(Name = "表名")]
        public string TableName { get; set; }

        public void ColumnListModel(IList<ColumnModel> columnList)
        {
            List = new List<ColumnModel>();
            foreach (var column in columnList)
            {
                ColumnModel columnModel = Mapper.Map<ColumnModel>(column);
                List.Add(columnModel);
            }
        }
    }

    public class ColumnModel
    {
        [Required]
        [Display(Name = "列名")]
        public string ColumnName { get; set; }

        [Required]
        [Display(Name = "是否主键")]
        public bool PrimaryKey { get; set; }

        [Required]
        [Display(Name = "字段类型")]
        public ColumnType ColumnType { get; set; }

        [Required]
        [Display(Name = "字段长度")]
        public Int32 ColumnLength { get; set; }

        [Required]
        [Display(Name = "不能为空")]
        public bool IsNull { get; set; }
    }
}