using System.Collections.Generic;
using System.Linq;
using cms.Data_Layer;
using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Queries
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly ApplicationDbContext ctx;

        public GetUsersQueryHandler(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public IEnumerable<User> Handle(GetUsersQuery query)
        {
            if (!query.userIds.Any())
            {
                return ctx.Users;
            }

            return ctx.Users.Where(u => query.userIds.Contains(u.Id));
        }
    }
}