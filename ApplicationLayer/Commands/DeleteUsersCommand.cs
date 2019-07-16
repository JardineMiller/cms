using System.Collections.Generic;

namespace cms.ApplicationLayer.Commands
{
    public class DeleteUsersCommand : ICommand<DeleteUsersCommand, CommandResult<bool>>
    {
        public IList<int> UserIds;

        public DeleteUsersCommand(IList<int> userIds)
        {
            UserIds = userIds;
        }
    }
}