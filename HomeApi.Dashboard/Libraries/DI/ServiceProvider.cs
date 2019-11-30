using System;
using System.Collections.Generic;

namespace HomeApi.Dashboard.Libraries.DI
{
    public class ServiceProvider : IServiceProvider
    {
        protected Dictionary<Type, IService> Services { get; } = new Dictionary<Type, IService>();
        
        public void AddService<TService>(TService service)
            where TService : IService
        {
            var serviceType = typeof(TService);

            if (Services.ContainsKey(typeof(TService)))
            {
                throw new ArgumentException($"Service of type {serviceType.Name} is already defined");
            }

            Services.Add(serviceType, service);
        }

        public object GetService(Type serviceType)
        {
            if (Services.ContainsKey(serviceType))
            {
                return Services[serviceType];
            }

            throw new ArgumentException($"No service exists for type {serviceType.Name}");
        }

        public TService GetService<TService>()
            where TService : IService
        {
            return (TService) GetService(typeof(TService));
        }

        public async void Initialise()
        {
            foreach (var service in Services.Values)
            {
                await service.InitialiseAsync();
            }
        }
    }
}
