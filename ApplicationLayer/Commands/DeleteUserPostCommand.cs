namespace cms.ApplicationLayer.Commands
{
    public class DeleteUserPostCommand : ICommand<DeleteUserPostCommand, CommandResult<long?>>
    {
        public long UserId;
        public long PostId;

        public DeleteUserPostCommand(long userId, long postId)
        {
            UserId = userId;
            PostId = postId;
        }
    }
}