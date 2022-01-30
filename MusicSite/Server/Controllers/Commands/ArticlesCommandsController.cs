using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicSite.Server.Commands;
using MusicSite.Server.Commands.Articles;
using MusicSite.Shared;
using MusicSite.Shared.SharedModels;
using MusicSite.Shared.SharedModels.Anon;

namespace MusicSite.Server.Controllers.Commands
{
    [ApiController, Route(Routing.ArticlesCrudController), Authorize]
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
            [FromBody] ArticleCreate article,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation("CreateArticle command. Input data: {Article}", article);
            var command = new CreateArticleCommand(article);
            try
            {
                var result_raw = await _mediator.Send(command, cancellationToken);
                if (result_raw.ValidationFailed())
                {
                    return BadRequest(result_raw.ValidationFailuresStringArray());
                }
                var result = ValidatedResponse<int>.TryCastResponse(result_raw).Result;
                _logger.LogInformation("Created article ({Article}) with id={Result}", article, result_raw);
                return Ok(result);
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating article ({Article}), details: {Message}", article, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateArticle(
            int id, 
            [FromBody] ArticleSharedEditMode article,
            CancellationToken cancellationToken
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
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating article (id={Id}, data={Atricle}), details: {Message}", id, article, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteArticle(
            int id,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation("DeleteArticle(id={Id}) command", id);
            var command = new DeleteArticleCommand(id);
            try
            {
                await _mediator.Send(command, cancellationToken);
                _logger.LogInformation("Article (id={Id}) deleted", id);
                return Ok();
            }
            catch (TaskCanceledException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting article (id={Id}), details: {Message}", id, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
