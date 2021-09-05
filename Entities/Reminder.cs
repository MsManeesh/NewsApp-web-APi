using System;
namespace Entities
{
    // The class "Reminder" will be acting as the data model for the Reminder Table in the database.
    public class Reminder
    {
        /*
        * This class should have three properties
        * (ReminderId,Schedule,NewsId). 
        * Out of these three fields,
        * 1.the field ReminderId returns a integer data type
        * 2.the field Schedule returns a DateTime data type
        * 3.the field NewsId returns a string data type
        */
        public int ReminderId { get; set; }
        public DateTime Schedule { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
    }
}
