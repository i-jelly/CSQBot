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
using Newtonsoft.Json;

namespace cn.orua.qngel.Code.Model
{
    class 搜索涩图_MsgHandler : IMsgHandler
    {
        private class Img
        {
            public long id { get; set; }
            public String author { get; set; }
            public String source { get; set; }
            public int score { get; set; }
            public String sample_url { get; set; }
            public String rating { get; set; }
        }

        private String UA = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.117 Safari/537.36";
        private WebProxy proxy = new WebProxy("127.0.0.1",1081);
        private String ErrMsg = "没有切噜噜......";

        public string Handler(CQGroupMessageEventArgs e, Base_SQLHelper.SQLHelperData b)
        {
            String _Order = e.Message.Text.Trim();
            String _Tag = _Order.Contains("#") ? _Order.Substring(_Order.IndexOf("#") + 1).Trim():"";
            String Page = _Tag == "" ?  new Random().Next(300).ToString():"1" ;
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://konachan.net/post.json?limit=30&tags=underwear " + _Tag + "&page=" + Page);
            request.UserAgent = UA;
            request.Proxy = proxy;

            Img Infos;
            try
            {
                String raw = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("UTF-8")).ReadToEnd();
                var data = JsonConvert.DeserializeObject<Img[]>(raw);
                while (true)
                {
                    Infos = data[new Random().Next(data.Length)];
                    if (Infos.rating != "e" && Infos.score > 40)
                    {
                        request = (HttpWebRequest)WebRequest.Create(Infos.sample_url);
                        break;
                    }
                }
                request.UserAgent = UA;
                request.Proxy = proxy;
                Stream FileStream = request.GetResponse().GetResponseStream();
                String Filename = "rand/" + Infos.id.ToString() + "." + Infos.sample_url.Split(new char[] { '.' }).Last();
                FileStream LocalImg = new FileStream("data/image/" + Filename, FileMode.OpenOrCreate,FileAccess.Write);
                byte[] Buff = new byte[512];
                int Count = 0;
                while ((Count = FileStream.Read(Buff, 0, Buff.Length)) > 0)
                {
                    LocalImg.Write(Buff, 0, Count);
                }
                FileStream.Close();
                LocalImg.Close();
                return CQApi.CQCode_Image(Filename).ToString() + (Infos.author != "" ? "作者:" + Infos.author + "\n" : "") + (Infos.source != ""?"来源:" + Infos.source:"");
            }
            catch
            {
                return ErrMsg;
            }
        }
    }
}
