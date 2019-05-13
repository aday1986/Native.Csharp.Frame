using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leo.Data;

namespace Leo.Native.Message
{
    public class GroupMessage
    {
        /// <summary>
        /// 自增主键。
        /// </summary>
        [Column(IsPrimaryKey = true,IsIdentity =true)]
        public int Id { get; set; }

        /// <summary>
        /// 消息Id
        /// </summary>
        public int MsgId { get; set; }

        public DateTime MsgDate { get; set; }

        /// <summary>
		/// 来源QQ
		/// </summary>
		public long FromQQ { get; set; }

        /// <summary>
        /// 来源群号
        /// </summary>
        public long FromGroup { get; set; }

        /// <summary>
        /// 是否是匿名消息
        /// </summary>
        public bool IsAnonymousMsg { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg { get; set; }


    }
}
