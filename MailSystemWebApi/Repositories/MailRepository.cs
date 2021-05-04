using MailSystemWebApi.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailSystemWebApi.Repositories
{

    public class MailRepository<TDbModel> : IMailRepository<TDbModel> where TDbModel : Mail
    {
        private ApplicationContext Context { get; set; }
        public MailRepository(ApplicationContext context)
        {
            Context = context;
        }

        //getAllMail() - возвращает Context всех писем
        public IEnumerable<TDbModel> getAllMail()
        {
            return Context.Set<TDbModel>().ToList();
        }
        //getMailById() - возвращает Context письма по его ID
        public TDbModel getMailById()
        {
            return null;
        }
        //getAllMailByUserId() - возвращает Context всех писем, в которых содержится ID пользователя
        public IList<TDbModel> getAllMailByUserId(int UserId)
        {
            List<TDbModel> mailsByUserId = new List<TDbModel>();
            foreach(TDbModel mailByUserId in Context.Set<TDbModel>().ToList())
            {
                if(mailByUserId.AddresseeID == UserId || mailByUserId.AddresserID == UserId)
                    mailsByUserId.Add(mailByUserId);
            }
            return mailsByUserId;
        }
        public TDbModel createMailByUser(TDbModel mail)
        {
            try
            {
                //TDbModel nMail = (TDbModel)new Mail();
                mail.MailID = Context.Set<TDbModel>().OrderBy(TDbModel => TDbModel.MailID).Last().MailID + 1;
                //nMail.Title = title;
                //nMail.Date = date;
                //nMail.AddresseeID = addresseeId;
                //nMail.AddresserID = addresserId;
                //nMail.MailContent = content;
                TDbModel newMail = Context.Set<TDbModel>().Add(mail).Entity;
                Context.SaveChanges();
                return newMail;
            }
            catch
            {
                throw;
            }
        }
        public TDbModel deleteMailByUser(int mailID)
        {
            try
            {
                TDbModel deletedMail = Context.Set<TDbModel>().Remove(Context.Set<TDbModel>().Find(mailID)).Entity;
                Context.SaveChanges();
                return deletedMail;
            }
            catch
            {
                throw;
            }
        }

    }
}
