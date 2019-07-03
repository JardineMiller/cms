using System.Collections.Generic;
using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Commands
{
    public class UpdateUsersCommand : ICommand
    {
        public IEnumerable<User> users;

        public UpdateUsersCommand(IEnumerable<User> users)
        {
            this.users = users;
        }
    }
}