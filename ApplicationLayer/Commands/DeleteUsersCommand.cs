using System.Collections.Generic;

namespace cms.ApplicationLayer.Commands
{
    public class DeleteUsersCommand : ICommand
    {
        public IEnumerable<int> UserIds;

        public DeleteUsersCommand(IEnumerable<int> userIds)
        {
            UserIds = userIds;
        }
    }
}