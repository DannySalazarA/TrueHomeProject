using Framework.DomainKernel.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueHome.Entities.DBModel;
using TrueHome.Entities.Events;
using TrueHome.Infrastructure.Contracts;
using TrueHome.Infrastructure.Impl;
using TrueHome.Provider;
using TrueHome.Provider.Contracts;

namespace TrueHome
{
    public static class Extension
    {
		public static void ConfigureServicesAndProviders(this IServiceCollection services)
		{
			
			services.AddTransient<IActivityRepository, ActivityRepository>();
			services.AddTransient<IActivityProvider, ActivityProvider>();
			services.AddTransient<IPropertyRepository, PropertyRepository>();
			services.AddTransient<IPropertyProvider, PropertyProvider>();

		}

		public static void ConfigureEvents(this IServiceCollection services)
		{
			services.AddTransient<IHandle<ActivityRequestingEvent, List<Activity>>, ActivityProvider>();
			services.AddTransient<IHandle<ActivityRequestingByPropertyIdEvent, List<Activity>>, ActivityProvider>();
			services.AddTransient<IHandle<PropertyRequestingEvent, List<Property>>, PropertyProvider>();
			services.AddTransient<IHandle<ActivityCreatingEvent, long>, ActivityProvider>();
			services.AddTransient<IHandle<ActivityUpdateScheduleEvent>, ActivityProvider>();
			services.AddTransient<IHandle<ActivityUpdateStatusEvent>, ActivityProvider>();

		}

		public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddCors(e =>
			{
				e.AddDefaultPolicy(build =>
				{
					var allowedOrigins = configuration.GetSection("CORS-Settings:Allow-Origins").Get<string[]>();

					build.AllowAnyHeader()
						.AllowAnyMethod()
						.WithOrigins(allowedOrigins);
				});
			});
		}
	}
}
