namespace cms.ApplicationLayer.Commands.Processor
{
    public interface ICommandProcessor
    {
        TResult Process<TCommand, TResult>(ICommand<TCommand, TResult> command)where TCommand : ICommand<TCommand, TResult>;
    }
}