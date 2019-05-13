using System;
using System.Collections.Generic;
using System.Text;

namespace Leo.Native.Commands
{
    /// <summary>
    /// 表示一组可执行的任务字典，TaskName/<see cref="Task"/>。
    /// </summary>
    public interface ITaskCollection:IDictionary<string, Task>
    {
     
    }
}
