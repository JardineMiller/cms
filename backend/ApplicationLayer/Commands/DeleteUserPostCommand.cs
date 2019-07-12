using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Commands
{
    public class DeleteUserPostCommand : ICommand<DeleteUserPostCommand, CommandResult<bool>>
    {
        public int UserId;
        public Post Post;

        public DeleteUserPostCommand(int userId, Post post)
        {
            UserId = userId;
            Post = post;
        }
    }
}