using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.Model;


namespace cn.orua.qngel.Code
{
    public class Event_GroupMessage : IGroupMessage
    {
        private Base_MsgHandler handler = new Base_MsgHandler();

        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            while (handler.isBusy) { }
            if(handler.handle(e)) e.Handler = true;//如果机器人回复了消息则阻塞
        }
    }
}
