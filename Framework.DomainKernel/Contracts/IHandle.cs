using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DomainKernel.Contracts
{
    public interface IHandle<T> where T : IDomainEvent
    {
        //void Handle(T args);

        Task HandleAsync(T args);
    }

    public interface IHandle<T, TResult> where T : IDomainEvent
    {
        //TResult Handle(T args);

        Task<TResult> HandleAsync(T args);
    }
}
