using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cn.orua.qngel.Code.Interface;
using Native.Sdk.Cqp.EventArgs;

namespace cn.orua.qngel.Code.Model
{
    class 签到_MsgHandler : IMsgHandler
    {

        private Base_SQLHelper SQL = new Base_SQLHelper();
        private static readonly TimeSpan LateNight = DateTime.Parse("2:00").TimeOfDay;
        private static readonly TimeSpan Overnight = DateTime.Parse("4:00").TimeOfDay;
        private static readonly TimeSpan EarlyMorning = DateTime.Parse("6:00").TimeOfDay;
        private static readonly TimeSpan Morning = DateTime.Parse("9:00").TimeOfDay;
        private static readonly TimeSpan Forenoon = DateTime.Parse("11:00").TimeOfDay;
        private static readonly TimeSpan Noon = DateTime.Parse("13:00").TimeOfDay;
        private static readonly TimeSpan Afternoon = DateTime.Parse("17:00").TimeOfDay;
        private static readonly TimeSpan Night = DateTime.Parse("22:00").TimeOfDay;
        
        

        public string Handler(CQGroupMessageEventArgs e, Base_SQLHelper.SQLHelperData b)
        {
            if (!SQL.UserExists(b, e.FromQQ)) return "不认识的孩子呢";
            SQL.AddFavorEveryChat(b, e.FromQQ);
            TimeSpan t = DateTime.Now.TimeOfDay;
            if (SQL.RenewSign(b, e.FromQQ))
            {
                if(t < LateNight)
                {
                    return "记得保温杯多泡枸杞\n签到成功,获得水晶" + SQL.DailySignWithCashUpdate(b,e.FromQQ).ToString();
                }
                else if (t < Overnight)
                {
                    return "在？才八点\n签到成功,获得水晶" + SQL.DailySignWithCashUpdate(b, e.FromQQ).ToString();
                }
                else if(t < EarlyMorning)
                {
                    return "今天好像很早desu,补作业？\n签到成功,获得水晶" + SQL.DailySignWithCashUpdate(b, e.FromQQ).ToString();
                }
                else if(t < Morning)
                {
                    return "早上好~\n签到成功,获得水晶" + SQL.DailySignWithCashUpdate(b, e.FromQQ).ToString();
                }
                else if(t < Forenoon)
                {
                    return "上午好!\n签到成功,获得水晶" + SQL.DailySignWithCashUpdate(b, e.FromQQ).ToString();
                }
                else if(t < Noon)
                {
                    return "做懒狗舒服吗？\n签到成功,获得水晶" + SQL.DailySignWithCashUpdate(b, e.FromQQ).ToString();
                }
                else if (t < Afternoon)
                {
                    return "午安~\n签到成功,获得水晶" + SQL.DailySignWithCashUpdate(b, e.FromQQ).ToString();
                }
                else if (t < Night)
                {
                    return "是展现真正技术的时候了！\n签到成功,获得水晶" + SQL.DailySignWithCashUpdate(b, e.FromQQ).ToString();
                }
                else
                {
                    return "[CQ:image,file=rand/5.jpg]\n签到成功,获得水晶" + SQL.DailySignWithCashUpdate(b, e.FromQQ).ToString();
                }
            }
            return "签过到了";
        }
    }
}
