using Leo.Data;

namespace Leo.Native.Commands
{
    /// <summary>
    /// 任务调用权限。
    /// </summary>
    [Table(TableName = "TaskAuthority")]
    public class TaskAuthority
    {
        [Column(IsPrimaryKey = true)]
        public long GroupId { get; set; }

        [Column(IsPrimaryKey = true)]
        public long QQId { get; set; }

        [Column(IsPrimaryKey = true)]
        public string TaskName { get; set; }
    }
}
