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

            var diff = DateTime.UtcNow - b.DttmCreated;

            //ensure that our datetime is correct; it should have been created within 1 minute of the current time.
            Assert.IsTrue(diff < new TimeSpan(0, 1, 0));

        }

        [TestMethod]
        public void BlabsShouldHaveCorrectValues()
        {
            var u1 = new User("test@test.com", "testusername", "first", "user");
            var e = new Blab(u1, "this is the content of both blabs");
            var a = new Blab(u1, "this is the content of both blabs");

            Assert.AreNotEqual(a.Id, e.Id);
            Assert.AreEqual(e.User.Id, a.User.Id);
            Assert.AreEqual(e.Content, a.Content);
        }

        //we can't pass in a user through data rows, so instead we'll initialize both users we may want to use and pass in a boolean indicating which one to use.
        [DataTestMethod]
        [DataRow(false, "sample content")]
        public void TestAreEqual(bool whichuser, string content)
        {
            User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
            User u2 = new User("sample@gmail.com", "test", "test", "test");
            Blab b = new Blab(u, "sample content");

            //we have to manually set the IDs to be equal as they are generated on creation
            Blab b2 = new Blab(whichuser ? u2 : u, content);
            { b2.Id = b.Id; }

            Assert.IsTrue(b.AreEqual(b2));
        }

        //we can't pass in a user through data rows, so instead we'll initialize both users we may want to use and pass in a boolean indicating which one to use.
        [DataTestMethod]
        [DataRow(false, "different sample content")]
        [DataRow(true, "different sample content")]
        [DataRow(true, "sample content")]
        public void TestAreNotEqual(bool whichuser, string content)
        {
            User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
            User u2 = new User("sample@gmail.com", "test", "test", "test");
            Blab b = new Blab(u, "sample content");

            //we have to manually set the IDs to be equal as they are generated on creation
            Blab b2 = new Blab(whichuser ? u2 : u, content);
            { b2.Id = b.Id; }

            Assert.IsFalse(b.AreEqual(b2));
        }


        [TestMethod]
        public void TestValidateInvalidUser()
        {
            User u = new User("johndoe@gmail.com", "", "John", "Doe");
            var b = new Blab(u, null);
            Assert.IsFalse(b.Validate());
        }

        [TestMethod]
        public void TestValidateInvalidContent()
        {
            User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
            var b = new Blab(u, "");
            Assert.IsFalse(b.Validate());
        }

        [TestMethod]
        public void TestValidateTooLongContent()
        {
            User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
            var b = new Blab(u, "this string is way over 128 characters: somebody once told me the world was gonna roll me I ain't the sharpest tool in the shed she was looking kinda dumb with her finger and her thumb in the shape of an L on her forehead well the years start coming and they don't stop coming fed to the rules and I hit the ground running");
            Assert.IsFalse(b.Validate());
        }

        [TestMethod]
        public void TestValidate()
        {
            User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
            var b = new Blab(u, "this string should be valid");
            Assert.IsTrue(b.Validate());
        }

    }
}

