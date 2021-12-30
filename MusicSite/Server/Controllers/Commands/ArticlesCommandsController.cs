using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicSite.Shared.SharedModels;

namespace MusicSite.Server.Controllers.Commands
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ArticlesCommandsController : Controller
    {
        private readonly ILogger<ArticlesCommandsController> _logger;
        private readonly IMediator _mediator;

        public ArticlesCommandsController(ILogger<ArticlesCommandsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateArticle(
            [FromBody] ArticleSharedEditMode article
        )
        {
            
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(
            int id, 
            [FromBody] ArticleSharedEditMode article
        )
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            throw new NotImplementedException();
        }
    }
}
