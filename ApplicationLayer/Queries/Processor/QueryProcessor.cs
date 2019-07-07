using cms.ApplicationLayer.Queries.Resolver;

namespace cms.ApplicationLayer.Queries.Processor
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IQueryResolver resolver;

        public QueryProcessor(IQueryResolver resolver)
        {
            this.resolver = resolver;
        }

        public TResult Process<TQuery, TResult>(IQuery<TQuery, TResult> query) where TQuery : IQuery<TQuery, TResult>
        {
            var handler = this.resolver.Resolve(query);
            var result = handler.Handle((TQuery)query);
            return result;
        }
    }
}