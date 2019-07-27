using cms.ApplicationLayer.Commands;
using cms.ApplicationLayer.Commands.Processor;
using cms.ApplicationLayer.Queries;
using cms.ApplicationLayer.Queries.Processor;
using cms.Data_Layer.Models;
using Microsoft.AspNetCore.Mvc;

namespace cms.Controllers
{
    [Produces("application/json")]
    [Route("api/posts")]
    public class PostsController : Controller
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandProcessor commandProcessor;


        public PostsController(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor)
        {
            this.queryProcessor = queryProcessor;
            this.commandProcessor = commandProcessor;
        }

        [HttpGet("posts")]
        public IActionResult GetAllPosts()
        {
            var query = new GetPostsQuery();
            var result = queryProcessor.Process(query);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserPosts(int userId)
        {
            var query = new GetUserPostsQuery(userId);
            var result = queryProcessor.Process(query);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost("user/{userId}")]
        public IActionResult CreateUserPost(int userId, [FromBody] Post post)
        {
            var cmd = new CreateUserPostCommand(userId, post);
            var result = commandProcessor.Process(cmd);

            if (!result.Success)
            {
                return BadRequest(); //TODO: Not accurate
            }

            return Ok(result.Response);

        }

        [HttpPut("user/{userId}")]
        public IActionResult UpdateUserPost(int userId, [FromBody] Post post)
        {
            var cmd = new UpdateUserPostCommand(userId, post);
            var result = commandProcessor.Process(cmd);

            if (!result.Success)
            {
                return BadRequest(); //TODO: Not accurate
            }

            return Ok(result.Response);
        }

        [HttpDelete("user/{userId}")]
        public IActionResult DeleteUserPost(int userId, [FromQuery] int postId)
        {
            var cmd = new DeleteUserPostCommand(userId, postId);
            var result = commandProcessor.Process(cmd);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result.Response);
        }
    }
}