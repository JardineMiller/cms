namespace cms.ApplicationLayer.Queries.Processor
{
    public interface IQueryProcessor
    {
        TResult Process<TQuery, TResult>(IQuery<TQuery, TResult> request) where TQuery : IQuery<TQuery, TResult>;
    }
}