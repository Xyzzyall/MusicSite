using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.Server.Queries.Anon.Article;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class ArticlesAnonController : Controller
    {
        private readonly ILogger<ArticlesAnonController> _logger;
        private readonly IMediator _mediator;
        public ArticlesAnonController(ILogger<ArticlesAnonController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("index")]
        public async Task<ActionResult<ArticleSharedIndex[]>> Index(
            CancellationToken cancel,
            [FromQuery] string language,
            [FromQuery] int page = 0,
            [FromQuery] int recordsPerPage = 10
        )
        {
            var query = new IndexArticlesQuery(language, page, recordsPerPage);
            var result = await _mediator.Send(query, cancel);
            return Ok(result);
        }

        [HttpGet("index/byTags")]
        public async Task<ActionResult<ArticleSharedIndex[]>> IndexByTags(
            CancellationToken cancel,
            [FromQuery] string language,
            [FromQuery] List<string> tags,
            [FromQuery] int page = 0,
            [FromQuery] int records_per_page = 20
        )
        {
            var query = new IndexArticlesByTagsQuery(language, tags, page, records_per_page);
            var result = await _mediator.Send(query, cancel);
            return Ok(result);
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<ArticleSharedIndex>> Details(
            CancellationToken cancel,
            [FromQuery] string language,
            string title
        )
        {
            var query = new ArticleDetailsQuery(language, title);
            var result = await _mediator.Send(query, cancel);

            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("exists/{title}")]
        public async Task<ActionResult<bool>> ArticleExists(
            CancellationToken cancel,
            [FromQuery] string language,
            string title
        )
        {
            var query = new ArticleExistsQuery(language, title);
            var result = await _mediator.Send(query, cancel);
            return Ok(result);
        }
    }
}
