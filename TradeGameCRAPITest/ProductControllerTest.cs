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
    public class ProductControllerTest
    {
        [TestMethod]
        public void Get_All()
        {
            var entities = new List<Product>()
            {
                new Product()
            };
            var pagination = new PaginationDTO();
            var mockRepository = new Mock<IRepository<Product>>();

            mockRepository.Setup(x => x.GetByPagination(pagination, false)).ReturnsAsync(entities);

            var controllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var controller = new ProductController(mockRepository.Object);

            controller.ControllerContext = controllerContext;

            var expected = new List<ProductDTO>();
            var result = controller.Get(pagination).Result.Value;

            Assert.IsInstanceOfType(result, expected.GetType());
        }

        [TestMethod]
        public void Get_ById_Exist()
        {
            var id = 1;
            var entity = new Product()
            {
                Id = id
            };
            var mockRepository = new Mock<IRepository<Product>>();

            mockRepository.Setup(x => x.Get(id)).ReturnsAsync(entity);

            var controller = new ProductController(mockRepository.Object);
            var expected = new ProductDTO()
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
            var mock = new Mock<IRepository<Product>>();

            mock.Setup(x => x.Get(id)).ReturnsAsync(default(Product));

            var controller = new ProductController(mock.Object);
            var result = controller.Get(id);

            Assert.IsInstanceOfType(result.Result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Post_Create()
        {
            var entityCreateDto = new ProductCreateDTO();
            var entity = new Product();
            var mock = new Mock<IRepository<Product>>();

            mock.Setup(x => x.Add(entity)).ReturnsAsync(entity);

            var controller = new ProductController(mock.Object);
            var result = controller.Post(entityCreateDto);

            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult));
        }

        [TestMethod]
        public void Put_Update()
        {
            var entityUpdateDto = new ProductUpdateDTO();
            var entity = new Product();
            var mock = new Mock<IRepository<Product>>();

            mock.Setup(x => x.Update(entity)).ReturnsAsync(entity);

            var controller = new ProductController(mock.Object);
            var result = controller.Put(entityUpdateDto);

            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void Patch_PatchDocument_NotExist()
        {
            var id = 1;
            var patchDocument = default(JsonPatchDocument<ProductUpdateDTO>);
            var mock = new Mock<IRepository<Product>>();
            var controller = new ProductController(mock.Object);
            var result = controller.Patch(id, patchDocument);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Patch_Entity_NotExist()
        {
            var id = 1;
            var patchDocument = new JsonPatchDocument<ProductUpdateDTO>();
            var mock = new Mock<IRepository<Product>>();

            mock.Setup(x => x.Get(id)).ReturnsAsync(default(Product));

            var controller = new ProductController(mock.Object);
            var result = controller.Patch(id, patchDocument);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Patch_Valid()
        {
            var id = 1;
            var entity = new Product()
            {
                Id = id,
                Name = "Darth Vader"
            };
            var patchDocument = new JsonPatchDocument<ProductUpdateDTO>();
            var operation = new Operation<ProductUpdateDTO>("replace", "/name", "value");
            var mock = new Mock<IRepository<Product>>();
            var objectValidatorMock = new Mock<IObjectModelValidator>();

            patchDocument.Operations.Add(operation);
            mock.Setup(x => x.Get(id)).ReturnsAsync(entity);
            mock.Setup(x => x.SaveChangesAsync()).Verifiable();
            objectValidatorMock.Setup(x => x.Validate(It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(),
                It.IsAny<string>(),
                It.IsAny<object>()));

            var controller = new ProductController(mock.Object);

            controller.ObjectValidator = objectValidatorMock.Object;

            var result = controller.Patch(id, patchDocument);

            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void Delete()
        {
            var id = 1;
            var mock = new Mock<IRepository<Product>>();

            mock.Setup(x => x.Delete(id));

            var controller = new ProductController(mock.Object);
            var result = controller.Delete(id);

            Assert.IsInstanceOfType(result.Result.Result, typeof(NoContentResult));
        }
    }
}
