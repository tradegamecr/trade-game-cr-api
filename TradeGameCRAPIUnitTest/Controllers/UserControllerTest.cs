using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TradeGameCRAPI.Controllers;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPIUnitTest
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public async void Get()
        {
            var users = new List<User>()
            {
                new User(),
                new User()
            };
            var expected = new List<UserDTO>()
            {
                new UserDTO(),
                new UserDTO()
            };
            var mock = new Mock<IRepository<User>>();

            mock.Setup(x => x.GetAll(false)).ReturnsAsync(users);

            var userController = new UserController(mock.Object);
            var result = await userController.Get();

            Assert.AreEqual(1, 2);
        }
    }
}
