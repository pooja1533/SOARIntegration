using System;
using System.Collections.Generic;
using SOARIntegration.PipeDrive.Api.Users.Service;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Users.WebJob
{
    public class UserModel
	{
		#region Private Fields
		private JSONToObject _user;
		#endregion

		#region CTOR
		public UserModel(JSONToObject user)
		{
			_user = user;
		}
		#endregion

		#region ProcessData
		public void ProcessData(IUsersService userService)
		{
			try
			{
				List<User> userList = new List<User>();
				foreach (var user in _user.Data)
				{
					var objUser = MapFields(user);
					userList.Add(objUser);
				}

				userService.InsertUsers(userList);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		#endregion

		#region Map Fields
		private User MapFields(UserData user)
		{
			User objUser = new User();
			objUser.EntityId = user.Id;
			objUser.Name = user.Name;
			objUser.Default_Currency = user.DefaultCurrency;
			objUser.Locale = user.Locale;
			objUser.Lang = user.Lang;
			objUser.Email = user.Email;
			objUser.Phone = user.Phone;
			objUser.Activated = user.Activated;
			objUser.Last_Login = IsValidDate(user.LastLogin) ? DateTime.Parse(user.LastLogin) : (DateTime?)null;
			objUser.Created = user.Created;
			objUser.Modified = user.Modified;
			objUser.SignUp_Flow_Variation = user.SignupFlowVariation;
			objUser.Has_Created_Company = user.HasCreatedCompany;
			objUser.Is_Admin = user.IsAdmin;
			objUser.TimeZone_Name = user.TimezoneName;
			objUser.TimeZone_Offset = user.TimezoneOffset;
			objUser.Active_Flag = user.ActiveFlag;
			objUser.Role_Id = user.RoleId;
			objUser.Icon_URL = user.IconUrl;
			objUser.Is_You = user.IsYou;

			return objUser;
		}

		#endregion

		protected bool IsValidDate(string date)
		{
			try
			{
				DateTime dt = DateTime.Parse(date);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
