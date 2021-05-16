using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    public class DealControllerTest
    {
        [TestMethod]
        public void Get_All()
        {
            var entities = new List<Deal>()
            {
                new Deal()
            };
            var pagination = new PaginationDTO();
            var mockRepository = new Mock<IRepository<Deal>>();

            mockRepository.Setup(x => x.GetByPagination(pagination, false)).ReturnsAsync(entities);

            var controllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var controller = new DealController(mockRepository.Object);

            controller.ControllerContext = controllerContext;

            var expected = new List<DealDTO>();
            var result = controller.Get(pagination).Result.Value;

            Assert.IsInstanceOfType(result, expected.GetType());
        }

        [TestMethod]
        public void Get_ById_Exist()
        {
            var id = 1;
            var entity = new Deal()
            {
                Id = id
            };
            var mockRepository = new Mock<IRepository<Deal>>();

            mockRepository.Setup(x => x.Get(id)).ReturnsAsync(entity);

            var controller = new DealController(mockRepository.Object);
            var expected = new DealDTO()
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
            var mock = new Mock<IRepository<Deal>>();

            mock.Setup(x => x.Get(id)).ReturnsAsync(default(Deal));

            var controller = new DealController(mock.Object);
            var result = controller.Get(id);

            Assert.IsInstanceOfType(result.Result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Post_Create()
        {
            var entityCreateDto = new DealCreateDTO();
            var entity = new Deal();
            var mock = new Mock<IRepository<Deal>>();

            mock.Setup(x => x.Add(entity)).ReturnsAsync(entity);

            var controller = new DealController(mock.Object);
            var result = controller.Post(entityCreateDto);

            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult));
        }

        [TestMethod]
        public void Put_Update()
        {
            var entityUpdateDto = new DealUpdateDTO();
            var entity = new Deal();
            var mock = new Mock<IRepository<Deal>>();

            mock.Setup(x => x.Update(entity)).ReturnsAsync(entity);

            var controller = new DealController(mock.Object);
            var result = controller.Put(entityUpdateDto);

            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void Patch_PatchDocument_NotExist()
        {
            var id = 1;
            var patchDocument = default(JsonPatchDocument<DealUpdateDTO>);
            var mock = new Mock<IRepository<Deal>>();
            var controller = new DealController(mock.Object);
            var result = controller.Patch(id, patchDocument);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Patch_Entity_NotExist()
        {
            var id = 1;
            var patchDocument = new JsonPatchDocument<DealUpdateDTO>();
            var mock = new Mock<IRepository<Deal>>();

            mock.Setup(x => x.Get(id)).ReturnsAsync(default(Deal));

            var controller = new DealController(mock.Object);
            var result = controller.Patch(id, patchDocument);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Patch_Valid()
        {
            var id = 1;
            var entity = new Deal()
            {
                Id = id
            };
            var patchDocument = new JsonPatchDocument<DealUpdateDTO>();
            var operation = new Operation<DealUpdateDTO>("replace", "/message", "value");
            var mock = new Mock<IRepository<Deal>>();
            var objectValidatorMock = new Mock<IObjectModelValidator>();

            patchDocument.Operations.Add(operation);
            mock.Setup(x => x.Get(id)).ReturnsAsync(entity);
            mock.Setup(x => x.SaveChangesAsync()).Verifiable();
            objectValidatorMock.Setup(x => x.Validate(It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(),
                It.IsAny<string>(),
                It.IsAny<object>()));

            var controller = new DealController(mock.Object);

            controller.ObjectValidator = objectValidatorMock.Object;

            var result = controller.Patch(id, patchDocument);

            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void Delete()
        {
            var id = 1;
            var mock = new Mock<IRepository<Deal>>();

            mock.Setup(x => x.Delete(id));

            var controller = new DealController(mock.Object);
            var result = controller.Delete(id);

            Assert.IsInstanceOfType(result.Result.Result, typeof(NoContentResult));
        }
    }
}
