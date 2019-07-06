using cms.ApplicationLayer.Commands.Resolver;

namespace cms.ApplicationLayer.Commands.Processor
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly ICommandResolver resolver;

        public CommandProcessor(ICommandResolver resolver)
        {
            this.resolver = resolver;
        }

        public TResult Process<TCommand, TResult>(ICommand<TCommand, TResult> command) where TCommand : ICommand<TCommand, TResult>
        {
            var handler = resolver.Resolve(command);
            var result = handler.Handle((TCommand)command);
            return result;
        }
    }
}