using System;
using System.Collections.Generic;
using System.Text;
namespace Entities
{
    // The class "News" will be acting as the data model for the News Table in the database.
    public class News
    {
        /*
        * This class should have seven properties
        * (NewsId,Title,Content,PublishedAt,CreatedBy,Url,UrlToImage). 
        * Out of these seven fields,
        * 1.the field NewsId returns a integer data type
        * 2.the field Title returns a string data type
        * 3.the field Content returns a string data type
        * 4.the field PublishedAt returns a DateTime and the the value of PublishedAt should not be
        *   accepted from the user but should be always initialized with the system date.
        * 5.the field CreatedBy returns a string data type
        * 6.the field Url returns a string data type
        * 7.the field UrlToImage returns a string data type
        */
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public UserProfile User { get; set; }
        public List<Reminder> Reminders { get; set; }
    }
}
