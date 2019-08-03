using System.Collections.Generic;
using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Queries
{
    public class GetUsersQuery : IQuery<GetUsersQuery, List<User>>
    {
        public IList<long> userIds;

        public GetUsersQuery(IList<long> userIds  = null)
        {
            this.userIds = userIds;
        }
    }
}