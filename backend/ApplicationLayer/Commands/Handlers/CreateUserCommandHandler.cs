using System;
using cms.Data_Layer.Contexts;
using cms.Data_Layer.Models;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Commands.Handlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUsersCommand, CommandResponse<User>>
    {
        private readonly ApplicationDbContext ctx;
        private readonly ILogger<CreateUserCommandHandler> logger;

        public CreateUserCommandHandler(ApplicationDbContext ctx, ILogger<CreateUserCommandHandler> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public CommandResponse<User> Handle(CreateUsersCommand command)
        {
            var response = new CommandResponse<User>();

            try
            {
                var users = command.Users;

                ctx.Users.AddRange(users);
                ctx.SaveChanges();

                response.Entities.AddRange(users);
                response.Success = true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                throw;
            }

            return response;
        }
    }
}