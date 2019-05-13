using Leo.ThirdParty.Dapper;
using System;
using System.Collections.Generic;

namespace Leo.Native.Message
{
    public class GroupMessageService : IGroupMessageService
    {
        private readonly Data.IRepository<GroupMessage> repository;
        private readonly Data.IDbProvider dbProvider;

        public GroupMessageService(Leo.Data.IRepository<GroupMessage> repository, Leo.Data.IDbProvider dbProvider)
        {
            this.repository = repository;
            this.dbProvider = dbProvider;
        }

        public bool Add(GroupMessage message)
        {
            repository.Add(message);
            return repository.SaveChanges() > 0;
        }

        public IEnumerable<MessageCount> GetTopMessageCounts(int top, long groupId, DateTime start, DateTime end)
        {
            using (var db = dbProvider.CreateConnection())
            {
                string sql = "select FromQQ as QQId,Count(1) as 'Count',@start as StartDate,@end as EndDate " +
                    "from groupmessage " +
                    "where MsgDate>=@start and MsgDate<=@end  " +
                    "group by qqid order by Count desc limit 0,@top";
                return db.Query<MessageCount>(sql, new { start, end, top });
            }
        }
    }

}
