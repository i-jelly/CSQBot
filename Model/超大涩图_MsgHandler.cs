using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.orua.qngel.Code.Interface;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp;
using Native.Tool.Http;
using System.Net;
using System.IO;

namespace cn.orua.qngel.Code.Model
{
    class 超大涩图_MsgHandler : IMsgHandler
    {
        public string Handler(CQGroupMessageEventArgs e, Base_SQLHelper.SQLHelperData b)
        {
            WebClient _Dl = new WebClient();
            String Filename = "rand/" + new Random().Next(10000, 99999).ToString() + ".jpg";
            try
            {
                _Dl.DownloadFile("http://ssr0.cn:8000/ACG", "data/image/" + Filename);
                return CQApi.CQCode_Image(Filename).ToString();
            }
            catch
            {
                return "下载出错力";
            }
            
        }
    }
}
