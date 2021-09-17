using Framework.DomainKernel.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;

namespace Framework.DomainKernel
{
    public static class DomainEvents
    {
        //[ThreadStatic]
        private static List<Delegate> actions;

        //TODO: look for alternative solution for this method, maybe review if there is a static acces to the the Net Core IOC container
        public static void Configure(IServiceProvider container)
        {
            if (Container != null)
            {
                throw new Exception("DomainEvents already initialized");
            }
            Container = container;
        }

        private static IServiceProvider Container { get; set; }
        public static void SubscribeManually<T>(Action<T> callback) where T : IDomainEvent
        {
            SubscribeDelegateManually(callback);
        }

        public static void SubscribeManually<T, TResult>(Func<T, TResult> callback) where T : IDomainEvent
        {
            SubscribeDelegateManually(callback);
        }

        private static void SubscribeDelegateManually(Delegate delegateToAdd)
        {
            if (actions == null)
            {
                actions = new List<Delegate>();
            }
            actions.Add(delegateToAdd);
        }

        public static void ClearCallbacks()
        {
            actions = null;
        }

        private static void ValidateCurrentContainer()
        {
            if (Container == null)
            {
                throw new ApplicationException("DomainEvents is not initialized");
            }
        }



        #region Events with single raising


        public static async Task<TResult> RaiseSingleAsync<T, TResult>(T args) where T : IDomainEvent
        {

            if (actions != null)
            {
                var delegateFound = actions.FirstOrDefault(w => w is Func<T, Task<TResult>>);
                if (delegateFound != null) return await ((Func<T, Task<TResult>>)delegateFound)(args);
            }
            else
            {
                ValidateCurrentContainer();
                var handler = Container.GetService<IHandle<T, TResult>>();
                return await handler?.HandleAsync(args);
            }
            return default;
        }

        public static async Task RaiseSingleAsync<T>(T args) where T : IDomainEvent
        {
            if (actions != null)
            {
                var delegateFound = actions.FirstOrDefault(w => w is Func<T, Task>);
                if (delegateFound != null) await ((Func<T, Task>)delegateFound)(args);
            }
            else
            {
                ValidateCurrentContainer();
                var handler = Container.GetService<IHandle<T>>();
                if (handler != null) await handler.HandleAsync(args);
            }
        }

        #endregion Events with single raising

    }
}
