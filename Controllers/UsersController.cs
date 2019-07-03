using System;
using System.Collections.Generic;
using System.Net.Http;
using cms.ApplicationLayer;
using cms.ApplicationLayer.Commands;
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
        private IQueryHandler<GetUsersQuery, IEnumerable<User>> getUsersQueryHandler;
        private ICommandHandler<DeleteUsersCommand> deleteUsersCommandHandler;
        private ICommandHandler<UpdateUsersCommand> updateUsersCommandHandler;

        public UsersController(
            IQueryHandler<GetUsersQuery, IEnumerable<User>> getUsersQueryHandler,
            ICommandHandler<UpdateUsersCommand> updateUsersCommandHandler,
            ICommandHandler<DeleteUsersCommand> deleteUsersCommandHandler)
        {
            this.getUsersQueryHandler = getUsersQueryHandler;
            this.deleteUsersCommandHandler = deleteUsersCommandHandler;
            this.updateUsersCommandHandler = updateUsersCommandHandler;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var query = new GetUsersQuery();
            var result = getUsersQueryHandler.Handle(query);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult DeleteUsers([FromBody] IEnumerable<int> userIds)
        {
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
    }
}