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
    class 切噜_MsgHandler : IMsgHandler
    {

        private static readonly String ErrMsg = "切、切噜太长切不动勒切噜噜...";
        private static readonly Dictionary<string, int> cheru = new Dictionary<string, int> {
            {"切",0},{"卟",1 },{"叮",2 },{"咧",3 },{"哔",4 },{"唎",5 },{"啪",6 },{"啰",7 },{"啵",8 },{"嘭",9 },{"噜",10 },{"噼",11 },{"巴",12 },{"拉",13 },{"蹦",14 },{"铃",15 }};

        public string Handler(CQGroupMessageEventArgs e, Base_SQLHelper.SQLHelperData b)
        {
            int Prefix = "切噜～[CQ:emoji,id=9834]".Length;
            String Msg = e.Message.Text.Trim();
            String Encrypted = Msg.Substring(Msg.IndexOf("#") + 1);
            if (Encrypted.Length < 21) return ErrMsg;
            if (Encrypted.Substring(0, Prefix) != "切噜～[CQ:emoji,id=9834]") return ErrMsg;
            Encrypted = Encrypted.Substring(Prefix).Trim();
            try
            {
                //Regex reg = new Regex(@"\%5Cu(\w{4})");
                Regex reg = new Regex("切[{切卟叮咧哔唎啪啰啵嘭噜噼巴拉蹦铃}]+");
                String result = reg.Replace(Encrypted, delegate (Match m)
                {
                    return Cheru2Word(m.Groups[0].Value);
                });
                return "你的切噜是:\n" + result;
            }
            catch
            {
                return ErrMsg;
            }
        }
        /// <summary>
        /// 切噜词转为GB18030的Byte
        /// 两个切噜字为一个字节
        /// 第一个字为高四位,第二个字为低四位
        /// </summary>
        /// <param name="e"></param>
        /// <returns>解密完成的字符</returns>
        private String Cheru2Word(String e)
        {
            string hexStr = e.Substring(1);
            byte[] _str = new byte[hexStr.Length + 1];
            for (int IndexOfStr = 0; IndexOfStr < hexStr.Length; IndexOfStr += 2)
            {
                _str[IndexOfStr / 2] = (byte)(cheru[hexStr.Substring(IndexOfStr, 1)]);
                _str[IndexOfStr / 2] += (byte)(cheru[hexStr.Substring(IndexOfStr + 1, 1)] << 4);
            }
            return Encoding.GetEncoding("GB2312").GetString(_str).Replace("\0","").Trim();
        }
    }
}
