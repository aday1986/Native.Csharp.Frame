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
        /// 判断命令是否存在，是有拥有执行权限，执行命令。
        /// </summary>
        /// <param name="command"></param>
        /// <param name="message">返回的信息。</param>
        /// <returns>是否执行成功。</returns>
        bool TryExecute(Command command, out string message);

        /// <summary>
        /// 判断是否是命令任务名称格式。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="taskName"></param>
        /// <returns></returns>
        bool TryGetTaskName(string message,out string taskName);

   
    }
}
