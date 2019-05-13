using System;
using System.Collections.Generic;
using System.Text;

namespace Leo.Native.Commands
{
    public interface ICommandService
    {
        
        /// <summary>
        /// 获取所有任务权限。
        /// </summary>
        /// <returns></returns>
        IEnumerable<TaskAuthority> GetAllAuthority();
        /// <summary>
        /// 添加任务权限。
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="qqId"></param>
        /// <returns></returns>
        IEnumerable<TaskAuthority> GetTaskAuthorities(long groupId, long qqId);

        bool AddTaskAuthority(TaskAuthority taskAuthority);

        /// <summary>
        /// 移除命令权限。
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        bool RemoveTaskAuthority(TaskAuthority  taskAuthority);

        /// <summary>
        /// 是否拥有权限。
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        bool HasAuthority(Command command);

        /// <summary>
        /// 执行命令。
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        bool Execute(Command command, out string message);

        bool IsCommand(string message,out string commandName);

   
    }
}
