using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DomainKernel.Contracts
{
    public interface IDomainEvent
    {
        DateTime DateTimeEventOccurred { get; }
    }
}
