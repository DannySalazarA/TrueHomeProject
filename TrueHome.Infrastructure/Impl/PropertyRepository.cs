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

namespace TrueHome.Infrastructure.Impl
{
    public class PropertyRepository : IPropertyRepository
    {
        public IConfiguration Configuration { get; private set; }

        public PropertyRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;

        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync()
        {
            string sqlQueryProperty = $@"select * from public.Property";

            using (var connection = new NpgsqlConnection(Configuration.GetConnectionString("truehome")))
            {
                return await connection.QueryAsync<Property>(sqlQueryProperty);
            }
        }
    }
}
