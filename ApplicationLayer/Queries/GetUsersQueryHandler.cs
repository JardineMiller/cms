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
            return ctx.Users;
        }
    }
}