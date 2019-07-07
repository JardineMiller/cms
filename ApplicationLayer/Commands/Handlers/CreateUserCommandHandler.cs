using System;
using System.Collections.Generic;
using System.Linq;
using cms.Data_Layer.Contexts;
using cms.Data_Layer.Models;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Commands.Handlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUsersCommand, CommandResult<List<User>>>
    {
        private readonly ApplicationDbContext ctx;
        private readonly ILogger<CreateUserCommandHandler> logger;

        public CreateUserCommandHandler(ApplicationDbContext ctx, ILogger<CreateUserCommandHandler> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public CommandResult<List<User>> Handle(CreateUsersCommand command)
        {
            var result = new CommandResult<List<User>>();

            try
            {
                var users = command.Users;

                ctx.Users.AddRange(users);
                ctx.SaveChanges();

                result.Response = users.ToList();
                result.Success = true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return result;
        }
    }
}