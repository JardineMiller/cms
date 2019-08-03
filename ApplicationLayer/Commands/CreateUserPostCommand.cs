using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Commands
{
    public class CreateUserPostCommand : ICommand<CreateUserPostCommand, CommandResult<Post>>
    {
        public long UserId;
        public Post Post;

        public CreateUserPostCommand(long userId, Post post)
        {
            UserId = userId;
            Post = post;
        }
    }
}