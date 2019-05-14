using Leo.Data;
using Leo.ThirdParty.Dapper;
using Leo.Util;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leo.Native.Message
{
    public class GroupMessageService : IGroupMessageService
    {
        private readonly IRepository<GroupMessage> repository;
        private readonly IDbProvider dbProvider;

        public GroupMessageService(IRepository<GroupMessage> repository, IDbProvider dbProvider)
        {
            this.repository = repository;
            this.dbProvider = dbProvider;
            queue = new WorkQueue<GroupMessage>(1000, (s, e) =>
            {
                repository.AddRange(e.Item);
                repository.SaveChanges();
            });
        }

        private WorkQueue<GroupMessage> queue = null;
        public async void AddAsync(GroupMessage message)
        {
            await Task.Run(() => { queue.EnqueueItem(message); });
        }

        public IEnumerable<MessageCount> GetTopMessageCounts(int top, long groupId, DateTime start, DateTime end)
        {
            using (var db = dbProvider.CreateConnection())
            {
                string sql = "select FromQQ as QQId,Count(1) as 'Count',@start as StartDate,@end as EndDate " +
                    "from groupmessage " +
                    "where MsgDate>=@start and MsgDate<=@end and FromGroup=@groupId  " +
                    "group by qqid order by Count desc limit 0,@top";
                return db.Query<MessageCount>(sql, new { start, end, top, groupId });
            }
        }
    }

}
