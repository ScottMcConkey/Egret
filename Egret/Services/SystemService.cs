using Egret.DataAccess;
//using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System;
using Npgsql;

namespace Egret.Services
{
    public class SystemService : BaseService
    {
        private IConfiguration _config;

        public SystemService(EgretDbContext context, IConfiguration config)
            : base(context)
        {
            _config = config;
        }

        public string GetEgretVersion()
        {
            string version;

            //var config = new ConfigurationExtensions.Configuration();
            var connString = _config.GetConnectionString("DefaultConnection");
            var sqlConnection = new NpgsqlConnection(connString);
            sqlConnection.Open();
            var command = new NpgsqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @"select substr(max(""MigrationId""), 16) from public.""__EFMigrationsHistory""",
                Connection = sqlConnection
            };
            version = (string)command.ExecuteScalar();
  

            return version;
        }
    }
}
