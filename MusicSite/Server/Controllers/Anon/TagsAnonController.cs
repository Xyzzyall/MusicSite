using MediatR;
using Microsoft.AspNetCore.Mvc;
using MusicSite.Server.Queries.Anon.Tags;

namespace MusicSite.Server.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class TagsAnonController : Controller
    {
        private readonly IMediator _mediator;

        public TagsAnonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Tags
        [HttpGet("like")]
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
