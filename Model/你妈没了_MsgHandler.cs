using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.orua.qngel.Code.Interface;
using Native.Sdk.Cqp.EventArgs;

namespace cn.orua.qngel.Code.Model
{
    class 你妈没了_MsgHandler : IMsgHandler
    {
        public string Handler(CQGroupMessageEventArgs e)
        {
            return "滴滴滴,出发警报\n关键词：你妈没了";
        }
    }
}
