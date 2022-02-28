using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System;
using Domain.Entities;

namespace DomainTest
{
    [TestClass]
    public class BlabTest
    {
        [TestMethod]
        public void TestCreation()
        {
            User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
            var b = new Blab(u, "sample content");

            Assert.AreEqual(u, b.User);
            Assert.AreEqual("sample content", b.Content);
        }

        [TestMethod]
        public void TestGetId()
        {
            User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
            var b = new Blab(u, "sample content");

            var id = b.GetId();

            Assert.IsInstanceOfType(id, typeof(System.Guid));
            Assert.IsNotNull(id);
        }

        [TestMethod]
        public void TestDttm()
        {
            //this test can fail it takes over 1 minute to run.  If the system is that slow, that's beyond the scope of this test to deal with.

            User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
            var b = new Blab(u, "sample content");

            var diff = DateTime.UtcNow - b.CreatedDttm;

            //ensure that our datetime is correct; it should have been created within 1 minute of the current time.
            Assert.IsTrue(diff < new TimeSpan(0, 1, 0));

        }

            //validate is not yet implemented
        //[TestMethod]
        //[ExpectedException(typeof(InvalidContentException))]
        //public void TestValidate()
        //{
        //    User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
        //    var b = new Blab(u, null);
        //    var b2 = new Blab(u, "");
        //    b.Validate();
        //    b2.Validate();
        //}


    }
}

