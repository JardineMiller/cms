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

        public TResult Process<TQuery, TResult>(IQuery<TQuery, TResult> request) where TQuery : IQuery<TQuery, TResult>
        {
            var handler = this.resolver.Resolve(request);
            var result = handler.Handle((TQuery)request);
            return result;
        }
    }
}