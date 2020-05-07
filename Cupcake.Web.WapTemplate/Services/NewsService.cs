using Cupcake.Web.WapTemplate.edmx;
using Cupcake.Web.WapTemplate.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Cupcake.Web.WapTemplate.Services
{
    public class NewsService : BaseService<News_News>
    {
        public IList<News_News> SearchNews(NewsCondition condition, ref Paging paging)
        {
            CupcakeEntities db = new CupcakeEntities();
            Expression<Func<News_News, bool>> where = PredicateExtensions.True<News_News>();
            if (condition.NewsType != null)
            {
                where = where.And(n => n.NewsType == condition.NewsType);
            }
            where = where.And(n => n.IsDelete == false);
            paging.Total = db.News_News.Where(n => n.NewsType == condition.NewsType).Count();
            return db.News_News.Where(where).OrderBy(n => n.CreateDate)
                      .Skip(paging.PageSize * paging.PageIndex)
                      .Take(paging.PageSize).ToList();
        }

        public List<News_News> GetListByTop(int topnum, int f_kindid)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                string sqlstr = string.Format("select top {0} * from [Cupcake].[dbo].[News_News] where NewsType=@f_kindid and IsDelete=0  order by CreateDate", topnum);
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@f_kindid", f_kindid));
                return entity.Database.SqlQuery<News_News>(sqlstr, param.ToArray()).ToList();
            }
        }

        public List<News_News> GetNewslist()
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                string sqlstr = string.Format("  select top 5* from [Cupcake].[dbo].[News_News] where IsDelete=0 order by UpdateDate desc");
                return entity.Database.SqlQuery<News_News>(sqlstr).ToList();
            }

        }
        public News_News GetbyNews(Guid id)
        {
            using (CupcakeEntities entity = new CupcakeEntities())
            {
                return entity.News_News.Where(p => p.Id == id && p.IsDelete == false).FirstOrDefault();
            }
        }

        public List<Dictionary> GetActivityTypeList(string lx)
        {
            CupcakeEntities entity = new CupcakeEntities();

            var listid = entity.DataDictionary.Where(n => n.Name == lx && n.IsDelete == false).ToList();
            if (listid.Count() > 0)
            {
                var parenid = listid.FirstOrDefault().Id;
                return entity.DataDictionary.Where(n => n.ParentId == parenid && n.IsDelete == false).Select(item => new Dictionary() { Id = item.Id, Name = item.Name, Tips = item.Tips }).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}