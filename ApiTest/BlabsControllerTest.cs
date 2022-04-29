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
    public class BlabsControllerTest
    {
        BlabsController controller;
        InMemBlabRepository repo;
        InMemUserRepository userRepository;
        [TestInitialize]
        public void Initialize()
        {
            repo = new InMemBlabRepository();
            userRepository = new InMemUserRepository();
            controller = new(
            new Mock<ILogger<BlabsController>>().Object,
            repo,
            userRepository);
        }
        [TestMethod]
        public void TestGetAllEmpty()
        {
            var output = controller.GetAll().ToList();
            Assert.IsNotNull(output);
            Assert.AreEqual(0, output.Count);
        }
        [TestMethod]
        public void TestGetAllNonEmpty()
        {
            User u = new User("unittest@gmail.com", "unittester", "unit", "tester");
            repo.Add(new Blab(u, "test 1"));
            repo.Add(new Blab(u, "test 2"));
            repo.Add(new Blab(u, "test 3"));
            var output = controller.GetAll().ToList();
            Assert.IsNotNull(output);
            Assert.AreEqual(3, output.Count);
        }
        [TestMethod]
        public void TestGetById()
        {
            User u = new User("unittest@gmail.com", "unittester", "unit", "tester");
            Blab b = new Blab(u, "test 1");
            repo.Add(b);
            repo.Add(new Blab(u, "test 2"));
            var output = controller.GetById(b.Id);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.OkObjectResult", output.ToString());
        }
        [TestMethod]
        public void TestGetByIdNonexistent()
        {
            var output = controller.GetById(new Guid());
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.NotFoundResult", output.ToString());
        }
        [TestMethod]
        public void TestPost()
        {
            User u = new("unittest@gmail.com", "unittester", "unit", "tester");
            BlabDto b = new("unittest@gmail.com", "sample content");
            //adding a blab will fail if there's no user associated with it, so we need to add a user
            userRepository.Add(u);
            var output = controller.Post(b);
            Assert.AreEqual("Microsoft.AspNetCore.Mvc.CreatedAtRouteResult", output.ToString());
            //make sure it was actually added to the repo
            Assert.AreEqual(1, repo.GetAll().ToList().Count);
        }

    }
}
