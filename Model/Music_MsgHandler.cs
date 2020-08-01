using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Native.Tool.Http;
using Native.Sdk.Cqp.EventArgs;
using cn.orua.qngel.Code.Interface;
using System.Data;
using System.IO;
using Native.Sdk.Cqp;

namespace cn.orua.qngel.Code.Model
{
    class Music_MsgHandler : IMsgHandler
    {
        private String UA = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.117 Safari/537.36";
        private JsonTextReader reader;
        private Base_SQLHelper SQL = new Base_SQLHelper();


        public string Handler(CQGroupMessageEventArgs e, Base_SQLHelper.SQLHelperData b)
        {
            String Order = e.Message.Text.Trim().Substring(22).Trim();
            String Music = Order.Substring(Order.IndexOf("#") + 1);
            String raw;
            //return Music;
            if (!SQL.UserExists(b, e.FromQQ)) return "不认识的孩子呢";
            try
            {
                raw = Encoding.Default.GetString(HttpWebClient.Get("http://music.163.com/api/search/pc?limit=1&type=1&s=" + Music, UA));
                
            }
            catch
            {
                return "Network ERR";
            }
            try
            {
                reader = new JsonTextReader(new StringReader(raw));
                while (reader.Read())
                {
                    if(reader.TokenType.ToString() == "PropertyName" && reader.Value.ToString() == "id")
                    {
                        reader.Read();
                        SQL.AddFavorEveryChat(b, e.FromQQ);
                        return CQApi.CQCode_Music(int.Parse(reader.Value.ToString()), Native.Sdk.Cqp.Enum.CQMusicType.Netease).ToString();
                    }
                }
                return "Music NOT found";
            }
            catch
            {
                return "Results ERR";
            }
        }
    }
}
