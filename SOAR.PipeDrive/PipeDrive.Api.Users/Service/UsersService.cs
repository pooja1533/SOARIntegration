using System;
using System.Collections.Generic;
using SOARIntegration.PipeDrive.Api.Users.Repository;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Users.Service
{
    public class UsersService : IUsersService
	{
		private IRepository<User> _userRepository;

		public UsersService(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		public void InsertUsers(List<User> users)
		{
			for (var count = 0; count < users.Count; count++)
			{
				try
				{
					var user = _userRepository.Get(users[count].EntityId);
					if (user == null)
					{
						_userRepository.Insert(users[count]);
					}
					else
					{
						users[count].Id = user.Id;
                        users[count].Audit_Created = user.Audit_Created;
                        users[count].Audit_Modified = user.Audit_Modified;
                        users[count].Audit_Deleted = user.Audit_Deleted;
                        _userRepository.Update(users[count]);
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		}
	}
}
