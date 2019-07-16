using System.Collections.Generic;
using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Queries
{
    public class GetUserPostsQuery : IQuery<GetUserPostsQuery, List<Post>>
    {
        public int UserId;

        public GetUserPostsQuery(int userId)
        {
            UserId = userId;
        }
    }
}