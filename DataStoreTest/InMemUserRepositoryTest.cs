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
    public class InMemUserRepositoryTest
    {
        public void TestCreation()
        {
            //arrange
            //act
            InMemUserRepository repo = new InMemUserRepository();
            //assert
            Assert.IsInstanceOfType(repo, typeof(InMemBlabRepository));
        }

        [TestMethod]
        public void TestAddition()
        {
            //arrange
            InMemUserRepository repo = new InMemUserRepository();
            //act
            repo.Add(new User("johndoe@gmail.com", "jdoe", "John", "Doe"));
            IEnumerable<IEntity> list = repo.GetAll();
            //assert
            Assert.AreEqual(System.Linq.Enumerable.Count(list), 1);
        }
        [ExpectedException(typeof(Exception), "Invalid User")]
        [TestMethod]
        public void TestAdditionInvalidUser()
        {
            //arrange
            InMemUserRepository repo = new InMemUserRepository();
            //act
            repo.Add(new User("johndoe@gmail.com", "", "John", "Doe"));
            IEnumerable<IEntity> list = repo.GetAll();
            //assert
            Assert.AreEqual(System.Linq.Enumerable.Count(list), 1);
        }

        [TestMethod]
        public void TestGetByEntityId()
        {
            InMemUserRepository repo = new InMemUserRepository();
            var u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
            repo.Add(u);
            var u2 = repo.GetById(u.GetId());
            Assert.AreEqual(u, u2);
        }

        [TestMethod]
        public void TestRemove()
        {
            InMemUserRepository repo = new InMemUserRepository();
            var u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
            repo.Add(u);
            repo.Remove(u);

            IEnumerable<IEntity> list = repo.GetAll();
            //ensure that there are 0 values in the repo, which means we deleted successfully
            Assert.AreEqual(System.Linq.Enumerable.Count(list), 0);
        }
        
        [TestMethod]
        [ExpectedException(typeof(Exception), "could not remove entity.  Did it actually exist?")]
        public void TestRemoveNonExistent()
        {
            InMemUserRepository repo = new InMemUserRepository();
            var u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
            repo.Remove(u);

            IEnumerable<IEntity> list = repo.GetAll();
            //ensure that there are 0 values in the repo, which means we deleted successfully
            Assert.AreEqual(System.Linq.Enumerable.Count(list), 0);
        }

        [TestMethod]
        public void TestUpdate()
        {
            InMemUserRepository repo = new InMemUserRepository();
            var u = new User("johndoe@gmail.com", "jdoe", null, null);
            repo.Add(u);
            u.FirstName = "John";
            u.LastName = "Doe";
            var u2 = (User)repo.GetById(u.GetId());
            //Assert.AreNotEqual(u2.FirstName, "John");
            repo.Update(u);
            var u3 = (User)repo.GetById(u.GetId());
            Assert.AreEqual(u3.FirstName, "John");

        }
    }
}

