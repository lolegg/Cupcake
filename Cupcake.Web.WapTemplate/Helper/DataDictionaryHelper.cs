
using Cupcake.Web.WapTemplate.edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cupcake.Web.WapTemplate.Helper
{
    public static class DataDictionaryHelper
    {
        public static string GetName(string expression)
        {
            return Get(expression).Name;
        }
        public static string GetName(Guid id)
        {
            return GetById(id).Name;
        }
        public static string GetValue(string expression)
        {
            return Get(expression).Value;
        }
        public static string GetValue(Guid id)
        {
            return GetById(id).Value;
        }
        public static Guid GetId(string expression)
        {
            return Get(expression).Id;
        }
        private static DataDictionary GetDataDictionaryByName(string name)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                var query = entity.DataDictionary;
                var root = query.FirstOrDefault(p => p.Name == "Root" && p.ParentId == null && p.IsDelete == false);
                if (root == null)
                {
                    throw new Exception("没有找到Root数据字典");
                }
                var dataDictionary = query.FirstOrDefault(p => p.Name == name && p.ParentId == root.Id && p.IsDelete == false);
                if (dataDictionary != null)
                {
                    return dataDictionary;
                }
                else
                {
                    throw new Exception("没有配置数据字典:" + name);
                }
            }
        }
        private static DataDictionary GetDataDictionaryByName(string parentName, string childName)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                var query = entity.DataDictionary;
                var root = query.FirstOrDefault(p => p.Name == "Root" && p.ParentId == null && p.IsDelete == false);
                if (root == null)
                {
                    throw new Exception("没有找到Root数据字典");
                }
                var dataDictionary = query.FirstOrDefault(p => p.Name == parentName && p.ParentId == root.Id && p.IsDelete == false);
                if (dataDictionary != null)
                {
                    var childDataDictionary = query.FirstOrDefault(p => p.Name == childName && p.ParentId == dataDictionary.Id && p.IsDelete == false);
                    if (childDataDictionary != null)
                    {
                        return childDataDictionary;
                    }
                    else
                    {
                        throw new Exception("没有该子数据字典:" + childName);
                    }
                }
                else
                {
                    throw new Exception("没有配置数据字典:" + parentName);
                }
            }
        }
        public static DataDictionary Get(string expression)
        {
            if (expression.Contains(">"))
            {
                var names = expression.Split(new[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                switch (names.Length)
                {
                    case 1:
                        return GetDataDictionaryByName(names[0]);
                    case 2:
                        return GetDataDictionaryByName(names[0], names[1]);
                    //case 3:
                    //    break;
                    default:
                        throw new Exception("表达式不正确");
                }
            }
            else
            {
                return GetDataDictionaryByName(expression);
            }
        }
        public static DataDictionary GetById(Guid id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                var query = entity.DataDictionary;
                var dataDictionary = query.FirstOrDefault(p => p.Id == id && p.IsDelete == false);
                if (dataDictionary != null)
                {
                    return dataDictionary;
                }
                else
                {
                    throw new Exception("没有该数据字典:" + id);
                }
            }
        }
        public static IList<SelectListItem> GetSelectList(string expression, Guid? selectedValue = null, string excludedValues = "")
        {
            var dataDictionary = Get(expression);
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                var query = entity.DataDictionary;
             
                var dataDictionarys = query.Where(p => p.ParentId == dataDictionary.Id && p.IsDelete == false).ToList();

                IList<SelectListItem> selectList = new List<SelectListItem>();
                selectList.Add(new SelectListItem { Text = "请选择", Value = "" });

                foreach (var item in dataDictionarys)
                {
                    if (excludedValues.Contains(item.Id.ToString()))
                    {
                        continue;
                    }
                    if (item.Id == selectedValue)
                    {
                        selectList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = true });
                    }
                    else
                    {
                        selectList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }

                return selectList;
            }
        }
        public static string GetDictionaryName(Guid? Name)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                var query = entity.DataDictionary;
                if (query != null)
                {
                    var dataDictionary = query.FirstOrDefault(n => n.Id == Name);
                    if (dataDictionary != null)
                    {
                        return dataDictionary.Name;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
        }
    }
}