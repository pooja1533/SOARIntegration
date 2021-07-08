using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Stages.Repository
{
    public interface IRepository<T> where T : BaseEntity {
		T Get (string entityId);
		void Insert (T entity);
		void Update (T entity);
	}
}