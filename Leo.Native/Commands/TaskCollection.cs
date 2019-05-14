using System;
using System.Collections.Generic;
using System.Text;
using Native.Csharp.Sdk.Cqp;
using Native.Csharp.Sdk.Cqp.Model;
using Native.Csharp.Sdk.Cqp.Enum;

namespace Leo.Native.Commands
{

    public class TaskCollection : Dictionary<string, Task>, ITaskCollection
    {
        private readonly Message.IGroupMessageService messageService;

        public TaskCollection(Leo.Native.Message.IGroupMessageService messageService)
        {
            this.messageService = messageService;
            Init();
           
        }

        private void Init()
        {
            this.Add("测试",
                   new Task()
                   {
                       TaskName = "测试",
                       Func = (cmd) => { return $"[{cmd.QQId}]拥有在群[{cmd.GroupId}]发送命令[{cmd.TaskName}]的权限。"; }
                   });

            this.Add("测试1",
                  new Task()
                  {
                      TaskName = "测试1",
                      Func = (cmd) => { return $"[{cmd.QQId}]拥有在群[{cmd.GroupId}]发送命令[{cmd.TaskName}]的权限。"; }
                  });
            this.Add("活跃查询",
               new Task()
               {
                   TaskName = "活跃查询",
                   NeedValidation = false,
                   Func = HUOYUECHAXUN
               });
        }

        /// <summary>
        /// 活跃查询。
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private string HUOYUECHAXUN(Command command)
        {
            DateTime end = DateTime.Now;
            DateTime start=end.AddMonths(-1).AddDays(1);
            StringBuilder @string = new StringBuilder();
            @string.AppendLine($"查询时段：{start.ToString("yy-MM-dd")}至{end.ToString("yy-MM-dd")}");
            var results = messageService.GetTopMessageCounts(10, command.GroupId, start, end);
            int i = 1;
            foreach (var item in results)
            {
                string nike = string.Empty;
                if (Startup.CqApi.GetMemberInfo(command.GroupId, item.QQId, out GroupMember member)==0)
                {
                    if (string.IsNullOrEmpty(member.Card.Trim()))
                    {
                        nike = member.Nick;
                    }
                    else
                    {
                        nike = member.Card;
                    }
                }
                else
                {
                    nike = item.QQId.ToString();
                }
                @string.AppendLine($"{i.ToString("00")} - [{item.QQId}]{nike}:{item.Count}条。");
                i++;

            }
            return @string.ToString().Trim();
        }

        public TaskCollection(IDictionary<string, Task> dictionary) : base(dictionary)
        {
        }
    }
}
