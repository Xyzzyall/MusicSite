using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Data.Models;

namespace MusicSite.Server.Data.Repositories.ArticlesRepo
{
    public partial class ArticlesRepository : Repository<Article>, IArticlesRepository
    {
        private readonly ITagsRepository _tagsRepository;

        public ArticlesRepository(MusicSiteServerContext context, ITagsRepository tagsRepository) : base(context)
        {
            this._tagsRepository = tagsRepository;
        }

        public async Task AssociateTagsAsync(Article article, List<string> tags, CancellationToken cancellationToken)
        {
            var tag_entities = await _tagsRepository.GetOrAddTagsAsync(tags, cancellationToken);
            article.Tags = tag_entities;
        }

        public async Task AssociateReleaseByCodenameAsync(Article article, string releaseCodename, CancellationToken cancellationToken)
        {
            article.RelatedRelease = await Context.Release
                .Where(r => r.Codename == releaseCodename && r.Language == article.Language)
                .FirstAsync(cancellationToken);
        }

        public Task<List<Article>> GetArticlesPagedAsync(
            string language, int page, int recordsPerPage,
            CancellationToken cancellationToken,
            List<string>? tags = null,
            Expression<Func<Article, bool>>? additionalPredicate = null
        )
        {
            if (tags is null)
            {
                return FindPagedAsync(
                    article => article.Language == language,
                    page,
                    recordsPerPage,
                    cancellationToken,
                    additionalPredicate
                );
            }

            tags = tags.Select(tag => tag.ToLower()).ToList();
            //todo there maybe a better solution to tagged search
            return FindPagedAsync(
                article => 
                    article.Language == language && 
                    article.Tags.Count(t => tags.Contains(t.Name.ToLower())) == tags.Count,
                page,
                recordsPerPage,
                cancellationToken,
                additionalPredicate
            );
        }

        public Task<Article?> TryToGetArticleAsync(string language, string title, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ArticleExistsAsync(string language, string title, CancellationToken cancellationToken)
        {
            return Context.Article
                .Where(a => a.Language == language && a.Title == title)
                .AnyAsync(cancellationToken);
        }

        public override Task<List<Article>> GetAllPagedAsync(int page, int recordsPerPage, CancellationToken cancel)
        {
            return Context.Article
                .OrderByDescending(e => e.PublishDate)
                .Skip(page * recordsPerPage)
                .Take(recordsPerPage)
                .ToListAsync(cancellationToken: cancel);
        }

        public override Task<List<Article>> FindPagedAsync(Expression<Func<Article, bool>> predicate, int page, int recordsPerPage, CancellationToken cancel, Expression<Func<Article, bool>>? additionalPredicate = null)
        {
            var query = Context.Article
                .Include(a => a.Tags)
                .Where(predicate)
                .OrderByDescending(e => e.PublishDate)
                .Skip(page * recordsPerPage)
                .Take(recordsPerPage);
            if (additionalPredicate is null)
            {
                return query.ToListAsync(cancellationToken: cancel);
            }

            return query.Where(additionalPredicate).ToListAsync(cancellationToken: cancel);
        }
    }
}
