using Cupcake.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cupcake.Data.Mappings
{
    public class MediaMapping : FrameworkBaseEntityMapping<Media>
    {
        public MediaMapping()
        {
            this.ToTable("Medias");
            this.Property(m => m.FileName).IsRequired();
            this.Property(m => m.FilePath).IsRequired();
        }
    }
}
