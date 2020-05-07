using Cupcake.Data;
using Cupcake.Core;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Cupcake.Services
{
    public class MediaService : BaseService<Media>
    {
        public IPagedList<Media> SearchMedias(MediaCondition condition)
        {
            var query = new Repository<Media>().Table;

            if (!string.IsNullOrEmpty(condition.FileName))
                query = query.Where(t => t.FileName.Contains(condition.FileName));
            if (condition.MediaType != null)
                query = query.Where(t => t.MediaType == condition.MediaType);

            query = query.Where(t => t.IsDelete == false);
            query = query.Where(t => t.IsPublic == true);
            query = query.OrderByDescending(t => t.CreateDate);

            return new PagedList<Media>(query, condition.PageIndex, condition.PageSize);
        }
        public IList<Media> GetListByMediaTypeId(MediaCondition condition, ref Paging paging)
        {
            using (var repository = new Repository<Media>())
            {
                Expression<Func<Media, bool>> where = PredicateExtensions.True<Media>();
                //if (condition.MediaTypeId.HasValue)
                //{
                //    where = where.And(p =>p.MediaType_Id == condition.MediaTypeId  );
                //}
                if (!string.IsNullOrEmpty(condition.FileName))
                {
                    where = where.And(p => p.FileName.Contains(condition.FileName));
                }


                //where = where.And(p => p.MediaClass == condition.MediaClass);

                return repository.GetPaged(ref paging, where, m => m.CreateDate).ToList();
            }
        }

    }
}
