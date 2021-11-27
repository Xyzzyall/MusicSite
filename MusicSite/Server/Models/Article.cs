namespace MusicSite.Server.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Language { get; set; }
        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
