using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication.Controllers;

namespace UnitTesting
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void HomeTest()
        {
            Assert.AreEqual("HomeController", "HomeController");
        }

        [TestMethod]
        public void ReturnRegistrationView()
        {
            //Arrange
            HomeController homecontroller = new HomeController();
            //Act
            var result = homecontroller.Register() as ViewResult;
            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void ReturnLoginView()
        {
            //Arrange
            HomeController homecontroller = new HomeController();
            //Act
            var result = homecontroller.Login() as ViewResult;
            //Assert
            Assert.AreEqual("", result.ViewName);
        }

        [TestMethod]
        public void ReturnRegistrationData()
        {
            //Arrange
            HomeController homecontroller = new HomeController();
            //Act
            var result = homecontroller.Register() as ViewResult;
            //Assert
            Assert.AreEqual("Register", result.ViewData["Name"]);
        }

        [TestMethod]
        public void ReturnLoginData()
        {
            //Arrange
            HomeController homecontroller = new HomeController();
            //Act
            var result = homecontroller.Login() as ViewResult;
            //Assert
            Assert.AreEqual("Login", result.ViewData["Name"]);
        }


    }
}
