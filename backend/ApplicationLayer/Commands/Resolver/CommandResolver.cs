using System;
using cms.ApplicationLayer.Commands.Handlers;

namespace cms.ApplicationLayer.Commands.Resolver
{
    public class CommandResolver : ICommandResolver
    {
        private readonly IServiceProvider provider;

        public CommandResolver(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public ICommandHandler<TCommand, TResult> Resolve<TCommand, TResult>(ICommand<TCommand, TResult> command) where TCommand : ICommand<TCommand, TResult>
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            var handler = provider.GetService(handlerType);
            return handler as ICommandHandler<TCommand, TResult>;
        }
    }
}