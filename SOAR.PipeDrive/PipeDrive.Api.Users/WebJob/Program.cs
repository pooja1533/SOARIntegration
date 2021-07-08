#region NameSpace
using System;
using System.IO;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOARIntegration.PipeDrive.Api.Users.Repository;
using SOARIntegration.PipeDrive.Api.Users.Service;
#endregion

namespace SOARIntegration.PipeDrive.Api.Users.WebJob
{
    public class Program
	{
		static void Main(string[] args)
		{
			var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
			var serviceProvider = new ServiceCollection()
				.AddLogging()
				.AddDbContext<Context>(options => options.UseSqlServer(connectionString))
				.AddScoped(typeof(IRepository<>), typeof(Repository<>))
				.AddSingleton<IUsersService, UsersService>()
				.BuildServiceProvider();
			
			var usersService = serviceProvider.GetService<IUsersService>();

			UsersData objUserData = new UsersData(usersService);
			objUserData.ProcessData();
		}
	}

    public partial class UsersData
    {
        #region Private Fields
        private readonly IUsersService _usersService;
        #endregion

        #region CTOR
        public UsersData(IUsersService usersService)
        {
            this._usersService = usersService;
        }
        #endregion

        #region ProcessData
        public void ProcessData()
        {
            try
            {
				var pipeDriveToken = Environment.GetEnvironmentVariable("PipeDriveToken");
				string apiUrl = "https://a1l.pipedrive.com/v1/users?api_token=" + pipeDriveToken;

				Uri address = new Uri(apiUrl);

                // Create the web request 
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

                request.Method = "GET";
                request.ContentType = "text/xml"; 

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream 
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string json = reader.ReadToEnd();

                    // Convert json to c# object
                    var userObject = JSONToObject.FromJson(json);

                    UserModel objUserModel = new UserModel(userObject);
                    objUserModel.ProcessData(this._usersService);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\\n" + ex.StackTrace);
            }
        }
        #endregion
    }
}