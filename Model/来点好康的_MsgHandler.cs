using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.Model;
using cn.orua.qngel.Code.Interface;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp;

namespace cn.orua.qngel.Code.Model
{
    class 来点好康的_MsgHandler : IMsgHandler
    {
        //private Base_SQLHelper SQL = new Base_SQLHelper();

        public string Handler(CQGroupMessageEventArgs e)
        {
            //if (SQL.UserExists(b, e.FromQQ)) SQL.AddFavorEveryChat(b, e.FromQQ);
            return CQApi.CQCode_Image("pixiv/" + new Base_FileHelper().RandomGetImg("data/image/pixiv")).ToString();
        }
    }
}
