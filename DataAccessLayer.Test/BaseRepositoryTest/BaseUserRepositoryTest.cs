using DataAccessLayer.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DataAccessLayer.Test.BaseRepositoryTest
{
	public class BaseUserRepositoryTest
	{
		protected IUsersRepository usersRepository;

		public virtual void TestInit()
		{
			DatabaseInitializer.InitDatabase();
		}

		public virtual void GetUsers_MustReturnUsersAsExpectedTest()
		{
			var expectedUsers = new List<User>
			{
				new User { Id = 1, Username = "TestUser1", Password = "Password1" },
				new User { Id = 2, Username = "TestUser2", Password = "Password2" }
			};

			var actualUsers = usersRepository.GetUsers();

			Assert.AreEqual(JsonConvert.SerializeObject(expectedUsers), JsonConvert.SerializeObject(actualUsers));
		}
	}
}
