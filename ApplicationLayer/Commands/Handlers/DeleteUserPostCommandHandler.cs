using System;
using System.Linq;
using cms.Data_Layer.Contexts;
using cms.Data_Layer.Models;
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
                var postId = command.PostId;

                var dbUser = ctx.Users.FirstOrDefault(u => u.Id == userId);
                var dbPost = ctx.Posts.FirstOrDefault(p => p.Id == postId);

                if (dbUser == null)
                {
                    logger.LogWarning($"Unable to find [{nameof(User)}] with Id: [{userId}]");
                    result.Response = false;
                    return result;
                }
                
                if (dbPost == null)
                {
                    logger.LogWarning($"Unable to find [{nameof(Post)}] with Id: [{postId}]");
                    result.Response = false;
                    return result;
                }

                if (dbPost.AuthorId != userId)
                {
                    logger.LogWarning($"Post [{postId}] does not belong [{nameof(User)}] [{userId}]. Refusing the delete request.");
                    result.Response = false;
                    return result;
                }

                ctx.Posts.Remove(dbPost);
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