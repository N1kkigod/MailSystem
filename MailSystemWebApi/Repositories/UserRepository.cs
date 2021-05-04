using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailSystemWebApi.DataBase;
using MailSystemWebApi.Models;
using MailSystemWebApi.Repositories;

namespace MailSystemWebApi.Repositories
{
    public class UserRepository<TDbModel> : IUserRepository<TDbModel> where TDbModel : User
    {
        private ApplicationContext Context { get; set; }
        public UserRepository(ApplicationContext context)
        {
            Context = context;
        }
        public TDbModel checkLogin(string login, string password)
        {
            try 
            {
                IQueryable<TDbModel> searchModel = Context.Set<TDbModel>().Where(user => user.UserName == login);
                bool searchUser = searchModel.Any<TDbModel>();
                if(!searchUser)
                {
                    return null;
                }

                //var selectedUser = Context.Set<TDbModel>().Select(
                //    (user, username) => new { User = user, UserName = username }).Where(user => user.UserName == login)
                //    .Select(user => user.UserID);

                //int id = Context.Set<TDbModel>().Find
                int userID = searchModel.First().UserID;
                if (Context.Set<TDbModel>().Find(userID).Password == password)
                    return Context.Set<TDbModel>().Find(userID);
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }
        public List<String> getAllUserNames()
        {
            //TDbModel user;
            List<String> userNames = new List<String>();
            foreach(TDbModel user in Context.Set<TDbModel>().ToList<TDbModel>())
            {
                userNames.Add(user.UserName);
            }
            return userNames;
        }
        public int getUserIdByUserName(string userName)
        {
            try
            {
                return Context.Set<TDbModel>().Where(user => user.UserName == userName).First().UserID;
            }
            catch
            {
                return -1;
            }
        }
        public TDbModel getUserByUserId(int userId)
        {
            try
            {
                return Context.Set<TDbModel>().Find(userId);
            }
            catch
            {
                return null;
            }
        }
    }
}
