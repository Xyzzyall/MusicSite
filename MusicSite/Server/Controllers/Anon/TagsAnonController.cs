using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.Server.Queries.Anon.Tags;
using MusicSite.Shared;

namespace MusicSite.Server.Controllers.Anon
{
    [ApiController, Route(Routing.AnonTagsController)]
    public class TagsAnonController : Controller
    {
        private readonly IMediator _mediator;

        public TagsAnonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<string>>> IndexTagsLike(
            [FromQuery] string part,
            CancellationToken cancel
        )
        {
            var query = new TagsLikeQuery(part);
            var result = await _mediator.Send(query, cancel);
            return Ok(result);
        }
    }
}
