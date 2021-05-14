using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TradeGameCRAPI.Controllers;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPITest
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void Get_All()
        {
            var entities = new List<User>()
            {
                new User()
            };
            var mockRepository = new Mock<IRepository<User>>();

            mockRepository.Setup(r => r.GetAll(false)).ReturnsAsync(entities);

            var controller = new UserController(mockRepository.Object);
            var expected = new List<UserDTO>();
            var result = controller.Get().Result.Value;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, expected.GetType());
        }

        [TestMethod]
        public void Get_ById_Exist()
        {
            var id = 1;
            var entity = new User()
            {
                Id = id
            };
            var mockRepository = new Mock<IRepository<User>>();

            mockRepository.Setup(r => r.Get(id)).ReturnsAsync(entity);

            var controller = new UserController(mockRepository.Object);
            var expected = new UserDTO()
            { 
                Id = id
            };
            var result = controller.Get(id).Result.Value;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, expected.GetType());
            Assert.AreEqual(expected.Id, result.Id);
        }

        [TestMethod]
        public void Get_ById_NotExist()
        {
            var id = 1;
            var mock = new Mock<IRepository<User>>();

            mock.Setup(x => x.Get(id)).ReturnsAsync(default(User));

            var controller = new UserController(mock.Object);
            var result = controller.Get(id);

            Assert.IsInstanceOfType(result.Result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Post_Create()
        {
            var entityCreateDto = new UserCreateDTO();
            var entity = new User();
            var mock = new Mock<IRepository<User>>();

            mock.Setup(x => x.Add(entity)).ReturnsAsync(entity);

            var controller = new UserController(mock.Object);
            var result = controller.Post(entityCreateDto);

            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult));
        }

        [TestMethod]
        public void Put_Update()
        {
            var entityUpdateDto = new UserUpdateDTO();
            var entity = new User();
            var mock = new Mock<IRepository<User>>();

            mock.Setup(x => x.Update(entity)).ReturnsAsync(entity);

            var controller = new UserController(mock.Object);
            var result = controller.Put(entityUpdateDto);

            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }
    }
}
