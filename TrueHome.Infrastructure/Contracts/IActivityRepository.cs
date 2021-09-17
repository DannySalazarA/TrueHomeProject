using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrueHome.Entities.DBModel;

namespace TrueHome.Infrastructure.Contracts
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> GetActivitiesAsync();

        Task<IEnumerable<Activity>> GetActivitiesByPropertyIdAsync(int propertyId);

        Task<long> CreateActivityAsync(Activity activityParam);
        Task UpdateScheduleActivityAsync(Activity activityParam);
        Task UpdateStatusActivityAsync(Activity activityParam);
    }
}
