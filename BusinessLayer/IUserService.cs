using System.Collections.Generic;

namespace BusinessLayer
{
	public interface IUserService
	{
		IEnumerable<UserViewModel> GetUsers();
	}
}
