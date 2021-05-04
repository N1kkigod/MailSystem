using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MailSystemWebApi.Models;

namespace MailSystemWebApi.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> UsersTable { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
