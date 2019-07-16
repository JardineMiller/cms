using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Commands
{
    public class CreateUserPostCommand : ICommand<CreateUserPostCommand, CommandResult<Post>>
    {
        public int UserId;
        public Post Post;

        public CreateUserPostCommand(int userId, Post post)
        {
            UserId = userId;
            Post = post;
        }
    }
}