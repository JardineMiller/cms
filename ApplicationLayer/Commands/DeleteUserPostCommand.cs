namespace cms.ApplicationLayer.Commands
{
    public class DeleteUserPostCommand : ICommand<DeleteUserPostCommand, CommandResult<bool>>
    {
        public int UserId;
        public int PostId;

        public DeleteUserPostCommand(int userId, int postId)
        {
            UserId = userId;
            PostId = postId;
        }
    }
}