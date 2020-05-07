using Cupcake.Core;
using Cupcake.Core.Domain;
using Cupcake.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Plugin.MessageOpen.Helper
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
        private static DataDictionary GetDataDictionaryByName(string parentName, string childName)
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
        public static DataDictionary GetById(Guid id)
        {
            var query = new Repository<DataDictionary>().TableNoTracking;
            var dataDictionary = query.FirstOrDefault(p => p.Id == id && p.IsDelete == false);
            if (dataDictionary != null)
            {
                return dataDictionary;
            }
            else
            {
                throw new NopException("没有该数据字典:" + id);
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
        public static IList<SelectListItem> GetTaskManagementSelectList()
        {
            IList<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "请选择", Value = "" });
            selectList.Add(new SelectListItem { Text = "未处置", Value = "false" });
            selectList.Add(new SelectListItem { Text = "已处置", Value = "true" });
            return selectList;
        }

        public static List<OrganizationExt> GetOrganizationList()
        {
            using (var repository = new Repository<OrganizationExt>())
            {

                string sql = @"select * from Organizations where IsDelete=0 ";

                return repository.GetwithdbSql(sql, new SqlParameter[] { }).ToList();
            }
        }
        public static string GetOrganizationName(string ids)
        {
            string sqldata = string.Empty;
            string organizationNames = string.Empty;
            if (!string.IsNullOrEmpty(ids))
            {
                    string str = ids.Substring(0, 1);
                    if(str==","){
                        var a = ids.Length;
                        Console.Write(ids.Length);

                        ids = ids.Substring(1, ids.Length-1);
                    }
             
                
                string[] strId = ids.Split(',');
                for (int i = 0; i < strId.Length; i++)
                {
                    if (strId[i] != "")
                    {
                        if (i > 0)
                        {
                            sqldata += ",";
                        }
                        sqldata += "'" + strId[i] + "'";
                     
                    }
                }
                using (var repository = new Repository<OrganizationExt>())
                {

                    string sql = @"select Name from Organizations where IsDelete=0 and  Id IN (" + sqldata + ") ";
                    List<OrganizationExt> organizationExtList = repository.GetwithdbSql(sql, new SqlParameter[] { }).ToList();
                    if (organizationExtList != null && organizationExtList.Count > 0)
                    {
                        for (int i = 0; i < organizationExtList.Count; i++)
                        {
                            if (i > 0)
                            {
                                organizationNames += ",";
                            }
                            organizationNames += organizationExtList[i].Name;
                        }
                    }

                }
            }
            return organizationNames;
        }
        public static string GetSelectName(Guid? id)
        {
            var query = new Repository<DataDictionary>().TableNoTracking;
            var name = query.Where(p => p.IsDelete == false).Where(p => p.Id == id).SingleOrDefault();
            return name.Name;
        }
        public static string GetYYYYMMDD(DateTime? time) {
            if (time == null)
                return "";
            DateTime date = (DateTime)time;
            return date.ToString("yyyy-MM-dd");
        }
        public static Guid GetOrganizationByName(string name) {
            using (var repository = new Repository<Organization>())
            {
               return repository.Table.Where(m => m.Name == name).SingleOrDefault().Id;
            }
        }


        //所属组织
        public static Organization OrganizationGet(string expression)
        {
            if (expression.Contains(">"))
            {
                var names = expression.Split(new[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                switch (names.Length)
                {
                    case 1:
                        return GetDataOrganizationByName(names[0]);
                    case 2:
                        return GetDataOrganizationByName(names[0], names[1]);
                    //case 3:
                    //    break;
                    default:
                        throw new NopException("表达式不正确");
                }
            }
            else
            {
                return GetDataOrganizationByName(expression);
            }
        }
        private static Organization GetDataOrganizationByName(string parentName, string childName)
        {
            var query = new Repository<Organization>().TableNoTracking;
            var root = query.FirstOrDefault(p => p.Name == "所属组织" && p.IsDelete == false);
            if (root == null)
            {
                throw new NopException("没有找到所属组织数据字典");
            }
            var dataDictionary = query.FirstOrDefault(p => p.Name == parentName  && p.IsDelete == false);
            if (dataDictionary != null)
            {
                var childDataDictionary = query.FirstOrDefault(p => p.Name == childName && p.IsDelete == false);
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
        private static Organization GetDataOrganizationByName(string name)
        {
            var query = new Repository<Organization>().TableNoTracking;
            var root = query.FirstOrDefault(p => p.Name == "所属组织" && p.IsDelete == false);
            if (root == null)
            {
                throw new NopException("没有找到所属组织数据字典");
            }
            var dataOrganization = query.FirstOrDefault(p => p.Name == name && p.IsDelete == false);
            if (dataOrganization != null)
            {
                return dataOrganization;
            }
            else
            {
                throw new NopException("没有配置数据字典:" + name);
            }
        }
        public static IList<SelectListItem> OrganizationGetSelectList(string expression, Guid? selectedValue = null, string excludedValues = "")
        {
            var dataOrganization = OrganizationGet(expression);
            var query = new Repository<Organization>().TableNoTracking;
            var dataDictionarys = query.Where(p => p.Pid == dataOrganization.Id && p.IsDelete == false).ToList();

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

       //分割字符串
        public static string[] GetStrSplit(string str){
            string[] itme=null;
            if(str!=null){
                itme=str.Split(',');
            }
            return itme;
        }

        //找到依申请公开
        //public bool GetMessageOpenName(Guid id)
        //{
        //    string name = DataDictionaryHelper.GetName(id);
        //    bool iswhere = false;
        //    if (name == "依申请公开")
        //    {
        //        iswhere = true;
        //    }
        //    return iswhere;
        //}
    }
}