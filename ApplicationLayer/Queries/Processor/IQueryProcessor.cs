namespace cms.ApplicationLayer.Queries.Processor
{
    public interface IQueryProcessor
    {
        TResult Process<TQuery, TResult>(IQuery<TQuery, TResult> query) where TQuery : IQuery<TQuery, TResult>;
    }
}