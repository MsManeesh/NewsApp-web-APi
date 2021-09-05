using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Exceptions;
using System;
using System.Threading.Tasks;
namespace NewsAPI.Controllers
{
    /*
   * As in this assignment, we are working with creating RESTful web service, hence annotate
   * the class with [ApiController] annotation and define the controller level route as per 
   * REST Api standard.
   */
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /*
        * UserService should  be injected through constructor injection. 
        * Please note that we should not create service object using the new keyword
        */
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /*
        * Example: //GET: api/user
        * Define a handler method which will get the user details by a userId.
        * This handler method should return any one of the status messages basis on
        * different situations: 
        * 1. 200(OK) - If the user details found successfully.
        * 2. 404(NOT FOUND) - If the userprofile with specified userid doesn't exist. 
        * This handler method should map to the URL "/api/user/{userId}" using HTTP GET method
        * 3. 500 (Internal Server Error),means that server cannot process the request 
          for an unknown reason.
        */
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                UserProfile User=await _userService.GetUser(userId);
                return Ok(User);
            }catch(UserNotFoundException e)
            {
                return NotFound(e.Message);
            }catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
        }
        /*
         * Define a handler method which will create a specific user profile by reading the
         * Serialized object from request body and save the user details in a userprofile table
         * in the database.
         * 
         * This handler method should
         * return any one of the status messages basis on different situations: 
         * 1. 201(CREATED) - If the user profile details created successfully. 
         * 2. 409(CONFLICT) - If the userId conflicts
         * This handler method should map to the URL "/api/user" using HTTP POST method
         * 3. 500 (Internal Server Error),means that server cannot process the request 
         *    for an unknown reason.
         */
        [HttpPost]
        public async Task<IActionResult> Post(UserProfile user)
        {
            try
            {
                bool flag = await _userService.AddUser(user);
                return Created("",flag);
            }
            catch (UserAlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        /*
         * Define a handler method which will update a specific user by reading the
         * Serialized object from request body and save the updated user details in
         * a userprofile table in database.
         * This handler method should return any one of the status
         * messages basis on different situations: 
         * 1. 200(OK) - If the reminder updated
         * successfully. 
         * 2. 404(NOT FOUND) - If the userprofile with specified userid doesn't exist. 
         * 
         * This handler method should map to the URL "/api/reminder/{id}" using HTTP PUT
         * method.
         * 3. 500(Internal Server Error),means that server cannot process the request 
         *    for an unknown reason.
         */
        [HttpPut]
        [Route("{userId}")]
        public async Task<IActionResult> Put(string userId,UserProfile user)
        {
            try
            {
                bool flag=await _userService.UpdateUser(userId,user);
                return Ok(flag);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        /*
        * Define a handler method which will delete a user from a database.
        * 
        * This handler method should return any one of the status messages basis on
        * different situations: 
        * 1. 200(OK) - If the user deleted successfully from database. 
        * 2. 404(NOT FOUND)-If the user with specified userrId is not found. 
        * 
        * This handler method should map to the URL "/api/user/{id}" using HTTP Delete
        * method" where "id" should be replaced by a valid userId without {}
        * 3. 500(Internal Server Error),means that server cannot process the request 
        *    for an unknown reason.
        */
        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            try
            {
                bool flag = await _userService.DeleteUser(userId);
                return Ok(flag);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
