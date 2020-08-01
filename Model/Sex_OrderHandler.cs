using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Native.Sdk.Cqp.Model;
using cn.orua.qngel.Code.Interface;
using Native.Sdk.Cqp.EventArgs;

namespace cn.orua.qngel.Code.Model
{
    class Sex_OrderHandler : IMsgHandler
    {
        public string Handler(CQGroupMessageEventArgs e, Base_SQLHelper.SQLHelperData b)
        {
            if(e.FromGroup.SetGroupMemberBanSpeak(e.FromQQ, TimeSpan.FromMinutes(1)))
            {
                return "憨批";
            }
            return "有绿帽子就把你🐎都扬了";
        }
    }
}
