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
            var posts = ctx.Posts
                .Include(p => p.Author)
                .Include(p => p.Comments)
                .ToList();

            //TODO: Come back to this as a DB hit for each comment is horrific
            foreach (var post in posts)
            {
                post.Comments = LoadComments(post);
            }

            return posts;
        }

        private List<Comment> LoadComments(Post post)
        {
            var resolvedComments = new List<Comment>();

            foreach (var comment in post.Comments)
            {
                resolvedComments.Add(LoadComment(comment));
            }

            return resolvedComments;
        }

        private Comment LoadComment(Comment comment)
        {
            var dbComment = ctx.Comments
                .Include(c => c.Author)
                .Include(c => c.Replies).ThenInclude(r => r.Author)
                .SingleOrDefault(c => c.Id == comment.Id);

            var replies = new List<Comment>();

            foreach (var reply in dbComment.Replies)
            {
                replies.Add(LoadComment(reply));
            }

            dbComment.Replies = replies;

            return dbComment;
        }
    }
}