using BlabberApp.DataStore.Plugins;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Dto;

namespace ApiTest
{
    [TestClass]
    public class UsersControllerTest
    {
        private InMemUserRepository repo;
        private UsersController controller;

        [TestInitialize]
        public void Initialize()
        {
            repo = new InMemUserRepository();
            controller = new(new Mock<ILogger<UsersController>>().Object, repo);
        }

        [TestMethod]
        public void TestGetAllNonEmpty()
        {
            repo.Add(new User("unittester@gmail.com", "unittester", "unit", "tester"));
            var output = controller.GetAll().ToList();
            Assert.AreEqual(1, output.Count);
        }

        [TestMethod]
        public void TestGetById()
        {
            User u = new User("unittester@gmail.com", "unittester", "unit", "tester");
            repo.Add(u);
            var output = controller.GetById(u.Id);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.OkObjectResult", output.ToString());
        }
        [TestMethod]
        public void TestGetByIdBadId()
        {
            Guid fake = new Guid();
            var output = controller.GetById(fake);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.NotFoundResult", output.ToString());
        }

        [TestMethod]
        public void TestGetByIdNonexistent()
        {
            var output = controller.GetById(new Guid());
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.NotFoundResult", output.ToString());
        }

        [TestMethod]
        public void TestGetByEmail()
        {
            User u = new User("unittester@gmail.com", "unittester", "unit", "tester");
            repo.Add(u);
            var output = controller.GetByEmail(u.Email.ToString());
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.OkObjectResult", output.ToString());
        }

        [TestMethod]
        public void TestGetByEmailNonexistent()
        {
            var output = controller.GetByEmail("fake@notreal.com");
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.NotFoundResult", output.ToString());
        }

        [TestMethod]
        public void TestGetByUsername()
        {
            User u = new User("unittester@gmail.com", "unittester", "unit", "tester");
            repo.Add(u);
            var output = controller.GetByUsername(u.Username);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.OkObjectResult", output.ToString());
        }

        [TestMethod]
        public void TestGetByUsernameNonexistent()
        {
            var output = controller.GetByUsername("fiction");
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.NotFoundResult", output.ToString());
        }

        [TestMethod]
        public void TestPost()
        {
            UserDto dto = new("unittester@gmail.com", "unittester", "unit", "tester");
            var output = controller.Post(dto);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.CreatedAtRouteResult", output.ToString());
            Assert.AreEqual("unittester", repo.GetByEmail("unittester@gmail.com").Username);
        }

        [TestMethod]
        public void TestPostNull()
        {
            var output = controller.Post(null);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.BadRequestObjectResult", output.ToString());
        }

        [TestMethod]
        public void TestUpdate()
        {
            User u = new User("unittester@gmail.com", "unittester", "unit", "tester");
            UserDto userDto = new("unittester@gmail.com", "unittester", "changedfirstname", "tester");
            repo.Add(u);
            var output = controller.Update(userDto);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult", output.ToString());
            Assert.AreEqual("changedfirstname", repo.GetByEmail("unittester@gmail.com").FirstName);
        }
        [TestMethod]
        public void TestUpdateNull()
        {
            var output = controller.Update(null);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.BadRequestObjectResult", output.ToString());
        }

        [TestMethod]
        public void TestRemove()
        {
            User u = new User("unittester@gmail.com", "unittester", "unit", "tester");
            UserDto userDto = new("unittester@gmail.com", "unittester", "changedfirstname", "tester");
            repo.Add(u);
            var output = controller.Remove(userDto);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.AcceptedAtRouteResult", output.ToString());
            Assert.AreEqual(0, repo.GetAll().ToList().Count);
        }
    }
}
