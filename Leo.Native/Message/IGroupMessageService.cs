using System;
using System.Collections.Generic;
using System.Text;

namespace Leo.Native.Message
{
    public interface IGroupMessageService
    {
        /// <summary>
        /// 异步队列添加。
        /// </summary>
        /// <param name="message"></param>
        void AddAsync(GroupMessage message);

        IEnumerable<MessageCount> GetTopMessageCounts(int top,long groupId,DateTime start,DateTime end);
    }

}
