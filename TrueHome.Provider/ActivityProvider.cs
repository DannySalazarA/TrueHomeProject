using Framework.DomainKernel.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueHome.Entities.DBModel;
using TrueHome.Entities.Events;
using TrueHome.Infrastructure.Contracts;
using TrueHome.Provider.Contracts;

namespace TrueHome.Provider
{
    public class ActivityProvider : IActivityProvider, IHandle<ActivityRequestingEvent, List<Activity>>, IHandle<ActivityRequestingByPropertyIdEvent, List<Activity>>,
        IHandle<ActivityCreatingEvent, long>, IHandle<ActivityUpdateScheduleEvent>, IHandle<ActivityUpdateStatusEvent>
    {
        public ActivityProvider(IActivityRepository activityRepository)
        {
            this.activityRepository = activityRepository;
        }

        public async Task<List<Activity>> GetAsync()
        {
            return await Activity.GetAsync();
        }

        public async Task<List<Activity>> GetAddressByPropertyIdAsync(int propertyId)
        {
            return await Activity.GetAsyncByPropertyId(propertyId);
        }

        public async Task<long> CreateActivityAsync(Activity activityParam)
        {
            return await activityParam.CreateAsync();
        }

        public async Task UpdateScheduleActivityAsync(Activity activityParam)
        {
            await activityParam.UpdateScheduleActivity();
        }

        public async Task UpdateStatusActivityAsync(Activity activityParam)
        {
            await activityParam.UpdateStatusActivity();
        }


        #region Handlers

        public async Task<List<Activity>> HandleAsync(ActivityRequestingEvent args)
        {
            return await activityRepository.GetActivitiesAsync() as List<Activity>;
        }

        public async Task<List<Activity>> HandleAsync(ActivityRequestingByPropertyIdEvent args)
        {
            return await activityRepository.GetActivitiesByPropertyIdAsync(args.PropertyId) as List<Activity>;
        }

        public async Task<long> HandleAsync(ActivityCreatingEvent args)
        {
            return await activityRepository.CreateActivityAsync(args.ActivityParam);
        }

        public async Task HandleAsync(ActivityUpdateScheduleEvent args)
        {
            await activityRepository.UpdateScheduleActivityAsync(args.ActivityParam);
        }

        public async Task HandleAsync(ActivityUpdateStatusEvent args)
        {
            await activityRepository.UpdateStatusActivityAsync(args.ActivityParam);
        }



        #endregion Handlers




        IActivityRepository activityRepository;
    }
}
