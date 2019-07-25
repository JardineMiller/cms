using System;
using System.Collections.Generic;
using System.Linq;
using cms.Data_Layer.Contexts;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Commands.Handlers
{
    public class DeleteUsersCommandHandler : ICommandHandler<DeleteUsersCommand, CommandResult<List<int>>>
    {
        private readonly ApplicationDbContext ctx;
        private readonly ILogger<DeleteUsersCommandHandler> logger;

        public DeleteUsersCommandHandler(ApplicationDbContext ctx, ILogger<DeleteUsersCommandHandler> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public CommandResult<List<int>> Handle(DeleteUsersCommand command)
        {
            var result = new CommandResult<List<int>>();

            try
            {
                var toRemove = ctx.Users.Where(u => command.UserIds.Contains(u.Id));
                var idsToRemove = toRemove.Select(u => u.Id).ToList();
                var userIdsNotFound = command.UserIds.Except(idsToRemove).ToList();

                if (userIdsNotFound.Any())
                {
                    var stringIds = string.Join(", ", userIdsNotFound);
                    logger.LogWarning($"Entries not found for [{userIdsNotFound.Count}] Users. Ids: [{stringIds}]");
                    result.Success = false;
                    return result;
                }

                ctx.Users.RemoveRange(toRemove);
                ctx.SaveChanges();

                result.Success = true;
                result.Response = idsToRemove;
            }
            catch (Exception e)
            {
                logger.LogWarning(e.Message);
            }

            return result;
        }
    }
}