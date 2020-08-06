using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.orua.qngel.Code.Interface;
using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Model;

namespace cn.orua.qngel.Code.Model
{
    class Register_OrderHandler : IMsgHandler
    {

        public String Handler(CQGroupMessageEventArgs e)
        {
            /**if (!SQL.HasTable(b)) SQL.NewGroupTable(b);
            if (!SQL.UserExists(b,e.FromQQ))
            {
                if (SQL.AddUser(b,e.FromQQ))
                {
                    return "注册成功";
                }
                return "注册失败";
            }**/
            return "注册功能关闭";
        }
       
    }
}
