using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSite.Shared.SharedModels
{
    public class ArticleSharedIndex
    {
        public string Title { get; set; }

        public string Language { get; set; }

        public ICollection<string> Tags { get; set; }
        public string ShortText { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime PublishDate { get; set; }
        public ReleaseSharedIndex? RelatedRelease { get; set; }
    }

    public class ArticleSharedDetail : ArticleSharedIndex
    {
        public string Text { get; set; }
    }

    public class ArticleSharedEditMode : ArticleSharedDetail
    {
        public int Id { get; protected set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDatePrivate { get; set; }
    }
}
