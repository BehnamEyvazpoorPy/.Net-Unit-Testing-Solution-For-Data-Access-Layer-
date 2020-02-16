using DataAccessLayer.Entity;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer.Ado
{
	public class UsersRepository : IUsersRepository
	{
		private readonly string _connectionString;
		public UsersRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public IEnumerable<User> GetUsers()
		{
			var users = new List<User>();
			// Connects to the database and queries the user records
			using var connection = new SqlConnection(_connectionString);
			using var command = new SqlCommand("SELECT Id,Username,Password FROM Users", connection);
			connection.Open();
			var reader = command.ExecuteReader();
			while (reader.Read())
			{
				var user = new User
				{
					Id = int.Parse(reader[0].ToString()),
					Username = reader[1].ToString(),
					Password = reader[2].ToString()
				};
				users.Add(user);
			}

			return users;
		}
	}
}
