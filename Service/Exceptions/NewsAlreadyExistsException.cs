using System;

namespace Service.Exceptions
{
    public class NewsAlreadyExistsException:Exception
    {
        public NewsAlreadyExistsException(string message):base(message)
        {

        }
    }
}
