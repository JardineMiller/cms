using System.Collections.Generic;
using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Commands
{
    public class UpdateUsersCommand : ICommand<UpdateUsersCommand, CommandResult<List<User>>>
    {
        public IList<User> users;

        public UpdateUsersCommand(IList<User> users)
        {
            this.users = users;
        }
    }
}