using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Exceptions
{
    public class ReminderNotFoundException:Exception
    {
        public ReminderNotFoundException(string message) : base(message)
        {

        }
    }
}
