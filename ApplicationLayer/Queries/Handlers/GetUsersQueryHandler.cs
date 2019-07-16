using System.Collections.Generic;
using System.Linq;
using cms.Data_Layer.Contexts;
using cms.Data_Layer.Models;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Queries.Handlers
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<User>>
    {
        private readonly ApplicationDbContext ctx;
        private readonly ILogger<GetUsersQueryHandler> logger;

        public GetUsersQueryHandler(ApplicationDbContext ctx, ILogger<GetUsersQueryHandler> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public List<User> Handle(GetUsersQuery query)
        {
            if (query.userIds == null || !query.userIds.Any())
            {
                return ctx.Users.ToList();
            }

            var usersFound = ctx.Users.Where(u => query.userIds.Contains(u.Id));
            var userIdsNotFound = query.userIds.Except(usersFound.Select(u => u.Id)).ToList();

            if (userIdsNotFound.Any())
            {
                var stringIds = string.Join(", ", userIdsNotFound);
                logger.LogWarning($"Entries not found for [{userIdsNotFound.Count}] Users. Ids: [{stringIds}]");
            }

            return usersFound.ToList();
        }
    }
}