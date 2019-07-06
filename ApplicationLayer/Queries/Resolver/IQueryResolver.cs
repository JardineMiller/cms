using cms.ApplicationLayer.Queries.Handlers;

namespace cms.ApplicationLayer.Queries.Resolver
{
    public interface IQueryResolver
    {
        IQueryHandler<TQuery, TResult> Resolve<TQuery, TResult>(IQuery<TQuery, TResult> query) where TQuery : IQuery<TQuery, TResult>;

    }
}