using DataAccessLayer.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessLayer.Test.EFRepositoryTest
{
	[TestClass]
	public class UserRepositoryTest : BaseRepositoryTest.BaseUserRepositoryTest
	{
		[TestInitialize]
		public override void TestInit()
		{
			usersRepository = new UsersRepository(TestConfigs.ConnectionString);
			base.TestInit();
		}

		[TestMethod]
		public override void GetUsers_MustReturnUsersAsExpectedTest()
		{
			base.GetUsers_MustReturnUsersAsExpectedTest();
		}
	}
}
