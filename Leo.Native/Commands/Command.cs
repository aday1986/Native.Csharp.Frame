using System;
using System.Linq;

namespace Leo.Native.Commands
{
    /// <summary>
    /// 命令。
    /// </summary>
    public class Command
    {
        /// <summary>
        /// 默认分隔符。
        /// </summary>
        public const char Separator= '#';

        public DateTime SendDate { get; set; }

        public long GroupId { get; set; }

        public long QQId { get; set; }

        public string CommandText { get; set; }

        public string TaskName { get; set; }

    }

   
}
