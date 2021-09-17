using Framework.DomainKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueHome.Entities.Events;

namespace TrueHome.Entities.DBModel
{
    public class Property
    {
        public int id { get; set; }
        public string title { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime disabled_at { get; set; }
        public string status { get; set; }

        public static async Task<List<Property>> GetAsyncPropertiesEnable()
        {
            List<Property> properties = await DomainEvents.RaiseSingleAsync<PropertyRequestingEvent, List<Property>>(new PropertyRequestingEvent());
            return properties.Where(x => x.status == "Activada").ToList();
        }
    }
}
