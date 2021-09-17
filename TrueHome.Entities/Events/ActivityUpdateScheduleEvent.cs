using Framework.DomainKernel.Contracts;
using System;
using TrueHome.Entities.DBModel;

namespace TrueHome.Entities.Events
{
    public  class ActivityUpdateScheduleEvent : IDomainEvent
    {

        public ActivityUpdateScheduleEvent(Activity activityParam)
        {
            DateTimeEventOccurred = DateTime.Now;
            this.ActivityParam = activityParam;
        }

        public DateTime DateTimeEventOccurred { get; private set; }
        public Activity ActivityParam { get; private set; }
    }
}