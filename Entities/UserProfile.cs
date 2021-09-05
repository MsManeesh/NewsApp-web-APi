using System;
using System.Collections.Generic;

namespace Entities
{
    //The class "UserProfile" will be acting as the data model for the User Table in the database.
    public class UserProfile
    {
        /*
        * This class should have six properties
        * (UserId,FirstName,LastName,Contact,Email,CreatedAt) 
        * Out of these six fields,
        * 1.the field UserId returns a string data type
        * 2.the field FirstName returns a string data type
        * 3.the field LastName returns a string data type
        * 4.the field Contact returns a string data type 
        * 5.the field Email returns a string data type
        * 6.the filed CreatedAt returns a DateTime data type
        */
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<News> News { get; set; }
    }
}
