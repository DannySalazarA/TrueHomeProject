using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrueHome.Entities.DBModel;

namespace TrueHome.Provider.Contracts
{
    public interface IActivityProvider
    {
        Task<List<Activity>> GetAsync();
        Task<List<Activity>> GetAddressByPropertyIdAsync(int propertyId);
        Task<long> CreateActivityAsync(Activity activityParam);
        Task UpdateScheduleActivityAsync(Activity activityParam);
        Task UpdateStatusActivityAsync(Activity activityParam);
        Task<Activity> GetActivityByIdAsync(int id);
    }
}
