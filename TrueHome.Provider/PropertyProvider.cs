using Framework.DomainKernel.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueHome.Entities.DBModel;
using TrueHome.Entities.Events;
using TrueHome.Infrastructure.Contracts;
using TrueHome.Provider.Contracts;

namespace TrueHome.Provider
{
    public class PropertyProvider :IPropertyProvider, IHandle<PropertyRequestingEvent, List<Property>>
    {
        public PropertyProvider(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<List<Property>> GetAsync()
        {
            return await Property.GetAsyncPropertiesEnable();
        }

        public async Task<List<Property>> HandleAsync(PropertyRequestingEvent args)
        {
            return await _propertyRepository.GetPropertiesAsync() as List<Property>;
        }

        public List<Property> Handle(PropertyRequestingEvent args)
        {
            throw new NotImplementedException();
        }

        IPropertyRepository _propertyRepository;
    }
}
