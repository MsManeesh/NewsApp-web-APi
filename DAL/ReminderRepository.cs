using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace DAL
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e ReminderRepository by inheriting IReminderRepository
    //ReminderRepository class is used to implement all Data access operations
    public class ReminderRepository:IReminderRepository
    {
        NewsDbContext _dbContext;
        public ReminderRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Reminder> AddReminder(Reminder reminder)
        {
            _dbContext.Reminders.Add(reminder);
            await _dbContext.SaveChangesAsync();
            return reminder;
        }

        public async Task<Reminder> GetReminder(int reminderId)
        {
            return await _dbContext.Reminders.FirstOrDefaultAsync(x => x.ReminderId == reminderId);
        }

        public async Task<Reminder> GetReminderByNewsId(int newsId)
        {
            return await _dbContext.Reminders.FirstOrDefaultAsync(x => x.NewsId == newsId);
        }

        public async Task<bool> RemoveReminder(Reminder reminder)
        {
            _dbContext.Reminders.Remove(reminder);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        // Implement CreateReminder method which should be used to save a new reminder.    
        // Implement DeletReminder method which method should be used to delete an existing reminder.
        // Implement GetAllRemindersByUserId method which should be used to get all reminder by userId.
        // Implement GetReminderById method which should be used to get a reminder by reminderId
        // Implement UpdateReminder method which should be used to update an existing reminder

    }
}
