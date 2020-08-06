using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.orua.qngel.Code.Interface;
using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;

namespace cn.orua.qngel.Code.Model
{
    class 我的好感_MsgHandler : IMsgHandler
    {
        //private Base_SQLHelper SQL = new Base_SQLHelper();

        public string Handler(CQGroupMessageEventArgs e)
        {
            /**if (!SQL.HasTable(b)) return "";
            if (SQL.UserExists(b,e.FromQQ))
            {
                SQL.AddFavorEveryChat(b, e.FromQQ);
                String _reply = SQL.GetFavor(b,e.FromQQ).ToString();
                return "好感度" + _reply;
            }**/
            return "数据库关闭";
        }
    }
}
