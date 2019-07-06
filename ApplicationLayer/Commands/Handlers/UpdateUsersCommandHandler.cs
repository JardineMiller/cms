using System;
using cms.Data_Layer;
using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Commands
{
    public class UpdateUsersCommandHandler : ICommandHandler<UpdateUsersCommand>
    {
        private readonly ApplicationDbContext ctx;

        public UpdateUsersCommandHandler(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Handle(UpdateUsersCommand command)
        {
            foreach (var updatedUser in command.users)
            {
                UpdateUser(updatedUser);
            }

            ctx.SaveChanges();
        }

        private void UpdateUser(User updatedUser)
        {
            var user = ctx.Users.Find(updatedUser.Id);

            if (user == null) return;

            var entry = ctx.Entry(user);
            entry.CurrentValues.SetValues(updatedUser);
        }
    }
}