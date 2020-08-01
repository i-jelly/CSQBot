using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.orua.qngel.Code.Interface;
using Native.Sdk.Cqp.EventArgs;

namespace cn.orua.qngel.Code.Model
{
    class 我的余额_MsgHandler : IMsgHandler
    {

        private Base_SQLHelper SQL = new Base_SQLHelper();

        public string Handler(CQGroupMessageEventArgs e, Base_SQLHelper.SQLHelperData b)
        {
            if (!SQL.UserExists(b, e.FromQQ)) return "不认识的孩子呢";
            SQL.AddFavorEveryChat(b, e.FromQQ);
            return "余额是" + SQL.GetCash(b, e.FromQQ) + "水晶";
        }
    }
}
