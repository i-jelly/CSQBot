﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.orua.qngel.Code.Interface;
using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;

namespace cn.orua.qngel.Code.Model
{
    class 学习套餐_MsgHandler : IMsgHandler
    {
        //private Base_SQLHelper SQL = new Base_SQLHelper();
        public string Handler(CQGroupMessageEventArgs e)
        {
            if (e.FromGroup.SetGroupMemberBanSpeak(e.FromQQ, TimeSpan.FromHours(2)))
            {
                //if (SQL.UserExists(b, e.FromQQ)) SQL.AddFavorEveryChat(b, e.FromQQ);
                return CQApi.CQCode_Image("sm/cai.jpg").ToString();
            }
            return "臣妾做不到啊";
        }
    }
}
