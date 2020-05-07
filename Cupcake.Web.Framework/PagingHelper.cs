using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Cupcake.Web.Framework
{
    public static class PagingHelper
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id">分页id</param>
        /// <param name="currentPageIndex">当前页</param>
        /// <param name="pageSize">分页尺寸</param>
        /// <param name="recordCount">记录总数</param>
        /// <param name="htmlAttributes">分页头标签属性</param>
        /// <param name="className">分页样式</param>
        /// <param name="mode">分页模式</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, Paging paging, string onclick = "submitPagebody")
        {
            StringBuilder pagerHtml = new StringBuilder();
            int pageCount = GetPageCount(paging);//12
            if (paging.PageIndex == 0)//
            {
                pagerHtml.Append("<li class=\"prev disabled\"><a href=\"javascript:void(0);\">首页</a></li>");
                pagerHtml.Append("<li class=\"prev disabled\"><a href=\"javascript:void(0);\">上一页</a></li>");
            }
            else
            {
                pagerHtml.Append(string.Format("<li class=\"prev\"><a href='javascript:void(0);' onclick='{0}(0);'>首页</a></li>", onclick));
                pagerHtml.Append(string.Format("<li class=\"prev\"><a href='javascript:void(0);' onclick='{1}({0});'>上一页</a></li>", paging.PageIndex - 1,onclick));
            }
            int t = 5;
            int j = 0;
            int bj = 0;
            for (int i = paging.PageIndex; i >= 0; i--)//3
            {
                if (i < paging.PageIndex - 1)
                {
                    break;
                }

                if (i != 0)
                {
                    if (paging.PageIndex < 3)
                    {
                        pagerHtml.Append(string.Format("<li><a href='javascript:void(0);' onclick='{2}({0});'>{1}</a></li>", i - i + j, i - i + j + 1,onclick));
                        t--;
                        j++;
                    }
                    else
                    {
                        if (bj < 1)
                        {
                            i -= 2;
                            pagerHtml.Append(string.Format("<li><a href='javascript:void(0);' onclick='{2}({0});'>{1}</a></li>", i, i + 1,onclick));
                            t--;
                            bj++;
                            i += 2;
                        }
                        else
                        {
                            if (i != paging.PageIndex)
                            {
                                pagerHtml.Append(string.Format("<li><a href='javascript:void(0);' onclick='{2}({0});'>{1}</a></li>", i, i + 1,onclick));
                                t--;
                            }
                        }
                    }
                }
            }

            for (int i = paging.PageIndex; i < pageCount; i++)
            {
                if (t == 0)
                {
                    break;
                }
                if (i == paging.PageIndex)
                {
                    pagerHtml.Append(string.Format("<li class=\"active\"><a href='javascript:void(0);'>{0}</a></li>", i + 1));
                }
                else
                {
                    pagerHtml.Append(string.Format("<li><a href='javascript:void(0);' onclick='{2}({0});'>{1}</a></li>", i, i + 1,onclick));
                }
                t--;
            }

            if (paging.PageIndex == pageCount - 1)
            {
                pagerHtml.Append("<li class=\"next disabled\"><a href='javascript:void(0);'>末页</a></li>");
                pagerHtml.Append("<li class=\"next disabled\"><a href='javascript:void(0);'>下一页</a></li>");
            }
            else
            {
                pagerHtml.Append(string.Format("<li class=\"next\"><a href='javascript:void(0);' onclick='{1}({0});'>末页</a></li>", pageCount - 1,onclick));
                pagerHtml.Append(string.Format("<li class=\"next\"><a href='javascript:void(0);' onclick='{1}({0});'>下一页</a></li>", paging.PageIndex + 1,onclick));
            }
            return MvcHtmlString.Create(string.Format("<ul class=\"pagination\">{0}</ul>", pagerHtml.ToString()));
        }

        public static string PagingInfo(this HtmlHelper helper, Paging paging)
        {
            return string.Format("总计 {0} 条 当前第 {1} 页共 {2} 页", paging.Total, paging.PageIndex + 1, GetPageCount(paging));
        }

        private static int GetPageCount(Paging paging)
        {
            return paging.Total % paging.PageSize == 0 ? paging.Total / paging.PageSize : paging.Total / paging.PageSize + 1;
        }
    }
}
