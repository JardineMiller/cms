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
        public IActionResult GetUser(long userId)
        {
            var ids = new List<long>() {userId};

            var query = new GetUsersQuery(ids);
            var result = queryProcessor.Process(query);

            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result.First());
        }

        [HttpDelete]
        public IActionResult DeleteUsers([FromQuery] IList<long> userIds)
        {
            var cmd = new DeleteUsersCommand(userIds);
            var response = commandProcessor.Process(cmd);

            if (!response.Success)
            {
                return BadRequest(); //TODO: Not accurate
            }

            return Ok(response.Response);
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
    }
}