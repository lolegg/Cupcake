
using Cupcake.Core.Domain;
using Cupcake.Services;
using Cupcake.Web.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;

namespace Cupcake.Web.Jobs
{
    public class UARCUserJob : Job
    {
        public override void Execute()
        {
            TextWriter tw = new StringWriter();
            HttpWorkerRequest wr = new SimpleWorkerRequest("SaveUarc", "SaveUarc", tw);
            HttpContext.Current = new HttpContext(wr);

            DirectorUarc director = new DirectorUarc(new ConcreteBuilderUarc());
            director.SaveUarc();
        }
    }

    #region 保存URAC 数据，使用建造者模式屏蔽构建和表现
    public abstract class BuilderUarc
    {
        public abstract void SaveDiffUser(string date);
        public abstract void SaveDiffOrg(string date);
        public abstract void SaveAllUserAndOrg();
    }

    public class ConcreteBuilderUarc : BuilderUarc
    {

        public static OrganizationService orgService = new OrganizationService();
        public static UserService userService = new UserService();

        public override void SaveDiffUser(string date)
        {
            Hashtable ht = new Hashtable();
            ht.Add("datetime", date);
            var xml = GenerUARCXml("GetUserDiff", ht);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml.InnerText);
            XmlElement root = xmlDoc.DocumentElement;
            XmlNodeList userList = root.SelectNodes("/District/item");
            List<User> queUserList = new List<User>();
            User user;
            foreach (XmlNode node in userList)
            {
                user = GenerDiffUser(node);
                queUserList.Add(user);
            }
            userService.SaveUARCUser(queUserList);
        }

        public override void SaveDiffOrg(string date)
        {
            Hashtable ht = new Hashtable();
            ht.Add("datetime", date);
            var xml = GenerUARCXml("GetDeptDiff", ht);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml.InnerText);
            XmlElement root = xmlDoc.DocumentElement;

            XmlNodeList orgList = root.SelectNodes("/District/item");
            List<Organization> queList = new List<Organization>();
            Organization org;
            foreach (XmlNode node in orgList)
            {
                org = GenerDiffOranization(node);
                queList.Add(org);

            }
            orgService.SaveUARCOrganization(queList);
        }

        public override void SaveAllUserAndOrg()
        {
            var xml = GenerUARCXml("GetDeptAndUser", new Hashtable());

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml.InnerText);
            XmlElement root = xmlDoc.DocumentElement;

            XmlNodeList GroupList = root.SelectNodes("/District/GroupName");

            List<Organization> queList = new List<Organization>();
            List<User> queUserList = new List<User>();

            Organization orgDistrict = GenerOrganization(root, 0);
            if (orgDistrict == null) return;

            queList.Add(orgDistrict);

            #region 载入处理队列
            foreach (XmlNode groupnode in GroupList)
            {

                Organization orgGroupName = GenerOrganization(groupnode, orgDistrict.UARCId);
                queList.Add(orgGroupName);

                XmlNodeList DeptList = groupnode.SelectNodes("Dept");
                foreach (XmlNode deptnode in DeptList)
                {
                    Organization orgDept = GenerOrganization(deptnode, orgGroupName.UARCId);
                    queList.Add(orgDept);

                    //一级section
                    XmlNodeList SectionList = deptnode.SelectNodes("Section");
                    foreach (XmlNode sectionnode in SectionList)
                    {
                        Organization orgSection = GenerOrganization(sectionnode, orgDept.UARCId);
                        queList.Add(orgSection);

                        XmlNodeList Users = sectionnode.SelectNodes("User");
                        foreach (XmlNode usernode in Users)
                        {
                            User user = GenerUser(usernode, orgSection.UARCId);
                            queUserList.Add(user);
                        }
                    }

                    //二级section
                    XmlNodeList SecondSectionList = deptnode.SelectNodes("DeptSecond");
                    foreach (XmlNode sectionnode in SecondSectionList)
                    {
                        Organization orgSecondSection = GenerOrganization(sectionnode, orgDept.UARCId);
                        queList.Add(orgSecondSection);
                        XmlNodeList Users = sectionnode.SelectNodes("User");
                        foreach (XmlNode usernode in Users)
                        {
                            User user = GenerUser(usernode, orgSecondSection.UARCId);
                            queUserList.Add(user);
                        }
                    }

                    //部门下直接挂接用户
                    XmlNodeList DeptUserList = deptnode.SelectNodes("User");
                    foreach (XmlNode usernode in DeptUserList)
                    {
                        User user = GenerUser(usernode, orgDept.UARCId);
                        queUserList.Add(user);
                    }

                }


            }
            #endregion

            #region 处理科室队列中的数据
            orgService.SaveUARCOrganization(queList);
            #endregion

            #region 处理用户队列中的数据
            userService.SaveUARCUser(queUserList);
            #endregion
        }


        #region
        public Organization GenerOrganization(XmlNode node, int pId)
        {

            Organization org = new Organization();
            org.UARCId = SafeParseInt(node.Attributes["id"].Value);
            org.UARCPId = pId;
            org.Name = node.Attributes["name"].Value;
            switch (node.Name)
            {
                case "GroupName":
                    org.Type = OrganizationType.Group;
                    break;
                case "Dept":
                    org.Type = OrganizationType.Department;
                    break;
                case "Section":
                    org.Type = OrganizationType.Section;
                    break;
                case "DeptSecond":
                    org.Type = OrganizationType.Section;
                    break;
                case "District":
                    org.Type = OrganizationType.District;
                    break;
            }
            //service.Add(org);
            return org;
        }

        public Organization GenerDiffOranization(XmlNode node)
        {
            Organization org = new Organization();
            org.UARCId = SafeParseInt(node.Attributes["AutoNum"].Value);
            org.UARCPId = SafeParseInt(node.Attributes["OrgParentOrgID"].Value);
            org.Name = node.Attributes["OrgFullName"].Value;
            if (node.Attributes["OrgUpdateTime"] != null && !string.IsNullOrEmpty(node.Attributes["OrgUpdateTime"].Value))
            {
                org.UpdateDate = DateTime.Parse(node.Attributes["OrgUpdateTime"].Value);
            }
            else
            {
                org.UpdateDate = DateTime.Now;
            }
            switch (node.Attributes["OrgUseStatus"].Value)
            {
                case "0":
                    org.IsDelete = true;
                    break;
            }
            return org;
        }



        public User GenerUser(XmlNode node, int orgId)
        {

            User user = new User();
            user.UARCId = SafeParseInt(node.Attributes["id"].Value);
            user.Name = node.Attributes["name"].Value;
            user.UserName = node.Attributes["UserName"].Value;
            user.Password = "123456";
            user.UserType = UserType.UARC;
            user.UARCOrganizationId = orgId;
            user.Status = ObjectStatus.Enable;
            return user;
        }

        public User GenerDiffUser(XmlNode node)
        {
            User user = new User();
            user.UARCId = SafeParseInt(node.Attributes["UorgUserID"].Value);
            user.UARCOrganizationId = SafeParseInt(node.Attributes["UorgOrganizationID"].Value);
            user.UserType = UserType.UARC;
            user.UserName = node.Attributes["UiviUserName"].Value;
            user.Name = node.Attributes["UbiDisplayName"].Value;
            switch (node.Attributes["Type"].Value)
            {
                case "delete":
                    user.IsDelete = true;
                    break;
            }
            user.Password = "123456";
            user.Status = ObjectStatus.Enable;
            return user;
        }


        private int SafeParseInt(string str, int arc = 0)
        {
            int.TryParse(str, out arc);
            return arc;
        }


        public XmlDocument GenerUARCXml(string functionName, Hashtable ht)
        {
            string url = ConfigurationManager.AppSettings["UARCOrganizationWebService"].ToLower();
            return WebServiceHelper.QueryGetWebService(url, functionName, ht);
        }
        #endregion
    }

    public class DirectorUarc
    { 
        ConcreteBuilderUarc builder ;

        public DirectorUarc(ConcreteBuilderUarc _builder)
        {
            this.builder = _builder;
        }


        public void SaveUarc()
        {
            

            VariablesService paraService = new VariablesService();
            //Parameter param = paraService.Get("uarclastupdatetime");
            
            //if (param != null)
            //{
            //    this.SaveDiffUarc(param.Value);
            //}
            //else
            //{
            //    /*数据库没有上传记录就是第一次全部获取,否则取最后一次获取数据的日期经行差异获取*/
            //    this.SaveAllUarc();
            //    param = new Parameter();
            //    param.Key = "uarclastupdatetime";
            //    param.Value = DateTime.Now.ToString("yyyy-MM-dd");
            //    paraService.Add(param);
            //}
        }

        public void SaveAllUarc()
        {
            builder.SaveAllUserAndOrg();
        }

        public void SaveDiffUarc(string date)
        {
            builder.SaveDiffUser(date);
            builder.SaveDiffOrg(date);
        }

        public void SaveDiffUser(string date)
        {
            builder.SaveDiffUser(date);
        }

        public void SaveDiffOrg(string date)
        {
            builder.SaveDiffOrg(date);
        }
    }

    #endregion
}