using System.Collections.Generic;

namespace cms.ApplicationLayer
{
    public class CommandResult<T>
    {
        public bool Success;
        public T Response;

        public CommandResult()
        {
            Success = false;
        }
    }
}