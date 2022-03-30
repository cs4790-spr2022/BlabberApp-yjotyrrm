using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System;
using Domain.Entities;

namespace DomainTest
{

	[TestClass]
	public class UserTest
	{

		[TestMethod]
		public void TestCreation()
		{
			//arrange
			var e = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
			//act
			var a = new User("bobsmith@gmail.com", "bsmith", "Bob", "Smith");
			//assert
			Assert.IsInstanceOfType(a, e.GetType());
		}
		[TestMethod]
		public void TestCreationNoName()
		{
			var e = new User("johndoe@gmail.com", "jdoe", null, null);
			Assert.IsInstanceOfType(e, typeof(User));
		}
		[TestMethod]
		public void TestCreationBadEmail()
		{
			bool flag = false;
			try
			{
				var e = new User("bademail", "jdoe", "John", "Doe");
			}
			catch (Exception e)
			{
				flag = true;
			}

			Assert.IsTrue(flag);


		}

		[TestMethod]
		public void TestFullNameFirstLast()
		{
			string e = "John Doe";

			User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");


			string a = u.GetFullNameFirstLast();

			Assert.AreEqual(e, a);

		}

		[TestMethod]
		public void TestFullNameLastFirst()
		{
			string e = "Doe, John";

			User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");


			string a = u.GetFullNameLastFirst();

			Assert.AreEqual(e, a);

		}

		[TestMethod]
		public void TestUUID()
		{
			//arrange
			User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
			//act
			System.Guid id = u.GetId();
			//assert
			Assert.IsNotNull(id);
		}

		[TestMethod]
		public void TestEqualityDifferentUUIDs()
        {
			//these should have different ids, despite having the same data, because each unit generates its own id
			User u1 = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
			User u2 = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
			Assert.IsFalse(u1.AreEqual(u2));
		}

		[DataTestMethod]
		[DataRow("johndoe@gmail.com", "jdoe", "John", "Doe")]
		public void TestIsEqual(string email, string username, string firstName, string lastName)
		{
			//we will manually set the Ids to be the same to test the rest of the isEqual functionality
			User u1 = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
			User u2 = new User(email, username, firstName, lastName);
			{ u2.Id = u1.Id; }
			Assert.IsTrue(u1.AreEqual(u2));
		}
		//data rows to check each individual item that could be different.
		[DataTestMethod]
		[DataRow("notjohndoe@gmail.com", "jdoe", "John", "Doe")]
		[DataRow("johndoe@gmail.com", "notjdoe", "John", "Doe")]
		[DataRow("johndoe@gmail.com", "jdoe", "notJohn", "Doe")]
		[DataRow("johndoe@gmail.com", "jdoe", "John", "notDoe")]
		public void TestAreNotEqual(string email, string username, string firstName, string lastName)
        {
			//we will manually set the Ids to be the same to test the rest of the isEqual functionality
			User u1 = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
			User u2 = new User(email, username, firstName, lastName);
			{ u2.Id = u1.Id; }
			Assert.IsFalse(u1.AreEqual(u2));
		}



        [TestMethod]
        public void TestValidateInvalidUsername()
        {


            //deliberate invalid username
            User u = new User("johndoe@gmail.com", "", "John", "Doe");
            Assert.IsFalse(u.Validate());

        }
		[TestMethod]
		public void TestValidate()
		{


			//valid user
			User u = new User("johndoe@gmail.com", "jdoe", "John", "Doe");
			Assert.IsTrue(u.Validate());

		}
	}
}
