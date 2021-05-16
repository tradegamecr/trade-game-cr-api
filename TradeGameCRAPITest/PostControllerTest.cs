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
    public class PostControllerTest
    {
        [TestMethod]
        public void Get_All()
        {
            var entities = new List<Post>()
            {
                new Post()
            };
            var pagination = new PaginationDTO();
            var mockRepository = new Mock<IRepository<Post>>();

            mockRepository.Setup(x => x.GetByPagination(pagination, false)).ReturnsAsync(entities);

            var controllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var controller = new PostController(mockRepository.Object);

            controller.ControllerContext = controllerContext;

            var expected = new List<PostDTO>();
            var result = controller.Get(pagination).Result.Value;

            Assert.IsInstanceOfType(result, expected.GetType());
        }

        [TestMethod]
        public void Get_ById_Exist()
        {
            var id = 1;
            var entity = new Post()
            {
                Id = id
            };
            var mockRepository = new Mock<IRepository<Post>>();

            mockRepository.Setup(x => x.Get(id)).ReturnsAsync(entity);

            var controller = new PostController(mockRepository.Object);
            var expected = new PostDTO()
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
            var mock = new Mock<IRepository<Post>>();

            mock.Setup(x => x.Get(id)).ReturnsAsync(default(Post));

            var controller = new PostController(mock.Object);
            var result = controller.Get(id);

            Assert.IsInstanceOfType(result.Result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Post_Create()
        {
            var entityCreateDto = new PostCreateDTO();
            var entity = new Post();
            var mock = new Mock<IRepository<Post>>();

            mock.Setup(x => x.Add(entity)).ReturnsAsync(entity);

            var controller = new PostController(mock.Object);
            var result = controller.Post(entityCreateDto);

            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtRouteResult));
        }

        [TestMethod]
        public void Put_Update()
        {
            var entityUpdateDto = new PostUpdateDTO();
            var entity = new Post();
            var mock = new Mock<IRepository<Post>>();

            mock.Setup(x => x.Update(entity)).ReturnsAsync(entity);

            var controller = new PostController(mock.Object);
            var result = controller.Put(entityUpdateDto);

            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void Patch_PatchDocument_NotExist()
        {
            var id = 1;
            var patchDocument = default(JsonPatchDocument<PostUpdateDTO>);
            var mock = new Mock<IRepository<Post>>();
            var controller = new PostController(mock.Object);
            var result = controller.Patch(id, patchDocument);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Patch_Entity_NotExist()
        {
            var id = 1;
            var patchDocument = new JsonPatchDocument<PostUpdateDTO>();
            var mock = new Mock<IRepository<Post>>();

            mock.Setup(x => x.Get(id)).ReturnsAsync(default(Post));

            var controller = new PostController(mock.Object);
            var result = controller.Patch(id, patchDocument);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Patch_Valid()
        {
            var id = 1;
            var entity = new Post()
            {
                Id = id,
                Title = "Darth Vader"
            };
            var patchDocument = new JsonPatchDocument<PostUpdateDTO>();
            var operation = new Operation<PostUpdateDTO>("replace", "/title", "value");
            var mock = new Mock<IRepository<Post>>();
            var objectValidatorMock = new Mock<IObjectModelValidator>();

            patchDocument.Operations.Add(operation);
            mock.Setup(x => x.Get(id)).ReturnsAsync(entity);
            mock.Setup(x => x.SaveChangesAsync()).Verifiable();
            objectValidatorMock.Setup(x => x.Validate(It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(),
                It.IsAny<string>(),
                It.IsAny<object>()));

            var controller = new PostController(mock.Object);

            controller.ObjectValidator = objectValidatorMock.Object;

            var result = controller.Patch(id, patchDocument);

            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public void Delete()
        {
            var id = 1;
            var mock = new Mock<IRepository<Post>>();

            mock.Setup(x => x.Delete(id));

            var controller = new PostController(mock.Object);
            var result = controller.Delete(id);

            Assert.IsInstanceOfType(result.Result.Result, typeof(NoContentResult));
        }
    }
}
