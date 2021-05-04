using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailSystemWebApi.Repositories;
using MailSystemWebApi.Models;
using Microsoft.AspNetCore.Mvc.Filters;
//using System.Web.Http;

namespace MailSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly ActionExecutedContext context;
        public UserController(IUserRepository<User> user)
        {
            //this.context = context;
            UsersTable = user;
        }
        private IUserRepository<User> UsersTable { get; set; }
        //public UserController(IUserRepository<User> user)
        //{
        //    
        //}
        [HttpGet]
        public ActionResult Get()
        {

            string header = Request.Headers["auth"];
            User answer = UsersTable.checkLogin(header.Split(" ")[0], header.Split(" ")[1]);
            if (answer == null)
            {
                return NotFound();
            }
            else
                return new OkObjectResult(answer);
        }
        [Route("/getAllUserNames")]
        [HttpGet]
        public List<String> GetAllUserNames()
        {
            return UsersTable.getAllUserNames();
        }
        [Route("/getUserId")]
        [HttpGet]
        public int GetUserIdByUserName(string userName)
        {
            return UsersTable.getUserIdByUserName(userName);
        }
        [Route("/getUserByUserId")]
        [HttpGet]
        public User GetUserByUserId(int userId)
        {
            return UsersTable.getUserByUserId(userId);
        }
    }
}
