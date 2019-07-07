using System.Collections.Generic;
using System.Linq;
using cms.Data_Layer.Contexts;
using cms.Data_Layer.Models;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Queries.Handlers
{
    public class GetUserPostsQueryHandler : IQueryHandler<GetUserPostsQuery, List<Post>>
    {
        private readonly ILogger<GetUsersQueryHandler> logger;
        private readonly ApplicationDbContext ctx;

        public GetUserPostsQueryHandler(ILogger<GetUsersQueryHandler> logger, ApplicationDbContext ctx)
        {
            this.logger = logger;
            this.ctx = ctx;
        }

        public List<Post> Handle(GetUserPostsQuery query)
        {
            var userId = query.UserId;
            var user = ctx.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                logger.LogWarning($"Not User entry found for Id [{userId}]");
                return null;
            }

            var userPosts = ctx.Posts.Where(p => p.AuthorId == userId);

            return userPosts.ToList();
        }
    }
}