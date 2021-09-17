using Framework.DomainKernel.Contracts;
using System;

namespace TrueHome.Entities.DBModel
{
    public class ActivityUpdateStatusEvent : IDomainEvent
    {
        public ActivityUpdateStatusEvent(Activity activityParam)
        {
            DateTimeEventOccurred = DateTime.Now;
            this.ActivityParam = activityParam;
        }
        public DateTime DateTimeEventOccurred { get; private set; }
        public Activity ActivityParam { get; private set; }
    }
}