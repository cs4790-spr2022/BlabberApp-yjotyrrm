//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;
//using DataStore;
//using Domain;


//namespace DataStoreTest
//{
//    [TestClass]
//    public class InMemRepositoryTest
//    {
//        public void TestCreation()
//        {
//            //arrange
//            //act

//            InMemRepository repo = new InMemRepository();
//            //assert
//            Assert.IsInstanceOfType(repo, typeof(InMemRepository));
//        }

//        [TestMethod]
//        public void TestAddition()
//        {
//            //arrange
//            InMemRepository repo = new InMemRepository();
//            //act
//            repo.Add(new User("johndoe@gmail.com", "jdoe", "John", "Doe"));
//            IEnumerable<IEntity> list = repo.List();
//            //assert
//            Assert.AreEqual(System.Linq.Enumerable.Count(list), 1);
//        }

//        [TestMethod]
//        public void TestGet()
//        {
//            //arrange
//            InMemRepository repo = new InMemRepository();
//            var u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
//            repo.Add(u);
//            var u2 = repo.Get(u);
//            Assert.AreEqual(u, u2);
//        }

//        [TestMethod]
//        public void TestGetByEntityId()
//        {
//            InMemRepository repo = new InMemRepository();
//            var u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
//            repo.Add(u);
//            var u2 = repo.GetByEntityId(u.GetId());
//            System.Diagnostics.Debug.WriteLine(u2.GetId());
//            Assert.AreEqual(u, u2);
//        }

//        [TestMethod]
//        public void TestDelete()
//        {
//            InMemRepository repo = new InMemRepository();
//            var u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
//            repo.Add(u);
//            repo.Delete(u);

//            IEnumerable<IEntity> list = repo.List();
//            //ensure that there are 0 values in the repo, which means we deleted successfully
//            Assert.AreEqual(System.Linq.Enumerable.Count(list), 0);

//        }

//        [TestMethod]
//        public void TestEdit()
//        {
//            InMemRepository repo = new InMemRepository();
//            var u = new User("johndoe@gmail.com", "jdoe", null, null);
//            repo.Add(u);
//            u.FirstName = "John";
//            u.LastName = "Doe";
//            var u2 = (User)repo.GetByEntityId(u.GetId());
//            //Assert.AreNotEqual(u2.FirstName, "John");
//            repo.Edit(u);
//            var u3 = (User)repo.GetByEntityId(u.GetId());
//            Assert.AreEqual(u3.FirstName, "John");

//        }

//    }
//}

