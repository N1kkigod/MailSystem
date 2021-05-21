using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MailSystemWebApi.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Permission { get; set; }
        public override bool Equals(object other)
        {
            var toCompareWith = other as User;
            if (toCompareWith == null)
                return false;
            return this.UserID == toCompareWith.UserID && this.UserName == toCompareWith.UserName && this.Password == toCompareWith.Password && this.Permission == toCompareWith.Permission;
        }
    }
}
