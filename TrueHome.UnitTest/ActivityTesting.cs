using Framework.DomainKernel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueHome.Entities.DBModel;
using TrueHome.Entities.Events;
using TrueHome.Entities.Exceptions;
using Xunit;

namespace TrueHome.UnitTest
{
    [TestClass]
    public class ActivityTesting
    {
        [Fact]
        public void RaiseErrorIfScheduleExistForCreation()
        {

            DomainEvents.ClearCallbacks();


            Func<ActivityRequestingByPropertyIdEvent, Task<List<Activity>>> activityRequest = (args) => ActivityDataAsync();
            DomainEvents.SubscribeManually(activityRequest);

            Activity activity = ResetActivityData();
            Action validateData = async () => { await activity.ValidateSchedule(activity.property_id, activity.schedule); };

            var ex = Xunit.Assert.Throws<ExistingScheduleException>(validateData);
            Xunit.Assert.IsType<ExistingScheduleException>(ex);

        }

        private Activity ResetActivityData()
        {
            return new Activity(
            property_id: 2,
            schedule: DateTime.Parse("2021-09-19 13:00:00"),
            title: "Unit Test");
        }

        private async Task<List<Activity>> ActivityDataAsync()
        {
            Func<Activity> returnActivity = () => new Activity(
                                                            property_id: 2,
                                                            schedule: DateTime.Parse("2021-09-19 13:00:00"),
                                                            title: "Unit Test");
            List<Activity> activityList = new List<Activity>();
            var activity = await Task.Factory.StartNew(returnActivity);
            activityList.Add(activity);
            return activityList;
        }
    }
}
