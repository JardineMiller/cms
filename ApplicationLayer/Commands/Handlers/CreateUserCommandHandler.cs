using System;
using cms.ApplicationLayer.Commands.Responses;
using cms.Data_Layer;
using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Commands
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CommandResponse>
    {
        private readonly ApplicationDbContext ctx;

        public CreateUserCommandHandler(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public CommandResponse Handle(CreateUserCommand command)
        {
            var response = new CommandResponse();

            try
            {
                var user = command.User;

                ctx.Users.Add(user);
                ctx.SaveChanges();

                response.Id = user.Id;
                response.Success = true;


            }
            catch (Exception e)
            {
                // handle ho ho
            }

            return response;
        }
    }
}