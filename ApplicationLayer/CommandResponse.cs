using System.Collections.Generic;

namespace cms.ApplicationLayer
{
    public class CommandResponse<T>
    {
        public bool Success;
        public List<T> Entities;

        public CommandResponse()
        {
            Success = false;
            Entities = new List<T>();
        }
    }

    public class CommandResponse
    {
        public bool Success;

        public CommandResponse()
        {
            Success = false;
        }
    }
}