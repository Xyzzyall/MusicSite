using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicSite.Server.Queries.ArticleQueries;
using MusicSite.Shared;

namespace MusicSite.Server.Controllers.Queries
{
    [ApiController, Route(Routing.ArticleQueriesController), Authorize]
    public class ArticlesQueriesController : Controller
    {
        private readonly IMediator _mediator;

        public ArticlesQueriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> TryGetArticle(
            string title,
            [FromQuery]string language,
            CancellationToken cancellationToken
        )
        {
            var query = new TryGetArticleDetailQuery(title, language);
            var result = await _mediator.Send(query, cancellationToken);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
