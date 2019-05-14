using Leo.Data;
using Leo.Data.Dapper;
using Leo.Logging.File;
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
        public static CqApi CqApi { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }
        static Startup()
        {

            IServiceCollection services = new ServiceCollection();
            string dir = $"{AppDomain.CurrentDomain.BaseDirectory }\\Data";
            //string dir = $"{AppDomain.CurrentDomain.BaseDirectory }Data";
            // "D:\VS\Leo\Leo.Native\Leo.Native.Tests\bin\Debug\Data"
            string path = $"DataSource={dir}/leo.db";
            services.AddDapperRepository(new SqliteDbProvider(path));
            services.AddScoped<ICommandService, CommandService>();
            services.AddSingleton<ITaskCollection,TaskCollection>();
            services.AddScoped<IGroupMessageService, GroupMessageService>();
            ServiceProvider = services.BuildServiceProvider();
        }

        //public static string GetCount(Command cmd)
        //{
        //    var repository = provider.GetService<IRepository<GroupMessage>>();
        //    List<Condition> conditions = new List<Condition>();
        //    conditions.Add(new Condition() { Key = nameof(GroupMessage.FromQQ), Value = cmd.QQId, ConditionType = ConditionEnum.Equal });
        //    conditions.Add(new Condition() { Key = nameof(GroupMessage.FromGroup), Value = cmd.GroupId, ConditionType = ConditionEnum.Equal });
        //    var resul = repository.Query(conditions);
        //    return $"您在本群中总共发送了{resul.Count()}条信息。";
        //}
    }

    public static class ServiceProviderEx
    {
        public static T GetService<T>(this IServiceProvider provider)
        {
            return ServiceProviderServiceExtensions.GetService<T>(provider);
        }
    }
}
