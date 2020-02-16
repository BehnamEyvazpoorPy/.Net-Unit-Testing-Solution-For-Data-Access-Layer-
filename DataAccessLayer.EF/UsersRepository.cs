using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccessLayer.EF
{
	public class UsersRepository : IUsersRepository
	{
		private readonly Database _database;

		public UsersRepository(string connectionStrig)
		{
			var options = new DbContextOptionsBuilder<Database>()
			.UseSqlServer(connectionStrig)
			.Options;

			_database = new Database(options);
		}

		public IEnumerable<User> GetUsers()
		{
			return _database.Users;
		}
	}
}
