using Native.Csharp.Sdk.Cqp;
using Native.Csharp.Sdk.Cqp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Native.Csharp.App.Model;
using Native.Csharp.App.Interface;
using System.Diagnostics;
using Leo.Native.Message;
using Leo.Native.Commands;
using Leo.Native;
using Native.Csharp.Sdk.Cqp.Enum;

namespace Native.Csharp.App.Event
{
    public class Event_GroupMessage : IEvent_GroupMessage
    {
        private readonly IGroupMessageService messageService;
        private readonly ICommandService commandService;

        public Event_GroupMessage()
        {
            this.messageService = Startup.ServiceProvider.GetService<IGroupMessageService>();
            this.commandService = Startup.ServiceProvider.GetService<ICommandService>();
        }
        #region --公开方法--
        /// <summary>
        /// Type=2 群消息<para/>
        /// 处理收到的群消息
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupMessage(object sender, GroupMessageEventArgs e)
        {
            Debug.Print(e.Msg);
            messageService.Add(new GroupMessage()
            {
                MsgDate = DateTime.Now,
                MsgId = e.MsgId,
                FromGroup = e.FromGroup,
                FromQQ = e.FromQQ,
                IsAnonymousMsg = e.IsAnonymousMsg,
                Msg = e.Msg
            });

            if (commandService.IsCommand(e.Msg,out string taskName))
            {
                //暂时只让管理员允许发送
                if (Startup.CqApi.GetMemberInfo(e.FromGroup, e.FromQQ, out GroupMember member) == 0
                    && member.PermitType != PermitType.None)
                {
                    if (commandService.Execute(new Command()
                    {
                        CommandText = e.Msg,
                        GroupId = e.FromGroup,
                        QQId = e.FromQQ,
                        SendDate = DateTime.Now,
                        TaskName = taskName
                    }, out string message))
                    {
                        Common.CqApi.SendGroupMessage(e.FromGroup, message);
                    }
                    else //未能执行命令。
                    {
                        Common.CqApi.SendPrivateMessage(e.FromQQ, message);
                    }
                }
            }

        }

        /// <summary>
        /// Type=21 群私聊<para/>
        /// 处理收到的群私聊消息
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupPrivateMessage(object sender, PrivateMessageEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息


            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=11 群文件上传事件<para/>
        /// 处理收到的群文件上传结果
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupFileUpload(object sender, FileUploadMessageEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息
            // 关于文件信息, 触发事件时已经转换完毕, 请直接使用



            e.Handled = false;   // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=101 群事件 - 管理员增加<para/>
        /// 处理收到的群管理员增加事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupManageIncrease(object sender, GroupManageAlterEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=101 群事件 - 管理员减少<para/>
        /// 处理收到的群管理员减少事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupManageDecrease(object sender, GroupManageAlterEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=103 群事件 - 群成员增加 - 主动入群<para/>
        /// 处理收到的群成员增加 (主动入群) 事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupMemberJoin(object sender, GroupMemberAlterEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用，请注意使用对象等需要初始化(CoInitialize,CoUninitialize)。
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=103 群事件 - 群成员增加 - 被邀入群<para/>
        /// 处理收到的群成员增加 (被邀入群) 事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupMemberInvitee(object sender, GroupMemberAlterEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用, 请注意使用对象等需要初始化(ConIntialize, CoUninitialize).
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=102 群事件 - 群成员减少 - 成员离开<para/>
        /// 处理收到的群成员减少 (成员离开) 事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupMemberLeave(object sender, GroupMemberAlterEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用, 请注意使用对象等需要初始化(ConIntialize, CoUninitialize).
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=102 群事件 - 群成员减少 - 成员移除<para/>
        /// 处理收到的群成员减少 (成员移除) 事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupMemberRemove(object sender, GroupMemberAlterEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用, 请注意使用对象等需要初始化(ConIntialize, CoUninitialize).
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=302 群事件 - 群请求 - 申请入群<para/>
        /// 处理收到的群请求 (申请入群) 事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupAddApply(object sender, GroupAddRequestEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用, 请注意使用对象等需要初始化(ConIntialize, CoUninitialize).
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }

        /// <summary>
        /// Type=302 群事件 - 群请求 - 被邀入群 (机器人被邀)<para/>
        /// 处理收到的群请求 (被邀入群) 事件
        /// </summary>
        /// <param name="sender">事件的触发对象</param>
        /// <param name="e">事件的附加参数</param>
        public void ReceiveGroupAddInvitee(object sender, GroupAddRequestEventArgs e)
        {
            // 本子程序会在酷Q【线程】中被调用, 请注意使用对象等需要初始化(ConIntialize, CoUninitialize).
            // 这里处理消息



            e.Handled = false;  // 关于返回说明, 请参见 "Event_FriendMessage.ReceiveFriendMessage" 方法
        }
        #endregion
    }
}
