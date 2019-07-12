using System;
using cms.ApplicationLayer.Queries.Handlers;

namespace cms.ApplicationLayer.Queries.Resolver
{
    public class QueryResolver : IQueryResolver
    {
        private readonly IServiceProvider serviceProvider;

        public QueryResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IQueryHandler<TQuery, TResult> Resolve<TQuery, TResult>(IQuery<TQuery, TResult> query) where TQuery : IQuery<TQuery, TResult>
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handler = this.serviceProvider.GetService(handlerType);
            return handler as IQueryHandler<TQuery, TResult>;
        }
    }
}