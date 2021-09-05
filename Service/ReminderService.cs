using DAL;
using Entities;
using Service.Exceptions;
using System.Threading.Tasks;

namespace Service
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e ReminderService by inheriting IReminderService

    // ReminderService class is used to implement all input validation operations for Reminder CRUD operations

    public class ReminderService : IReminderService
    {
        /*
         * this service depends on ReminderRepository instance for the crud operations
         */
        IReminderRepository _reminderRepo;
        public ReminderService(IReminderRepository reminderRepo)
        {
            _reminderRepo = reminderRepo;
        }


        /*
         * Implement AddReminder() method which should be used to 
         * save a new reminder, provided the reminder does not exist for the specific news id, 
         * else should throw ReminderAlreadyExistsException
         */
        public async Task<Reminder> AddReminder(Reminder reminder)
        {
            Reminder exist = await _reminderRepo.GetReminderByNewsId(reminder.NewsId);
            if (exist == null)
            {
                return await _reminderRepo.AddReminder(reminder);
            }
            else
                throw new ReminderAlreadyExistsException($"This news: {reminder.NewsId} already have a reminder");
        }
        /*
         * Implement GetReminderByNewsId() method which should be used to 
         * get complete reminder details for the provided newsId,
         * however, should throw ReminderNotFoundException if no reminder exist for the provided newsId
         */

        public async Task<Reminder> GetReminderByNewsId(int newsId)
        {
            Reminder reminder = await _reminderRepo.GetReminderByNewsId(newsId);
            if (reminder != null)
            {
                return reminder;
            }
            else
                throw new ReminderNotFoundException($"No reminder found for news: {newsId}");
        }
        /*
         * Implement RemoveReminder() method which should be used to 
         * delete an existing reminder
         * however, should throw ReminderNotFoundException if reminder with provided reminderId does not exist         * 
         */
        public async Task<bool> RemoveReminder(int reminderId)
        {
            Reminder reminder = await _reminderRepo.GetReminder(reminderId);
            if (reminder != null)
            {
                return await _reminderRepo.RemoveReminder(reminder);

            }
            else
                throw new ReminderNotFoundException($"No reminder found with id: {reminderId}");
        }
    }
}
