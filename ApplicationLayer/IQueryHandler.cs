using cms.ApplicationLayer.Queries;

namespace cms.ApplicationLayer
{
    public interface IQueryHandler<in TQuery, out TResponse> where TQuery : IQuery<TResponse>
    {
        TResponse Handle(TQuery query);
    }
}