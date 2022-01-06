using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicSite.Server.Commands.Articles;
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
            CancellationToken cancellationToken,
            [FromBody] ArticleSharedEditMode article
        )
        {
            _logger.LogInformation("CreateArticle command. Input data: {Article}", article);
            var command = new CreateArticleCommand(article);
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                _logger.LogInformation("Created article ({Article}) with id={Result}", article, result);
                return Ok(result);
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating article ({Atricle}), details: {Message}", article, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(
            CancellationToken cancellationToken,
            int id, 
            [FromBody] ArticleSharedEditMode article
        )
        {
            _logger.LogInformation("UpdateArticle(id={Id}) command. Input data: {Article}", id, article);
            var command = new UpdateArticleCommand(id, article);
            try 
            {
                await _mediator.Send(command, cancellationToken);
                _logger.LogInformation("Updated article ({Article}) with id={Id}", article, id);
                return Ok();
            }
            catch(TaskCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating article (id={Id}, data={Atricle}), details: {Message}", id, article, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(
            CancellationToken cancellationToken,
            int id
        )
        {
            _logger.LogInformation("DeleteArticle(id={Id}) command.", id);
            var command = new DeleteArticleCommand(id);
            try
            {
                await _mediator.Send(command, cancellationToken);
                _logger.LogInformation("Article (id={Id}) deleted.", id);
                return Ok();
            }
            catch (TaskCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting article (id={Id}), details: {Message}", id, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
