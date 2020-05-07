using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cupcake.Plugin.Activity.Helper
{
    public static class DataDictionaryHelper
    {
        public static string GetDataDictionaryValue(string parentName, string childName)
        {
            var query = new Repository<DataDictionary>().TableNoTracking;
            var root = query.FirstOrDefault(p => p.Name == "Root" && p.ParentId == null && p.IsDelete == false);
            if (root == null)
            {
                throw new NopException("没有找到Root数据字典");
            }
            var dataDictionary = query.FirstOrDefault(p => p.Name == parentName && p.ParentId == root.Id && p.IsDelete == false);
            if (dataDictionary != null)
            {
                var childDataDictionary = query.FirstOrDefault(p => p.Name == childName && p.ParentId == dataDictionary.Id && p.IsDelete == false);
                if (childDataDictionary != null)
                {
                    return childDataDictionary.Value;
                }
                else
                {
                    throw new NopException("没有该子数据字典:" + childName);
                }
            }
            else
            {
                throw new NopException("没有配置数据字典:" + parentName);
            }
        }
        public static DataDictionary GetDataDictionaryByName(string name)
        {
            var query = new Repository<DataDictionary>().TableNoTracking;
            var root = query.FirstOrDefault(p => p.Name == "Root" && p.ParentId == null && p.IsDelete == false);
            if (root == null)
            {
                throw new NopException("没有找到Root数据字典");
            }
            var dataDictionary = query.FirstOrDefault(p => p.Name == name && p.ParentId == root.Id && p.IsDelete == false);
            if (dataDictionary != null)
            {
                return dataDictionary;
            }
            else
            {
                throw new NopException("没有配置数据字典:" + name);
            }
        }
        public static DataDictionary GetDataDictionaryByName(string parentName, string childName)
        {
            var query = new Repository<DataDictionary>().TableNoTracking;
            var root = query.FirstOrDefault(p => p.Name == "Root" && p.ParentId == null && p.IsDelete == false);
            if (root == null)
            {
                throw new NopException("没有找到Root数据字典");
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
                    throw new NopException("没有该子数据字典:" + childName);
                }
            }
            else
            {
                throw new NopException("没有配置数据字典:" + parentName);
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
                        throw new NopException("表达式不正确");
                }
            }
            else
            {
                return GetDataDictionaryByName(expression);
            }
        }
        public static IList<SelectListItem> GetSelectList(string expression, Guid? selectedValue = null, string excludedValues = "")
        {
            var dataDictionary = Get(expression);
            var query = new Repository<DataDictionary>().TableNoTracking;
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


        public static string GetDictionaryName(Guid? Name) 
        {
            var query = new Repository<DataDictionary>().TableNoTracking;
            if (Name != null)
            {


                var dataDictionary = query.FirstOrDefault(n => n.Id.ToString() == Name.ToString());
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

        public static DataDictionary GetDictionary(string Name) 
        {
                var query = new Repository<DataDictionary>().TableNoTracking;
                if (Name != null)
                {
                    var dataDictionary = query.FirstOrDefault(n => n.Name == Name);
                    if (dataDictionary != null)
                    {
                        return dataDictionary;
                    }
                    else
                    {
                        return null;
                    }
                }
                else {
                    return null;
                }
        }
    }
}