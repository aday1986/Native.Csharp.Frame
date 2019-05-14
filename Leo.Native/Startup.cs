using Leo.Data;
using Leo.Data.Dapper;
using Leo.Logging.File;
using Leo.Logging.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Leo.Config;
using Leo.Native.Commands;
using Leo.Native.Message;
using System.Linq;
using Native.Csharp.Sdk.Cqp;

namespace Leo.Native
{
    public class Startup
    {
        public const string AppId = "cn.zkbar.leo";
        public static CqApi CqApi { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }
        static Startup()
        {

            IServiceCollection services = new ServiceCollection();
           
            string dir = $"{AppDomain.CurrentDomain.BaseDirectory }\\Data\\{AppId}";
            services.AddDapperRepository(new SqliteDbProvider($"DataSource={dir}\\leo.db"));
            services.AddFileLogging($"{dir}\\log");
            services.AddSqliteLogging($"DataSource={dir}\\log\\log.db");
            services.AddScoped<ICommandService, CommandService>();
            services.AddSingleton<ITaskCollection,TaskCollection>();
            services.AddScoped<IGroupMessageService, GroupMessageService>();
            ServiceProvider = services.BuildServiceProvider();
        }

    }

    public static class ServiceProviderEx
    {
        public static T GetService<T>(this IServiceProvider provider)
        {
            return ServiceProviderServiceExtensions.GetService<T>(provider);
        }
    }
}
