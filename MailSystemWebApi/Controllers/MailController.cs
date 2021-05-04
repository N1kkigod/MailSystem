using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailSystemWebApi.Repositories;

namespace MailSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private IMailRepository<Mail> Mails { get; set; }
        public MailController(IMailRepository<Mail> mail)
        {
            Mails = mail;
        }
        [Route("/mails")]
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(Mails.getAllMail());
        }
        [Route("/mails/{userId}")]
        [HttpGet]
        public IList<Mail> Get(int userId)
        {
            return Mails.getAllMailByUserId(userId);
        }
        [HttpPost]
        public JsonResult Post(Mail mail)
        {
            return new JsonResult(Mails.createMailByUser(mail));
        }
        [HttpDelete]
        public JsonResult Delete(int mailID)
        {
            JsonResult jsonResult = new JsonResult(Mails.deleteMailByUser(mailID));
            jsonResult.StatusCode = 200;
            return jsonResult;
        }
    }
}
