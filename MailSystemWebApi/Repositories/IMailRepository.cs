using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailSystemWebApi.Repositories
{
    public interface IMailRepository<TDbModel> where TDbModel : Mail
    {
        public IEnumerable<TDbModel> getAllMail();
        public TDbModel getMailById();
        public IList<TDbModel> getAllMailByUserId(int UserId);
        public TDbModel createMailByUser(TDbModel mail);
        public TDbModel deleteMailByUser(int mailID);
    }
}
