using System.Linq;
using cms.Data_Layer;

namespace cms.ApplicationLayer.Commands
{
    public class DeleteUsersCommandHandler : ICommandHandler<DeleteUsersCommand>
    {
        private readonly ApplicationDbContext ctx;

        public DeleteUsersCommandHandler(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Handle(DeleteUsersCommand command)
        {
            var toRemove = ctx.Users.Where(u => command.UserIds.Contains(u.Id));
            ctx.Users.RemoveRange(toRemove);
            ctx.SaveChanges();
        }
    }
}