using System.Collections.Generic;
using cms.Data_Layer.Models;

namespace cms.ApplicationLayer.Queries
{
    public class GetUsersQuery : IQuery<GetUsersQuery, List<User>>
    {
        public IList<int> userIds;

        public GetUsersQuery(IList<int> userIds  = null)
        {
            this.userIds = userIds;
        }
    }
}