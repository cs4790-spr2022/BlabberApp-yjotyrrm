using BlabberApp.DataStore.Plugins;
using BlabberApp.Domain.Common.Interfaces;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreTest
{
    [TestClass]
    public class InMemBlabRepositoryTest
    {
        //a sample user to use in initialzing blabs
        private User _user;

        public InMemBlabRepositoryTest()
        {
            _user = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
        }

        public void TestCreation()
        {
            //arrange
            //act
            InMemBlabRepository repo = new InMemBlabRepository();
            //assert
            Assert.IsInstanceOfType(repo, typeof(InMemBlabRepository));
        }

        [TestMethod]
        public void TestAddition()
        {
            //arrange
            InMemBlabRepository repo = new InMemBlabRepository();
            //act
            repo.Add(new Blab (_user, "sample content"));
            IEnumerable<IEntity> list = repo.GetAll();
            //assert
            Assert.AreEqual(System.Linq.Enumerable.Count(list), 1);
        }

        [TestMethod]
        public void TestGetByEntityId()
        {
            InMemBlabRepository repo = new InMemBlabRepository();
            var u = new Blab(_user, "sample content");
            repo.Add(u);
            var u2 = repo.GetById(u.GetId());
            Assert.AreEqual(u, u2);
        }

        [TestMethod]
        public void TestRemove()
        {
            InMemBlabRepository repo = new InMemBlabRepository();
            var u = new Blab(_user, "sample content");
            repo.Add(u);
            repo.Remove(u);

            IEnumerable<IEntity> list = repo.GetAll();
            //ensure that there are 0 values in the repo, which means we deleted successfully
            Assert.AreEqual(System.Linq.Enumerable.Count(list), 0);

        }

        [TestMethod]
        public void TestUpdate()
        {
            InMemBlabRepository repo = new InMemBlabRepository();
            var u = new Blab(_user, "sample content one");
            repo.Add(u);
            u.Content = "sample content two";
            var u2 = (Blab)repo.GetById(u.GetId());
            //Assert.AreNotEqual(u2.FirstName, "John");
            repo.Update(u);
            var u3 = (Blab)repo.GetById(u.GetId());
            Assert.AreEqual(u3.Content, "sample content two");

        }
    }
}
