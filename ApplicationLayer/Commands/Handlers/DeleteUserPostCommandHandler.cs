using System;
using System.Linq;
using cms.Data_Layer.Contexts;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Commands.Handlers
{
    public class DeleteUserPostCommandHandler : ICommandHandler<DeleteUserPostCommand, CommandResult<bool>>
    {
        private readonly ApplicationDbContext ctx;
        private readonly ILogger<DeleteUserPostCommandHandler> logger;

        public DeleteUserPostCommandHandler(ApplicationDbContext ctx, ILogger<DeleteUserPostCommandHandler> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public CommandResult<bool> Handle(DeleteUserPostCommand command)
        {
            var result = new CommandResult<bool>();

            try
            {
                var userId = command.UserId;
                var postToDelete = command.Post;

                var dbUser = ctx.Users.FirstOrDefault(u => u.Id == userId);

                if (dbUser == null)
                {
                    logger.LogWarning($"Unable to find User with Id: [{userId}]");
                    result.Response = false;
                    return result;
                }

                if (postToDelete.AuthorId != userId)
                {
                    logger.LogWarning($"Post [{postToDelete.Id}] does not belong to to User [{userId}]. Refusing the delete request.");
                    result.Response = false;
                    return result;
                }

                ctx.Posts.Remove(postToDelete);
                ctx.SaveChanges();

                result.Success = true;
                result.Response = true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return result;
        }
    }
}