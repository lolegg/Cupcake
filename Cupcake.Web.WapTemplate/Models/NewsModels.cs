using Cupcake.Web.WapTemplate.edmx;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cupcake.Web.WapTemplate.Models
{
    public class NewsModels
    {
        public Guid Id { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "新闻类型")]
        public Guid NewsType { get; set; }
        [Display(Name = "发布人")]
        public string Name { get; set; }
        [Display(Name = "发布时间")]
        public DateTime publicTime { get; set; }
        public List<Dictionary> DataDictionary { get; set; }
        public NewsModels()
        {
            Condition = new NewsCondition();
        }
        public NewsModels ToModel(News_News info)
        {
            this.Id = info.Id;
            this.Title = info.Title;
            this.Content = info.Content;
            this.NewsType = info.NewsType;
            this.Name = info.Name;
            this.publicTime = info.publicTime;
            return this;
        }

        public NewsCondition Condition { get; set; }
    }
    public class NewsCondition : Condition
    {
        public Guid NewsType { get; set; }

    }

    public class NewsModelsListModel : ListModel<NewsModels>
    {
        public string Index { get; set; }
        public string Total { get; set; }
        public string Size { get; set; }

        public List<Dictionary> DataDictionary { get; set; }
        public NewsModelsListModel(IList<News_News> NewsModelsListModel)
        {
            List = new List<NewsModels>();
            foreach (var dp in NewsModelsListModel)
            {
                NewsModels model = new NewsModels();
                model.Id = dp.Id;
                model.Name = dp.Name;
                model.NewsType = dp.NewsType;
                model.publicTime = dp.publicTime;
                model.Title = dp.Title;
                model.Content = dp.Content;
                List.Add(model);
            }
        }

    }
}