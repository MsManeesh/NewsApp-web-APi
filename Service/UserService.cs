using DAL;
using Entities;
using Service.Exceptions;
using System.Threading.Tasks;

namespace Service
{
    //Inherit the respective interface and implement the methods in 
    // this class i.e UserService by inheriting IUserService

    // UserService class is used to implement all input validation operations for User CRUD operations

    public class UserService : IUserService
    {
        /*
         * this service depends on UserRepository instance for the crud operations
         */
        IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }


        /*
         * Implement AddUser() method which should be used to 
         * save a new user, provided the UserId does not exist, 
         * else should throw UserAlreadyExistsException
         */
        public async Task<bool> AddUser(UserProfile user)
        {
            bool flag = await _userRepo.AddUser(user);
            if (flag)
                return true;
            else
                throw new UserAlreadyExistsException($"{user.UserId} already exists");
        }


        /*
         * Implement DeleteUser() method which should be used to 
         * delete an existing user,
         * however, should throw UserNotFoundException if User with provided userId does not exist         * 
         */

        public async Task<bool> DeleteUser(string userId)
        {
            UserProfile user =await _userRepo.GetUser(userId);
            if (user != null)
            {
                bool flag = await _userRepo.DeleteUser(user);
                return flag;
            }
            else
                throw new UserNotFoundException($"{userId} doesn't exist");
        }


        /*
         * Implement GetUser() method which should be used to 
         * get complete userprofile details by userId,
         * however, should throw UserNotFoundException if User with provided userId does not exist
         */
        public async Task<UserProfile> GetUser(string userId)
        {
            UserProfile user = await _userRepo.GetUser(userId);
            if (user != null)
            {
                return user;
            }
            else
                throw new UserNotFoundException($"{userId} doesn't exist");
        }

        /*
         * Implement UpdateUser() method which should be used to 
         * update email and contact details for existing user,
         * however, should throw UserNotFoundException if User with provided userId does not exist
         */
        public async Task<bool> UpdateUser(string userId, UserProfile user)
        {
            UserProfile exist = await _userRepo.GetUser(userId);
            if (exist != null)
            {
                bool flag = await _userRepo.UpdateUser(user);
                return flag;
            }
            else
                throw new UserNotFoundException($"{userId} doesn't exist");
        }
    }
}
