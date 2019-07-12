using System;
using System.Collections.Generic;
using System.Linq;
using cms.Data_Layer.Contexts;
using cms.Data_Layer.Models;
using Microsoft.Extensions.Logging;

namespace cms.ApplicationLayer.Commands.Handlers
{
    public class UpdateUsersCommandHandler : ICommandHandler<UpdateUsersCommand, CommandResponse<User>>
    {
        private readonly ApplicationDbContext ctx;
        private readonly ILogger<UpdateUsersCommandHandler> logger;

        public UpdateUsersCommandHandler(ApplicationDbContext ctx, ILogger<UpdateUsersCommandHandler> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public CommandResponse<User> Handle(UpdateUsersCommand command)
        {
            var response = new CommandResponse<User>();

            try
            {
                var validUsers = ValidateEntries(command.users);
                UpdateUsers(validUsers);

                ctx.SaveChanges();

                response.Entities.AddRange(validUsers);
                response.Success = true;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                throw;
            }

            return response;
        }

        private List<User> ValidateEntries(IList<User> users)
        {
            var updatedUserIds = users.Select(u => u.Id);
            var validUserIds = ctx.Users.Where(u => updatedUserIds.Contains(u.Id)).Select(u => u.Id);
            var invalidIds = updatedUserIds.Except(validUserIds).ToList();

            if (invalidIds.Any())
            {
                var stringIds = string.Join(",", invalidIds);
                logger.LogWarning($"Entries not found for [{invalidIds.Count}] Users. Ids: [{stringIds}]");
            }

            return users.Where(u => validUserIds.Contains(u.Id)).ToList();
        }

        private void UpdateUsers(IEnumerable<User> usersToUpdate)
        {
            foreach (var user in usersToUpdate)
            {
                var dbUser = ctx.Users.Find(user.Id);
                var entry = ctx.Entry(dbUser);
                entry.CurrentValues.SetValues(user);
            }
        }
    }
}