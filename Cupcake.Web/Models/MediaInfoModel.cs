using AutoMapper;
using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cupcake.Web.Models
{
    public class MediaInfoModel : BaseModel
    {
        public string FileName { get; set; }

        public string ExtName { get; set; }

        public string FilePath { get; set; }
    }

    public class MediaInfoListModel : ListModel<MediaInfoModel>
    {

        public MediaInfoListModel(IList<Media> infoList)
        {
            List = new List<MediaInfoModel>();
            foreach (var info in infoList)
            {
                MediaInfoModel model = Mapper.Map<MediaInfoModel>(info);
                List.Add(model);
            }
        }

        public MediaCondition condition { get; set; }
    }
}