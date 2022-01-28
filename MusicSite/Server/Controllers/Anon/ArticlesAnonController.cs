using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.Server.Queries.Anon.Article;
using MusicSite.Shared;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Controllers.Anon
{
    [ApiController, Route(Routing.AnonArticlesController)]
    public class ArticlesAnonController : Controller
    {
        private readonly ILogger<ArticlesAnonController> _logger;
        private readonly IMediator _mediator;
        public ArticlesAnonController(ILogger<ArticlesAnonController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<ArticleSharedIndex[]>> Index(
            [FromQuery] string language,
            CancellationToken cancellationToken,
            [FromQuery] List<string>? tags = null,
            [FromQuery] int page = 0,
            [FromQuery] int recordsPerPage = 10
        )
        {
            if (tags is null || !tags.Any())
            {
                var query = new IndexArticlesQuery(language, page, recordsPerPage);
                var result = await _mediator.Send(query, cancellationToken);
                return Ok(result);
            }

            var query_tags = new IndexArticlesByTagsQuery(language, tags, page, recordsPerPage);
            var result_tags = await _mediator.Send(query_tags, cancellationToken);
            return Ok(result_tags);
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<ArticleSharedIndex>> Details(
            [FromQuery] string language,
            string title,
            CancellationToken cancel
        )
        {
            var query = new ArticleDetailsQuery(language, title);
            var result = await _mediator.Send(query, cancel);

            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("exists/{title}")]
        public async Task<ActionResult<bool>> ArticleExists(
            [FromQuery] string language,
            string title,
            CancellationToken cancel
        )
        {
            var query = new ArticleExistsQuery(language, title);
            var result = await _mediator.Send(query, cancel);
            return Ok(result);
        }
    }
}
