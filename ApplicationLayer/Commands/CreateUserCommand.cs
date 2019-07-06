using cms.ApplicationLayer.Commands.Responses;
using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Commands
{
    public class CreateUserCommand : ICommand<CommandResponse>
    {
        public User User;

        public CreateUserCommand(User user)
        {
            this.User = user;
        }
    }
}