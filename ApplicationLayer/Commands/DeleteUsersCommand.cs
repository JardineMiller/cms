using System.Collections.Generic;

namespace cms.ApplicationLayer.Commands
{
    public class DeleteUsersCommand : ICommand<DeleteUsersCommand, CommandResult<List<long>>>
    {
        public IList<long> UserIds;

        public DeleteUsersCommand(IList<long> userIds)
        {
            UserIds = userIds;
        }
    }
}