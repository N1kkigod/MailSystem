using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailSystemWebApi
{
    public class Mail
    {
        [Key]
        public int MailID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int AddresseeID { get; set; }
        public int AddresserID { get; set; }
        public string MailContent { get; set; } 
    }
}
