using System;

namespace Leo.Native.Commands
{
    /// <summary>
    /// 提供给<see cref="Command"/>执行的任务。
    /// </summary>
    public class Task
    {
        public string TaskName { get; set; }

        /// <summary>
        /// 是否需要验证权限。
        /// </summary>
        public bool NeedValidation { get; set; } = true;

        public Func<Command, string> Func { get; set; }
    }
}
