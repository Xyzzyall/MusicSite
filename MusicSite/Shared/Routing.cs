namespace MusicSite.Shared
{
    public static class Routing
    {
        public const string Root = "/api";

        public const string AuthentificationController = Root + "/Auth";

        public const string AnonReleasesController = Root + "/AnonReleases";

        public const string AnonTagsController = Root + "/AnonTags";

        public const string AnonArticlesController = Root + "/AnonArticles";

        public const string ReleasesCrudController = Root + "/Releases";
        public const string ArticlesCrudController = Root + "/Articles";

        public const string ArticleQueriesController = Root + "/ArticleQueries";
    }
}
