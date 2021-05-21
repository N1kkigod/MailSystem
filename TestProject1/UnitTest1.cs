using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailSystemWebApi.Controllers;
using MailSystemWebApi.Repositories;
using MailSystemWebApi.Models;
using MailSystemWebApi;
using Microsoft.AspNetCore.Mvc;
using MailSystemWebApi.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Net.Http;
using System.Linq;
using Moq;
using Appium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        string dbUrl = "Server=tcp:mailsystem.database.windows.net,1433;Initial Catalog=mailSystem;Persist Security Info=False;User ID=N1kki;Password=Pass1337;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        [TestMethod]
        public void MailControllerMoq_getAllMailByUserId()
        {
            var mock = new Mock<IMailRepository<Mail>>();
            mock.Setup(a => a.getAllMailByUserId(1)).Returns(new List<Mail>());
            var testMails = new MailController(mock.Object);
            Assert.IsNotNull(testMails.Get(1));
        }
        [TestMethod]
        public void MailControllerMoq_createMailByUser()
        {
            var mock = new Mock<IMailRepository<Mail>>();
            mock.Setup(a => a.createMailByUser(new Mail())).Returns(new Mail());
            var test = new MailController(mock.Object);
            Assert.IsNotNull(test.Post(new Mail()));
        }
        [TestMethod]
        public void MailControllerMoq_deleteMailByUser()
        {
            var mock = new Mock<IMailRepository<Mail>>();
            mock.Setup(a => a.deleteMailByUser(1)).Returns(new Mail());
            var test = new MailController(mock.Object);
            Assert.IsNotNull(test.Delete(1));
        }
        [TestMethod]
        public void MailControllerMoq_get()
        {
            var mock = new Mock<IMailRepository<Mail>>();
            mock.Setup(a => a.getAllMail()).Returns(new List<Mail>());
            var test = new MailController(mock.Object);
            Assert.IsNotNull(test.Get());
        }
        [TestMethod]
        public void UserControllerMoq_getAllUserNames()
        {
            var mock = new Mock<IUserRepository<User>>();
            mock.Setup(a => a.getAllUserNames()).Returns(new List<string>());
            var test = new UserController(mock.Object);
            Assert.IsNotNull(test.GetAllUserNames());
        }
        [TestMethod]
        public void UserControllerMoq_getUserIdByUserName()
        {
            var mock = new Mock<IUserRepository<User>>();
            mock.Setup(a => a.getUserIdByUserName("admin")).Returns(new int());
            var test = new UserController(mock.Object);
            Assert.IsNotNull(test.GetUserIdByUserName("admin"));
        }
        [TestMethod]
        public void UserControllerMoq_get()
        {
            var mock = new Mock<IUserRepository<User>>();
            mock.Setup(a => a.checkLogin("admin","admin")).Returns(new User());
            var test = new UserController(mock.Object);
            Assert.IsNotNull(test.Get());   
        }
        [TestMethod]
        public void UserControllerMoq_getUserByUserId()
        {
            var mock = new Mock<IUserRepository<User>>();
            mock.Setup(a => a.getUserByUserId(1)).Returns(new User());
            var test = new UserController(mock.Object);
            Assert.IsNotNull(test.GetUserByUserId(1));
        }
        [TestMethod]
        public void MailController_getAllMailByUserId()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var testMails = new MailController(new MailRepository<Mail>(context)).Get(1);
            var listMails = new MailRepository<Mail>(context).getAllMailByUserId(1);
            Assert.AreEqual(testMails[0], listMails[0]);
        }
        [TestMethod]
        public void MailController_createMailByUser()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var test = new MailController(new MailRepository<Mail>(context)).Post(new Mail());
            Assert.IsNotNull(test);
        }
        [TestMethod]
        public void MailController_deleteMailByUser()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var test = new MailController(new MailRepository<Mail>(context)).Delete(21);
            Assert.IsNotNull(test);
        }
        [TestMethod]
        public void UserController_getAllUserNames()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var test = new UserController(new UserRepository<User>(context)).GetAllUserNames();
            Assert.IsNotNull(test);
        }
        [TestMethod]
        public void UserController_getUserIdByUserName()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var test = new UserController(new UserRepository<User>(context)).GetUserIdByUserName("admin");
            Assert.IsNotNull(test);
        }
        [TestMethod]
        public void UserController_get()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            Assert.IsNotNull(new UserController(new UserRepository<User>(context)).Get());
        }
        [TestMethod]
        public void UserController_getUserByUserId()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var test = new UserController(new UserRepository<User>(context)).GetUserByUserId(1);
            Assert.IsNotNull(test);
        }
        [TestMethod]
        public void UserController_getUserByUserId_2()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var test = new UserController(new UserRepository<User>(context)).GetUserByUserId(-1);
            Assert.IsNull(test);
        }
        [TestMethod]
        public void MailRepository_getMailById()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            Assert.IsNull(new MailRepository<Mail>(context).getMailById());
        }
        [TestMethod]
        public void MailRepository_getAllMailByUserId()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            Assert.IsNotNull(new MailRepository<Mail>(context).getAllMailByUserId(1));
        }
        [TestMethod]
        public void MailRepository_createMailByUser()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            Assert.IsNotNull(new MailRepository<Mail>(context).createMailByUser(new Mail()));
        }
        [TestMethod]
        public void MailRepository_deleteMailByUser()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var test = new MailRepository<Mail>(context).deleteMailByUser(context.Set<Mail>().OrderBy(Mail => Mail.MailID).Last().MailID);
            Assert.IsNotNull(test);
        }
        [TestMethod]
        public void MailRepository_getAllMail()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var listMails = new MailRepository<Mail>(context).getAllMail();
            Assert.IsNotNull(listMails);
        }
        [TestMethod]
        public void UserRepository_getAllUserNames()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var testUsers = new List<string>();
            testUsers.Add("admin");
            testUsers.Add("N1kki");
            testUsers.Add("Test");
            var listUsers = new UserRepository<User>(context).getAllUserNames();
            Assert.AreEqual(testUsers[0], listUsers[0]);
        }
        [TestMethod]
        public void UserController_GetUserIdByUserName()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var testUsers = 1;
            var listUsers = new UserController(new UserRepository<User>(context)).GetUserIdByUserName("admin");
            Assert.AreEqual(testUsers, listUsers);
        }
        [TestMethod]
        public void UserRepository_CheckLogin()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            User testValue = new();
            testValue.UserID = 1; testValue.UserName = "admin";
            testValue.Password = "admin"; testValue.Permission = "1";
            var actualValue = new UserRepository<User>(context).checkLogin(testValue.UserName, testValue.Password);
            Assert.AreEqual<User>(testValue, actualValue);
        }
        [TestMethod]
        public void User_ShouldReturnFalse()
        {
            Assert.IsFalse(Equals(new User(), false));
        }
        [TestMethod]
        public void Mail_ShouldReturnFalse()
        {
            Assert.IsFalse(Equals(new Mail(), false));
        }
        [TestMethod]
        public void User_ShouldBeEqual()
        {
            Assert.AreEqual(new User(), new User());
        }
        [TestMethod]
        public void Mail_ShouldBeEqual()
        {
            Assert.AreEqual(new Mail(), new Mail());
        }
        [TestMethod]
        public void UserRepository_getUserByUserId()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var testValue = new User();
            testValue.UserID = 1;
            testValue.UserName = "admin";
            testValue.Password = "admin";
            testValue.Permission = "1";
            var actualValue = new UserRepository<User>(context).getUserByUserId(testValue.UserID);
            Assert.AreEqual(testValue, actualValue);
        }
        [TestMethod]
        public void UserRepository_getUserIdByUserName()
        {
            DbContextOptionsBuilder<ApplicationContext> options = new DbContextOptionsBuilder<ApplicationContext>();
            options.UseSqlServer(dbUrl);
            ApplicationContext context = new ApplicationContext(options.Options);
            var testValue = 1;
            var actualValue = new UserRepository<User>(context).getUserIdByUserName("admin");
            Assert.AreEqual(testValue, actualValue);
        }
        [TestMethod]
        public void User_ShouldBeFalse()
        {
            Assert.AreEqual(false, new User().Equals(null));
        }
        [TestMethod]
        public void Mail_ShouldBeFalse()
        {
            Assert.AreEqual(false, new Mail().Equals(null));
        }
        [TestMethod]
        public void User_UserNameEquals()
        {
            User user = new User();
            user.UserName = "test";
            var actualValue = user.UserName;
            var testValue = "test";
            Assert.AreEqual(testValue, actualValue);
        }
        [TestMethod]
        public void User_IDEquals()
        {
            User user = new User();
            user.UserID = 1;
            var actualValue = user.UserID;
            var testValue = 1;
            Assert.AreEqual(testValue, actualValue);
        }
        [TestMethod]
        public void User_PasswordEquals()
        {
            User user = new User();
            user.Password = "test";
            var actualValue = user.Password;
            var testValue = "test";
            Assert.AreEqual(testValue, actualValue);
        }
        [TestMethod]
        public void User_PermissionEquals()
        {
            User user = new User();
            user.Permission = "1";
            var actualValue = user.Permission;
            var testValue = "1";
            Assert.AreEqual(testValue, actualValue);
        }
        [TestMethod]
        public void Mail_MailIDEquals()
        {
            Mail mail = new Mail();
            mail.MailID = 1;
            var actualValue = mail.MailID;
            var testValue = 1;
            Assert.AreEqual(testValue, actualValue);
        }
        [TestMethod]
        public void Mail_TitleEquals()
        {
            Mail mail = new Mail();
            mail.Title = "test";
            var actualValue = mail.Title;
            var testValue = "test";
            Assert.AreEqual(testValue, actualValue);
        }
        [TestMethod]
        public void Mail_MailContentEquals()
        {
            Mail mail = new Mail();
            mail.MailContent = "test";
            var actualValue = mail.MailContent;
            var testValue = "test";
            Assert.AreEqual(testValue, actualValue);
        }
        [TestMethod]
        public void Mail_DateEquals()
        {
            Mail mail = new Mail();
            mail.Date = DateTime.Today;
            var actualValue = mail.Date;
            var testValue = DateTime.Today;
            Assert.AreEqual(testValue, actualValue);
        }
        [TestMethod]
        public void Mail_AddresserIDEquals()
        {
            Mail mail = new Mail();
            mail.AddresserID = 1;
            var actualValue = mail.AddresserID;
            var testValue = 1;
            Assert.AreEqual(testValue, actualValue);
        }
        [TestMethod]
        public void Mail_AddresseeIDEquals()
        {
            Mail mail = new Mail();
            mail.AddresseeID = 1;
            var actualValue = mail.AddresseeID;
            var testValue = 1;
            Assert.AreEqual(testValue, actualValue);
        }
        [TestMethod]
        public void Desktop_e2e_login_test()
        {
            var appiumOptions = new OpenQA.Selenium.Appium.AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", @"C:\Users\9i9i\source\repos\MailSystemDesktopApp\MailSystemDesktopApp\bin\Debug\net5.0-windows\MailSystemDesktopApp.exe");
            var applicationSession = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
            applicationSession.FindElementByName("loginTextBox").SendKeys("admin");
            applicationSession.FindElementByName("passwordTextBox").SendKeys("admin");
            //applicationSession.FindElementByClassName("Button").Click();
            string current = applicationSession.CurrentWindowHandle;
            PopupWindowFinder finder = new PopupWindowFinder(applicationSession);
            string newHandle = finder.Click(applicationSession.FindElementByClassName("Button"));
            applicationSession.SwitchTo().Window(newHandle);
            applicationSession.FindElementByName("titleTextBox").SendKeys("test");
            var testText = applicationSession.FindElementByName("titleTextBox").Text;
            Assert.IsNotNull(testText);
        }
    }
}
