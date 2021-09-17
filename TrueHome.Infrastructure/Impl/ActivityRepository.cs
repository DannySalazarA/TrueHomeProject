using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TrueHome.Infrastructure.Contracts;
using TrueHome.Entities.DBModel;
using Dapper;
using Npgsql;
using System.Linq;

namespace TrueHome.Infrastructure.Impl
{
    public class ActivityRepository : IActivityRepository
    {
        public IConfiguration Configuration { get; private set; }

        public ActivityRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;

        }

        public async Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            string sqlQueryActivity = $@"select * from public.Activity";

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("truehome")))
            {
                return await connection.QueryAsync<Activity>(sqlQueryActivity);
            }
        }

        public async Task<Activity> GetActivityByIdAsync(int id)
        {
            string sqlQueryActivity = $@"select * from public.Activity where id = @Id";

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("truehome")))
            {
                return await connection.QueryFirstAsync<Activity>(sqlQueryActivity, new { Id = id });
            }
        }

        public async Task<IEnumerable<Activity>> GetActivitiesByPropertyIdAsync(int propertyId)
        {
            string sqlQueryActivity = $@"select * from public.Activity where property_id = @PropertyId and schedule <= CURRENT_DATE + INTERVAL '14 day'
            and schedule >= current_date-3;";

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("truehome")))
            {
                return await connection.QueryAsync<Activity>(sqlQueryActivity, new { PropertyId = propertyId });
            }
        }

        public async Task<long> CreateActivityAsync(Activity activityParam)
        {
            string sqlInsertActivity = $@"INSERT INTO public.Activity (property_id, schedule, title, created_at, updated_at, status)
                                            VALUES (@property_id, @schedule, @title, @created_at, @updated_at, @status); SELECT currval(pg_get_serial_sequence('Activity','id'));";

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("truehome")))
            {
                var result = await connection.QueryAsync(sqlInsertActivity,
                    new { activityParam.property_id, activityParam.schedule, activityParam.title, activityParam.created_at, activityParam.updated_at, activityParam.status });

                var activityCreated = result.Single();
                return activityCreated.currval;
            }
        }

        public async Task UpdateScheduleActivityAsync(Activity activityParam)
        {
            string sqlUpdateScheduleActivity = $@"UPDATE public.Activity SET schedule = @schedule, updated_at = @updated_at WHERE Id = @id";

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("truehome")))
            {
                await connection.ExecuteAsync(sqlUpdateScheduleActivity,
                    new { activityParam.schedule, activityParam.updated_at, activityParam.id});
            }
        }

        public async Task UpdateStatusActivityAsync(Activity activityParam)
        {
            string sqlUpdateScheduleActivity = $@"UPDATE public.Activity SET status = @status, updated_at = @updated_at WHERE Id = @id";

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("truehome")))
            {
                await connection.ExecuteAsync(sqlUpdateScheduleActivity,
                    new { activityParam.status, activityParam.updated_at, activityParam.id });
            }
        }
    }
}
