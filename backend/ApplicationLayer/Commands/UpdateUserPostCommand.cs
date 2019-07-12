using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Commands
{
    public class UpdateUserPostCommand : ICommand<UpdateUserPostCommand, CommandResult<Post>>
    {
        public int UserId;
        public Post Post;

        public UpdateUserPostCommand(int userId, Post post)
        {
            UserId = userId;
            Post = post;
        }
    }
}