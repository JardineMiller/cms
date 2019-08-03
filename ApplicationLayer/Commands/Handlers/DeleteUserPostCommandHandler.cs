using System;
using System.Linq;
using cms.Data_Layer.Contexts;
using cms.Data_Layer.Models;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Commands.Handlers
{
    public class DeleteUserPostCommandHandler : ICommandHandler<DeleteUserPostCommand, CommandResult<long?>>
    {
        private readonly ApplicationDbContext ctx;
        private readonly ILogger<DeleteUserPostCommandHandler> logger;

        public DeleteUserPostCommandHandler(ApplicationDbContext ctx, ILogger<DeleteUserPostCommandHandler> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public CommandResult<long?> Handle(DeleteUserPostCommand command)
        {
            var result = new CommandResult<long?>();

            try
            {
                var userId = command.UserId;
                var postId = command.PostId;

                var dbUser = ctx.Users.FirstOrDefault(u => u.Id == userId);
                var dbPost = ctx.Posts.FirstOrDefault(p => p.Id == postId);

                if (dbUser == null)
                {
                    logger.LogWarning($"Unable to find [{nameof(User)}] with Id: [{userId}]");
                    result.Success = false;
                    return result;
                }
                
                if (dbPost == null)
                {
                    logger.LogWarning($"Unable to find [{nameof(Post)}] with Id: [{postId}]");
                    result.Success = false;
                    return result;
                }

                if (dbPost.AuthorId != userId)
                {
                    logger.LogWarning($"Post [{postId}] does not belong to [{nameof(User)}] [{userId}]. Refusing the delete request.");
                    result.Success = false;
                    return result;
                }

                ctx.Posts.Remove(dbPost);
                ctx.SaveChanges();

                result.Success = true;
                result.Response = postId;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return result;
        }
    }
}