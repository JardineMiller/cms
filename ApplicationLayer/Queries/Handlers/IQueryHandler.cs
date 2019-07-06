namespace cms.ApplicationLayer.Queries.Handlers
{
    public interface IQueryHandler<in TQuery, out TResponse> where TQuery : IQuery<TQuery, TResponse>
    {
        TResponse Handle(TQuery query);
    }
}