using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using cn.orua.qngel.Code.Interface;
using Native.Sdk.Cqp.EventArgs;

namespace cn.orua.qngel.Code.Model
{
    class 切噜一下_MsgHandler : IMsgHandler
    {
        private static readonly Dictionary<int, string> cheru = new Dictionary<int, string> {
            {0,"切"},{1,"卟" },{2,"叮" },{3,"咧"},{4,"哔"},{5,"唎"},{6,"啪"},{7,"啰"},{8,"啵"},{9,"嘭"},{10,"噜"},{11,"噼"},{12,"巴"},{13,"拉"},{14,"蹦"},{15,"铃"}};
        public string Handler(CQGroupMessageEventArgs e, Base_SQLHelper.SQLHelperData b)
        {
            String str = e.Message.Text.Substring(e.Message.Text.IndexOf("#") + 1);
            if (str.Contains("CQ")) return "切噜噜？";
            Regex reg = new Regex(@"\b");
            Regex re = new Regex(@"^\w+$");
            List<String> t = new List<string>();
            List<String> l = new List<string>();
            foreach(String i in reg.Split(str))
            {
                String _new = re.Replace(i,delegate( Match m)
                {
                    byte[] _str = Encoding.GetEncoding("GB2312").GetBytes(i);//获取GB编码Byte
                    for(int j = 0;j < _str.Length; j ++)
                    {
                        t.Add(cheru[_str[j] & 0x0f]);//取低四位
                        t.Add(cheru[(_str[j] & 0xf0) >> 4]);//取高四位
                    }
                    return "切"+String.Join("",t.ToArray());
                });
                t.Clear();
                l.Add(_new);
            }
            return "你的歪比巴布是:\n切噜～♪" + String.Join("",l.ToArray());
        }
    }
}
