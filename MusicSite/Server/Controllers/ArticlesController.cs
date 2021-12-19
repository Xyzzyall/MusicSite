using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicSite.Server.Data;
using MusicSite.Server.Models;
using MusicSite.Shared.SharedModels;
using MusicSite.Server.Transformations.FromDbModelToShared;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using MusicSite.Server.Queries.Article;

namespace MusicSite.Server.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private readonly MusicSiteServerContext _context;
        private readonly ILogger<ArticlesController> _logger;
        private readonly IMediator _mediator;
        public ArticlesController(MusicSiteServerContext context, ILogger<ArticlesController> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<ArticleSharedIndex[]>> Index(
            [FromBody] IndexArticlesQuery query,
            CancellationToken cancel
        )
        {
            var result = await _mediator.Send(query, cancel);
            return View(result);
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<ArticleSharedIndex[]>> IndexByTags(
            CancellationToken cancel, 
            string language, 
            ICollection<string> tags, 
            int page = 0, 
            int records_per_page = 20
        )
        {
            var query = _context.Article
                .Where(
                    article => article.Language == language 
                    && article.Tags.Where(t => tags.Contains(t.Name)).Count() == tags.Count    //todo: there maybe some workaround
                 )
                .Skip(page * records_per_page)
                .Take(records_per_page);

            var query_result = await query.ToArrayAsync(cancel);
            var shared_articles = TransformArticlesIndex(query_result);

            return View(shared_articles);
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<ArticleSharedIndex>> Details(
            CancellationToken cancel,
            string language,
            string title
        )
        {
            var query = _context.Article
                .Where(article => article.Language == language && article.Title == title);

            try
            {
                var query_result = await query.FirstAsync(cancel);

                ArticleSharedIndex article_shared = new ToArticleSharedDetail(query_result);

                return View(article_shared);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<bool>> ArticleExists(
            CancellationToken cancel,
            string language,
            string title
        )
        {
            var exists = await _context.Article
                .AnyAsync(article => article.Language == language && article.Title == title, cancel);

            return View(exists);
        }

        private static ArticleSharedIndex[] TransformArticlesIndex(Article[] query_result)
        {
            return query_result.Select(
                article => new ToArticleSharedIndex(article)
            ).ToArray();
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticle(ArticleSharedEditMode article)
        {
            throw new NotImplementedException();
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateArticle(int id, ArticleSharedEditMode article)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            throw new NotImplementedException();
        }
    }
}
