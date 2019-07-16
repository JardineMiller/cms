using System;
using cms.Data_Layer.Contexts;
using cms.Data_Layer.Models;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Commands.Handlers
{
    public class CreateUserPostCommandHandler : ICommandHandler<CreateUserPostCommand, CommandResult<Post>>
    {
        private readonly ApplicationDbContext ctx;
        private readonly ILogger<CreateUserPostCommandHandler> logger;

        public CreateUserPostCommandHandler(ApplicationDbContext ctx, ILogger<CreateUserPostCommandHandler> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public CommandResult<Post> Handle(CreateUserPostCommand command)
        {
            var result = new CommandResult<Post>();

            try
            {
                var post = command.Post;
                ctx.Posts.Add(post);
                ctx.SaveChanges();

                result.Success = true;
                result.Response = post;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }

            return result;
        }
    }
}