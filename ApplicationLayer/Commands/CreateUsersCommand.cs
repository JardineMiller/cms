using System.Collections.Generic;
using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Commands
{
    public class CreateUsersCommand : ICommand<CreateUsersCommand, CommandResponse<User>>
    {
        public IList<User> Users;

        public CreateUsersCommand(IList<User> users)
        {
            this.Users = users;
        }
    }
}