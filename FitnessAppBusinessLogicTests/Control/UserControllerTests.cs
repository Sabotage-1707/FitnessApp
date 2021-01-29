using Microsoft.VisualStudio.TestTools.UnitTesting;
using FintnessAppBusinessLogic.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessAppBusinessLogic.Model;

namespace FintnessAppBusinessLogic.Control.Tests
{
    [TestClass()]
    public class UserControllerTests
    {

        [TestMethod()]
        public void SaveTest()
        {
            //Arange 
            var userName = Guid.NewGuid().ToString();

            //Act
            var controller = new UserController(userName);

            //Assert
            Assert.AreEqual(userName, controller.CurrentUser.Name);
        }

        [TestMethod()]
        public void SetNewUserDataTest()
        {
            var rnd = new Random();
            //Arange 
            var userName = Guid.NewGuid().ToString();
            var gender = Guid.NewGuid().ToString();
            var date = DateTime.Now.AddYears(-18);
            var weight = rnd.Next(10, 200);
            var height = rnd.Next(50, 200);
            var controller = new UserController(userName);
            //Act
            controller.SetNewUserData(gender, date, weight, height);
            var controller2 = new UserController(userName);
            
            //Assert
            Assert.AreEqual(userName, controller2.CurrentUser.Name);
            Assert.AreEqual(gender, controller2.CurrentUser.Gender.Name);
            Assert.AreEqual(date, controller2.CurrentUser.Birthday);
            Assert.AreEqual(weight, controller2.CurrentUser.Weight);
            Assert.AreEqual(height, controller2.CurrentUser.Height);

        }

    }
}