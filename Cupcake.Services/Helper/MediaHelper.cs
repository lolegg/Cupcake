using Cupcake.Core.Domain;
using Cupcake.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Cupcake.Services
{
    public static class MediaHelper
    {
        public static void SaveHasMedias(Guid ownerId, List<Guid> mediaIds)
        {
            using (var repository = new Repository<HasMedias>())
            {
                foreach (var mediaId in mediaIds)
                {
                    repository.Add(new HasMedias() { OwnerId = ownerId, MediaId = mediaId });
                }
                repository.Commit();
            }
        }
        public static List<Media> GetHasMedias(Guid ownerId)
        {
            var hasMedias = new Repository<HasMedias>().TableNoTracking;
            var medias = new Repository<Media>().TableNoTracking;
            return hasMedias.Where(p => p.OwnerId == ownerId).Join(medias, hm => hm.MediaId, m => m.Id, (hm, m) => m).ToList();
        }
        public static List<string> GetHasMediasThumbnailPath(Guid ownerId)
        {
            var hasMedias = new Repository<HasMedias>().TableNoTracking;
            var medias = new Repository<Media>().TableNoTracking;
            return hasMedias.Where(p => p.OwnerId == ownerId).Join(medias, hm => hm.MediaId, m => m.Id, (hm, m) => m.ThumbnailPath).ToList();
        }
    }
}
