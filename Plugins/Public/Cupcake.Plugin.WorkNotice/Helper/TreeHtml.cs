using Cupcake.Core.Domain;
using Cupcake.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Cupcake.Plugin.WorkNotice.Helper
{
    public static class TreeHtml
    {
        public static string GetTreeHtml(List<OrganizationExt> list, Guid ParentId, string checkeds="", int level = 0)
        {
            return child(list, ParentId, checkeds,level).ToString();
        }

        public static StringBuilder child(List<OrganizationExt> list, Guid ParentId, string checkeds, int level)
        {
            StringBuilder restr = new StringBuilder();
            if (ParentId != null && list!=null)
            {
                var thislist = list.Where(m=>m.Pid==ParentId);
                if (thislist != null)
                {
                    var i = 0;
                    foreach (var item in thislist)
                    {
                        if (item.Pid.ToString() == "00000000-0000-0000-0000-000000000000")
                        {
                            restr.Append("<p>");
                        }
                        else
                        {
                            restr.Append("<p style=\"margin-top:-10px;\">");
                        }
                        var listimg= list.Where(m => m.Pid ==item.Id).ToList();
                        if (listimg!=null&&listimg.Count()>0)
                        {
                            if (!string.IsNullOrEmpty(checkeds)&&checkeds.Contains(item.Id.ToString()))
                            {
                                restr.AppendFormat("<span style='padding-left:{0}px;'><img id={2} src=\'/Plugins/Cupcake.Plugin.WorkNotice/Content/Img/jiah.jpg\' alt=\"折叠\" style=\"cursor: pointer;margin-bottom: 8px\" onclick=\"btnShow(this)\" /><input type=\"checkbox\" checked=\"checked\" name=\"OrganizationList\" value={2} id={3} onclick=\"btnCheck(this)\" style=\"width:13px; height:13px;\"/>{1}</span>", level * 4, item.Name, item.Id, item.Pid);
                            }
                            else
                            {
                                restr.AppendFormat("<span style='padding-left:{0}px;'><img id={2} src=\'/Plugins/Cupcake.Plugin.WorkNotice/Content/Img/jiah.jpg\' alt=\"折叠\" style=\"cursor: pointer;margin-bottom: 8px\"  onclick=\"btnShow(this)\" /><input type=\"checkbox\"  name=\"OrganizationList\" value={2} id={3} onclick=\"btnCheck(this)\" style=\"width:13px; height:13px;\"/>{1}</span>", level * 4, item.Name, item.Id, item.Pid);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(checkeds) && checkeds.Contains(item.Id.ToString()))
                            {
                                restr.AppendFormat("<span style='padding-left:{0}px;'><img id={2} src=\'/Plugins/Cupcake.Plugin.WorkNotice/Content/Img/jiah.jpg\' style=\"visibility:hidden\"/><input type=\"checkbox\" checked=\"checked\" name=\"OrganizationList\" value={2} id={3} onclick=\"btnCheck(this)\" style=\"width:13px; height:13px;\"/>{1}</span>", level * 4, item.Name, item.Id, item.Pid);
                            }
                            else
                            {
                                restr.AppendFormat("<span style='padding-left:{0}px;'><img id={2} src=\'/Plugins/Cupcake.Plugin.WorkNotice/Content/Img/jiah.jpg\' style=\"visibility:hidden\"/><input type=\"checkbox\" name=\"OrganizationList\" value={2} id={3} onclick=\"btnCheck(this)\" style=\"width:13px; height:13px;\"/>{1}</span>", level * 4, item.Name, item.Id, item.Pid);
                            }
                            
                        }
                        restr.Append("</p>");
                        restr.Append(child(list, item.Id,checkeds, level + 5));
                        i++;
                    }
                }
            }
            return restr;
        }




        public static Dictionary<string,string> GetTreeDic(List<OrganizationExt> list, Guid ParentId, int level = 0)
        {
            return childDic(list, ParentId, level);
        }
        public static Dictionary<string, string> childDic(List<OrganizationExt> list, Guid ParentId, int level)
        {
            Dictionary<string, string> dirlist = new Dictionary<string, string>();
            if (ParentId != null)
            {
                var thislist = list.Where(m => m.Pid == ParentId);
                if (thislist != null)
                {
                    foreach (var item in thislist)
                    {
                        string tempname =GetChar('-',level*2)+ item.Name;
                        dirlist.Add(tempname, item.Id.ToString());
                        var tttt = childDic(list, item.Id, level + 1);
                        foreach (var itma in tttt)
                        {
                            dirlist.Add(itma.Key, itma.Value);
                        }
                        //dirlist.Add(childDic(list, item.Id, level + 1));
                    }
                }
            }
            return dirlist;
        }
        public static string GetChar(char a, int num)
        {
            string restr = "";
            for (int i = 0; i < num; i++)
            {
                restr += a;
            }
                return restr;
        }

        public static List<OrganizationExt> GetListOrganization()
        {
           using(var Repository=new Repository<OrganizationExt>())
           {
                var sql="select * from Organizations where IsDelete=0";
               return Repository.GetwithdbSql(sql, new SqlParameter[] { }).ToList();
           }
        }


        public static string GetName(string id) 
        {
            using (var Repository = new Repository<OrganizationExt>())
            {
                var sql = "select * from Organizations where IsDelete=0 and Id='"+id+"'";
                var name= Repository.GetwithdbSql(sql, new SqlParameter[] { }).FirstOrDefault();
                if (name.Pid.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    return "<br><span style=\'margin-left: 25px;\'>" + name.Name + ": </span>";
                }
                else 
                {
                    return "&nbsp;&nbsp;&nbsp;<span>" + name.Name + "</span>";
                }


            }
        
        }
    }
}
