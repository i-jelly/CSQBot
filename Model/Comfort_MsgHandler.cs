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
    class Comfort_MsgHandler : IMsgHandler
    {

        public string Handler(CQGroupMessageEventArgs e)
        {
            /**if (!SQL.HasTable(b))
            {
                return "";
            }
            if (SQL.UserExists(b,e.FromQQ))
            {
                SQL.AddFavorEveryChat(b, e.FromQQ);
                if (SQL.GetFavor(b,e.FromQQ) > 500)
                {
                    return "摸摸头";
                }
            }**/
            return "";
        }
    }
}
