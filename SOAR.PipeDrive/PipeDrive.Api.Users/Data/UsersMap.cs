using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Users.Data
{
	public class UsersMap
	{
		public UsersMap(EntityTypeBuilder<User> entityBuilder)
		{
			entityBuilder.HasKey(t => t.Id);
			entityBuilder.Property(t => t.Name).IsRequired();
		}
	}
}
