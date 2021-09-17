using Framework.DomainKernel.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueHome.Entities.Events
{
    public class PropertyRequestingEvent :IDomainEvent
    {
        public PropertyRequestingEvent()
        {
            DateTimeEventOccurred = DateTime.Now;
        }
        public DateTime DateTimeEventOccurred { get; private set; }


    }

}
