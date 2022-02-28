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


		//validate is not yet implemented
		//	[TestMethod]
		//	public void TestValidate()
		//	{

		//		bool flag = false;
		//		//deliberate invalid username
		//		User u = new User("johndoe@gmail.com", "", "John", "Doe");
		//		try
		//		{
		//			u.Validate();
		//		}
		//		catch (InvalidUsernameException e)
		//		{
		//			flag = true;
		//		}

		//		//the expected exception was caught
		//		Assert.IsTrue(flag);
		//	}
	}
}
