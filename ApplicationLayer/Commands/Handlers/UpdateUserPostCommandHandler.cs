using System;
using System.Linq;
using cms.Data_Layer.Contexts;
using cms.Data_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Commands.Handlers
{
    public class UpdateUserPostCommandHandler : ICommandHandler<UpdateUserPostCommand, CommandResult<Post>>
    {
        private readonly ApplicationDbContext ctx;
        private readonly ILogger<UpdateUserPostCommandHandler> logger;

        public UpdateUserPostCommandHandler(ApplicationDbContext ctx, ILogger<UpdateUserPostCommandHandler> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public CommandResult<Post> Handle(UpdateUserPostCommand command)
        {
            var result = new CommandResult<Post>();
            
            try
            {
                var updatedPost = command.Post;
                var userId = command.UserId;
                
                updatedPost.Timestamp = DateTimeOffset.UtcNow;

                var dbPost = ctx.Posts
                    .Include(p => p.Comments)
                    .SingleOrDefault(p => p.Id == updatedPost.Id);

                if (dbPost == null)
                {
                    logger.LogWarning($"No Post entry found with Id: [{updatedPost.Id}]");
                    result.Response = null;
                    return result;
                }

                if (dbPost.AuthorId != userId)
                {
                    logger.LogWarning($"Post [{updatedPost.Id}] does not belong to to User [{userId}]. Refusing the update.");
                    result.Response = null;
                    return result;
                }
                
                var entry = ctx.Entry(dbPost);
                entry.CurrentValues.SetValues(updatedPost);
                
                dbPost.Comments.Clear();
                foreach (var comment in updatedPost.Comments)
                {
                    Comment existingComment;

                    existingComment = ctx.Comments.SingleOrDefault(c => c.Id == comment.Id);
                    
                    if (existingComment == null)
                    {
                        comment.Timestamp = DateTimeOffset.UtcNow;
                        existingComment = ctx.Comments.Add(comment).Entity;
                    }

                    dbPost.Comments.Add(existingComment);
                }
                
                ctx.SaveChanges();

                result.Success = true;
                result.Response = ctx.Posts
                    .Include(p => p.Author)
                    .Include(p => p.Comments).ThenInclude(c => c.Author)
                    .Include(p => p.Comments).ThenInclude(c => c.Replies).ThenInclude(r => r.Author)
                    .SingleOrDefault(p => p.Id == updatedPost.Id);;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return result;
        }
    }
}