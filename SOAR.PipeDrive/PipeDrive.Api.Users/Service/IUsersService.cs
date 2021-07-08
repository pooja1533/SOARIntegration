using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;
using System.Collections.Generic;

namespace SOARIntegration.PipeDrive.Api.Users.Service
{
	public interface IUsersService
	{
		void InsertUsers(List<User> users);
	}
}
