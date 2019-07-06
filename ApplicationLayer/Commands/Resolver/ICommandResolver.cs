using cms.ApplicationLayer.Commands.Handlers;

namespace cms.ApplicationLayer.Commands.Resolver
{
    public interface ICommandResolver
    {
        ICommandHandler<TCommand, TResult> Resolve<TCommand, TResult>(ICommand<TCommand, TResult> command) where TCommand : ICommand<TCommand, TResult>;
    }
}