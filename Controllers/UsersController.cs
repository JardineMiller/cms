using System.Collections.Generic;
using System.Linq;
using cms.ApplicationLayer.Commands;
using cms.ApplicationLayer.Commands.Processor;
using cms.ApplicationLayer.Queries;
using cms.ApplicationLayer.Queries.Processor;
using cms.Data_Layer.Models;
using Microsoft.AspNetCore.Mvc;

namespace cms.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandProcessor commandProcessor;

        public UsersController(IQueryProcessor queryProcessor, ICommandProcessor commandProcessor)
        {
            this.queryProcessor = queryProcessor;
            this.commandProcessor = commandProcessor;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var query = new GetUsersQuery();
            var result = queryProcessor.Process(query);

            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(int userId)
        {
            var ids = new List<int>() {userId};

            var query = new GetUsersQuery(ids);
            var result = queryProcessor.Process(query);

            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult DeleteUsers([FromBody] IList<int> userIds)
        {
            var cmd = new DeleteUsersCommand(userIds);
            var response = commandProcessor.Process(cmd);

            if (!response.Success)
            {
                return BadRequest(); //TODO: Not accurate
            }

            return Ok(response.Success);
        }

        [HttpPut]
        public IActionResult UpdateUsers([FromBody] IList<User> users)
        {
            var cmd = new UpdateUsersCommand(users);
            var result = commandProcessor.Process(cmd);

            if (!result.Success)
            {
                return BadRequest(); //TODO: Not accurate
            }

            return Ok(result.Response);
        }

        [HttpPost]
        public IActionResult CreateUsers([FromBody] IList<User> users)
        {
            var cmd = new CreateUsersCommand(users);
            var result = commandProcessor.Process(cmd);

            if (!result.Success)
            {
                return BadRequest(); //TODO: Not accurate
            }

            return Ok(result.Response);
        }

        #region Posts

        [HttpGet("{userId}/posts")]
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

        [HttpPost("{userId}/posts")]
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

        [HttpPut("{userId}/posts")]
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

        [HttpDelete("{userId}/posts")]
        public IActionResult DeleteUserPost(int userId, [FromBody] Post post)
        {
            var cmd = new DeleteUserPostCommand(userId, post);
            var result = commandProcessor.Process(cmd);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result.Response);
        }

        #endregion
    }
}