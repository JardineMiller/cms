namespace cms.ApplicationLayer.Queries
{
    public interface IQuery<in TQuery, out TResponse> where TQuery : IQuery<TQuery, TResponse>
    {

    }
}