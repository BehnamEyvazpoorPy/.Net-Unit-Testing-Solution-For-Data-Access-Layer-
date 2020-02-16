using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
	public class UserService : IUserService
	{
		private readonly IUsersRepository _usersRepository;
		public UserService(IUsersRepository usersRepository)
		{
			_usersRepository = usersRepository;
		}

		public IEnumerable<UserViewModel> GetUsers()
		{
			var userDataModels = _usersRepository.GetUsers();
			return userDataModels.Select(ud => new UserViewModel
			{
				Id = ud.Id,
				Username = ud.Username
			});
		}
	}
}
