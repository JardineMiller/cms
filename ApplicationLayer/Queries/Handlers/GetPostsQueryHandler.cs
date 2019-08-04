using System.Collections.Generic;
using System.Linq;
using cms.Data_Layer.Contexts;
using cms.Data_Layer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Queries.Handlers
{
    public class GetPostsQueryHandler : IQueryHandler<GetPostsQuery, List<Post>>
    {
        private readonly ILogger<GetPostsQueryHandler> logger;
        private readonly ApplicationDbContext ctx;

        public GetPostsQueryHandler(ILogger<GetPostsQueryHandler> logger, ApplicationDbContext ctx)
        {
            this.logger = logger;
            this.ctx = ctx;
        }

        public List<Post> Handle(GetPostsQuery query)
        {
            return ctx.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments).ThenInclude(c => c.Author)
                .Include(p => p.Comments).ThenInclude(c => c.Replies).ThenInclude(r => r.Author)
                .ToList();
        }
    }
}