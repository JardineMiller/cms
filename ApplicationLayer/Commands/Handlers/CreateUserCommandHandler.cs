using System;
using cms.Data_Layer.Contexts;
using cms.Data_Layer.Models;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Commands.Handlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CommandResponse<User>>
    {
        private readonly ApplicationDbContext ctx;
        private readonly ILogger<CreateUserCommandHandler> logger;

        public CreateUserCommandHandler(ApplicationDbContext ctx, ILogger<CreateUserCommandHandler> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public CommandResponse<User> Handle(CreateUserCommand command)
        {
            var response = new CommandResponse<User>();

            try
            {
                var user = command.User;

                ctx.Users.Add(user);
                ctx.SaveChanges();

                response.Entities.Add(user);
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