using Microsoft.VisualStudio.TestTools.UnitTesting;
using FintnessAppBusinessLogic.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FintnessAppBusinessLogic.Model;

namespace FintnessAppBusinessLogic.Control.Tests
{
    [TestClass()]
    public class EatingControllerTests
    {
       

        [TestMethod()]
        public void AddTest()
        {
            var rnd = new Random();
            //Arange 
            var userName = Guid.NewGuid().ToString();
            var foodName = Guid.NewGuid().ToString();
            var manager = new SerializableSaver();
            var userController = new UserController(userName, manager);
            var eatingController = new EatingController(userController.CurrentUser,manager);
            Food food = new Food(foodName, rnd.Next(50, 500), rnd.Next(50, 500), rnd.Next(50, 500), rnd.Next(50, 500));
            //Act
            eatingController.Add(food, 200);
            
            //Assert
            Assert.AreEqual(food.Name, eatingController.Eating.Foods.First().Key.Name);
            
        }

        
    }
}