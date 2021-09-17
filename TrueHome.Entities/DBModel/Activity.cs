using Framework.DomainKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueHome.Entities.Events;
using TrueHome.Entities.Exceptions;

namespace TrueHome.Entities.DBModel
{
    public class Activity
    {
        public Activity()
        {
                
        }
        public Activity(int property_id, DateTime schedule, string title)
        {
            this.property_id = property_id;
            this.schedule = schedule;
            this.title = title;
        }
        
        public int id { get; set; }
        public int property_id { get; set; }
        public DateTime schedule { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string status { get; set; }
        public string condicion { get; set; }

        private Activity activityRequestParameter { get; set; }

        public static async Task<List<Activity>> GetAsync()
        {
            List<Activity> activities = await DomainEvents.RaiseSingleAsync<ActivityRequestingEvent, List<Activity>>(new ActivityRequestingEvent());
            return activities;
        }

        public static async Task<List<Activity>> GetAsyncByPropertyId(int propertyId)
        {
            List<Activity> activities = await DomainEvents.RaiseSingleAsync<ActivityRequestingByPropertyIdEvent, List<Activity>>(new ActivityRequestingByPropertyIdEvent(propertyId));
            foreach (var item in activities)
            {
                switch (item.status)
                {
                    case "Activa":
                        if (item.schedule > DateTime.Now)
                            item.condicion = "Pendiente a realizar";
                        else
                            item.condicion = "Atrasada";
                        break;
                    case "Realizada":
                        item.condicion = "Finalizada";
                        break;
                    default:
                        break;
                }
            }
            return activities;
        }

        public async Task<long> CreateAsync()
        {      
            await ValidateSchedule(property_id, schedule);
            this.created_at = DateTime.Now;
            this.updated_at = DateTime.Now;
            this.status = "Activa";
            long result = await DomainEvents.RaiseSingleAsync<ActivityCreatingEvent, long>(new ActivityCreatingEvent(this));

            return result;
        }

        public async Task UpdateScheduleActivity()
        {
            await ValidateSchedule(property_id, schedule);
            this.updated_at = DateTime.Now;
            this.status = "Activa";
            await DomainEvents.RaiseSingleAsync(new ActivityUpdateScheduleEvent(this));
        }

        public async Task UpdateStatusActivity()
        {
            //await ValidateSchedule(property_id, schedule);
            this.updated_at = DateTime.Now;
            await DomainEvents.RaiseSingleAsync(new ActivityUpdateStatusEvent(this));
        }


        //Validate Method
        public async Task ValidateSchedule(int propertyId, DateTime schedule)
        {
            List<Activity> activities = await DomainEvents.RaiseSingleAsync<ActivityRequestingByPropertyIdEvent, List<Activity>>(new ActivityRequestingByPropertyIdEvent(propertyId));
            var activitiesSchedule = activities.Where(x => x.schedule == schedule);
            if (activitiesSchedule != null && activitiesSchedule.Count() > 0)
            {
                throw new ExistingScheduleException();
            }
        }

        //update y mando llamar el metodo de la validacion de fecha.

        //update status
    }
}
