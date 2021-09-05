using System;

namespace Service.Exceptions
{
    public class ReminderAlreadyExistsException:Exception
    {
        public ReminderAlreadyExistsException(string message):base(message)
        {

        }
    }
}
