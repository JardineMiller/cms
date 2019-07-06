using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Commands
{
    public class CreateUserCommand : ICommand<CreateUserCommand, CommandResponse<User>>
    {
        public User User;

        public CreateUserCommand(User user)
        {
            this.User = user;
        }
    }
}