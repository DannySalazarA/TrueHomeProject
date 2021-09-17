using Framework.DomainKernel.Contracts;
using System;
using TrueHome.Entities.DBModel;

namespace TrueHome.Entities.Events
{
    public class ActivityCreatingEvent : IDomainEvent
    {
        public ActivityCreatingEvent(Activity activityParam)
        {
            DateTimeEventOccurred = DateTime.Now;
            this.ActivityParam = activityParam;
        }
        public DateTime DateTimeEventOccurred { get; private set; }
        public Activity ActivityParam { get; private set; }
    }
}