using System;
using System.Linq;
using cms.Data_Layer.Contexts;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Commands.Handlers
{
    public class DeleteUsersCommandHandler : ICommandHandler<DeleteUsersCommand, CommandResponse>
    {
        private readonly ApplicationDbContext ctx;
        private readonly ILogger<DeleteUsersCommandHandler> logger;

        public DeleteUsersCommandHandler(ApplicationDbContext ctx, ILogger<DeleteUsersCommandHandler> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public CommandResponse Handle(DeleteUsersCommand command)
        {
            var response = new CommandResponse();

            try
            {
                var toRemove = ctx.Users.Where(u => command.UserIds.Contains(u.Id));
                var userIdsNotFound = command.UserIds.Except(toRemove.Select(u => u.Id)).ToList();

                if (userIdsNotFound.Any())
                {
                    var stringIds = string.Join(", ", userIdsNotFound);
                    logger.LogWarning($"Entries not found for [{userIdsNotFound.Count}] Users. Ids: [{stringIds}]");
                }

                ctx.Users.RemoveRange(toRemove);
                ctx.SaveChanges();

                response.Success = true;
            }
            catch (Exception e)
            {
                logger.LogWarning(e.Message);
                throw;
            }

            return response;
        }
    }
}