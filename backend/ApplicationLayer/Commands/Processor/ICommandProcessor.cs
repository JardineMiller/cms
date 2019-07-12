namespace cms.ApplicationLayer.Commands.Processor
{
    public interface ICommandProcessor
    {
        TResult Process<TCommand, TResult>(ICommand<TCommand, TResult> request)where TCommand : ICommand<TCommand, TResult>;
    }
}