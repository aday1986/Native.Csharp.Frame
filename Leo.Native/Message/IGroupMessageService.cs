using System;
using System.Collections.Generic;
using System.Text;

namespace Leo.Native.Message
{
    public interface IGroupMessageService
    {
        bool Add(GroupMessage message);

        IEnumerable<MessageCount> GetTopMessageCounts(int top,long groupId,DateTime start,DateTime end);
    }

}
