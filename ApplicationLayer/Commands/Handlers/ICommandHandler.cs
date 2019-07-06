namespace cms.ApplicationLayer.Commands.Handlers
{
    public interface ICommandHandler<in TCommand, out TResponse> where TCommand : ICommand<TCommand, TResponse>
    {
        TResponse Handle(TCommand command);
    }
}