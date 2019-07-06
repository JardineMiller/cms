using System;
using System.Collections.Generic;
using System.Net.Http;
using cms.ApplicationLayer;
using cms.ApplicationLayer.Commands;
using cms.ApplicationLayer.Commands.Responses;
using cms.ApplicationLayer.Queries;
using cms.Data_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cms.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IQueryHandler<GetUsersQuery, IEnumerable<User>> getUsersQueryHandler;
        private readonly ICommandHandler<DeleteUsersCommand> deleteUsersCommandHandler;
        private readonly ICommandHandler<UpdateUsersCommand> updateUsersCommandHandler;
        private readonly ICommandHandler<CreateUserCommand, CommandResponse> createUserCommandHandler;

        public UsersController(
            IQueryHandler<GetUsersQuery, IEnumerable<User>> getUsersQueryHandler,
            ICommandHandler<UpdateUsersCommand> updateUsersCommandHandler,
            ICommandHandler<DeleteUsersCommand> deleteUsersCommandHandler,
            ICommandHandler<CreateUserCommand, CommandResponse> createUserCommandHandler
            )
        {
            this.getUsersQueryHandler = getUsersQueryHandler;
            this.deleteUsersCommandHandler = deleteUsersCommandHandler;
            this.updateUsersCommandHandler = updateUsersCommandHandler;
            this.createUserCommandHandler = createUserCommandHandler;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var query = new GetUsersQuery();
            var result = getUsersQueryHandler.Handle(query);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser([FromQuery] int userId)
        {
            var ids = new List<int>() {userId};

            var query = new GetUsersQuery(ids);
            var result = getUsersQueryHandler.Handle(query);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult DeleteUsers([FromBody] IEnumerable<int> userIds)
        {
            //TODO: Try catch should be within the command handler
            try
            {
                var cmd = new DeleteUsersCommand(userIds);
                deleteUsersCommandHandler.Handle(cmd);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult UpdateUsers([FromBody] IEnumerable<User> users)
        {
            //TODO: Try catch should be within the command handler
            try
            {
                var cmd = new UpdateUsersCommand(users);
                updateUsersCommandHandler.Handle(cmd);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            var cmd = new CreateUserCommand(user);
            var result = createUserCommandHandler.Handle(cmd);

            if (!result.Success) {
                return BadRequest(); //TODO: Not accurate
            }

            user.Id = result.Id;
            return Ok(user);
        }
    }
}