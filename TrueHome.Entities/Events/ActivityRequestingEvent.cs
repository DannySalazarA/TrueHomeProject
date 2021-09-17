using Framework.DomainKernel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueHome.Entities.Events
{
    public class ActivityRequestingEvent : IDomainEvent
    {
        public ActivityRequestingEvent()
        {
            DateTimeEventOccurred = DateTime.Now;
        }
        public DateTime DateTimeEventOccurred { get; private set; }
    }

    public class ActivityRequestingByPropertyIdEvent : IDomainEvent
    {
        public ActivityRequestingByPropertyIdEvent(int propertyId)
        {
            DateTimeEventOccurred = DateTime.Now;
            PropertyId = propertyId;

        }

        public DateTime DateTimeEventOccurred { get; private set; }
        public int PropertyId { get; private set; }
    }

    public class ActivityRequestingDefaultEvent : IDomainEvent
    {
        public ActivityRequestingDefaultEvent()
        {
            DateTimeEventOccurred = DateTime.Now;
        }

        public DateTime DateTimeEventOccurred { get; private set; }

    }
}
