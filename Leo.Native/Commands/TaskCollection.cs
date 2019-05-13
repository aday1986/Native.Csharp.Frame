using System.Collections.Generic;

namespace Leo.Native.Commands
{

    public class TaskCollection : Dictionary<string, Task>, ITaskCollection
    {
        public TaskCollection()
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

            //this.Add("活跃查询",
            //     new Task()
            //     {
            //         TaskName = "活跃查询",
            //         Func = GetCount,
            //         NeedValidation = false

            //     });
        }

        public TaskCollection(IDictionary<string, Task> dictionary) : base(dictionary)
        {
        }
    }
}
