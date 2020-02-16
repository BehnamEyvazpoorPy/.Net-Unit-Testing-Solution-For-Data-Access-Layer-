using DataAccessLayer.Entity;
using System.Collections.Generic;

namespace DataAccessLayer
{
	public interface IUsersRepository
	{
		IEnumerable<User> GetUsers();
	}
}
