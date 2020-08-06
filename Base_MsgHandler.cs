using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Unity;
using System.Data.SQLite;

using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.Model;
using cn.orua.qngel.Code.Interface;
using cn.orua.qngel.Code.Model;

namespace cn.orua.qngel.Code
{
    public class Base_MsgHandler
    {

        
        private String Msg;
        private String Reply;
        private Random rand = new Random();
        private bool IsInit = false;
        private Timer tim = new Timer();
        private Native.Sdk.Cqp.Model.Group _GroupID;
        //private Base_SQLHelper SQL = new Base_SQLHelper();
        //private static readonly String[] RandomOrder = new string[] {"歪比歪比" };

        
        private Base_GroupState GroupState = new Base_GroupState();
        private Base_Config GroupConfig = new Base_Config();
        private List<long> BlockList = new List<long>();
        private Base_MQTTHelper MQTTHelper = new Base_MQTTHelper();
        private List<CQGroupMessageEventArgs> dd = new List<CQGroupMessageEventArgs>();
        //private Dictionary<long, Base_SQLHelper.SQLHelperData> SQLPool = new Dictionary<long, Base_SQLHelper.SQLHelperData>();

        public bool isBusy = false;
        public UnityContainer OrderContainer = new UnityContainer();
        public UnityContainer SimpleContainer = new UnityContainer();
        //public UnityContainer RandomContainer = new UnityContainer();
        //private List<long> ListenList = new List<long>();


        /// <summary>
        /// 初始化函数，负责Timer以及MQTT的初始化
        /// 并注册对应命令处理函数的Container
        /// </summary>
        public void init()
        {
            IsInit = true;
            tim.Enabled = true;
            tim.Interval = 2000;
            tim.Elapsed += new ElapsedEventHandler(OnTimer);
            tim.Start();

            if (!MQTTHelper.IsInit)
            {
                MQTTHelper.Init();
                MQTTHelper.IsInit = true;
            }

            OrderContainer.RegisterType<IMsgHandler, Register_OrderHandler>("注册");
            OrderContainer.RegisterType<IMsgHandler, Sex_OrderHandler>("啪啪啪");
            OrderContainer.RegisterType<IMsgHandler, Music_MsgHandler>("点歌");
            OrderContainer.RegisterType<IMsgHandler, 摸一摸_MsgHandler>("摸一摸");
            OrderContainer.RegisterType<IMsgHandler, 我的好感_MsgHandler>("我的好感");
            OrderContainer.RegisterType<IMsgHandler, 安眠套餐_MsgHandler>("安眠套餐");
            OrderContainer.RegisterType<IMsgHandler, 学习套餐_MsgHandler>("学习套餐");
            OrderContainer.RegisterType<IMsgHandler, 切噜_MsgHandler>("切噜噜");
            OrderContainer.RegisterType<IMsgHandler, 切噜一下_MsgHandler>("切噜一下");
            OrderContainer.RegisterType<IMsgHandler, 搜索涩图_MsgHandler>("搜索涩图");

            SimpleContainer.RegisterType<IMsgHandler, Comfort_MsgHandler>("哭了");
            SimpleContainer.RegisterType<IMsgHandler, 你妈没了_MsgHandler>("你妈没了");
            SimpleContainer.RegisterType<IMsgHandler, 来点好康的_MsgHandler>("来点好康的");
            //SimpleContainer.RegisterType<IMsgHandler, 签到_MsgHandler>("签到");//签到函数暂时没完成
            SimpleContainer.RegisterType<IMsgHandler, 我的余额_MsgHandler>("我的余额");
            SimpleContainer.RegisterType<IMsgHandler, 超大涩图_MsgHandler>("超大涩图");


        }
        /// <summary>
        /// 主处理函数，负责主要流程即注册所有消息处理函数->缓存->发送处理函数返回的回复
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool handle(CQGroupMessageEventArgs e)
        {
            if (!IsInit) init();
            _GroupID = e.FromGroup;
            if (BlockList.Contains(_GroupID)) return false;//处于屏蔽列表不处理任何信息
            if (!GroupState.GroupState.ContainsKey(_GroupID))//若缓存中不存在此群
            {
                GroupState.AddNewGroupToList(_GroupID);//将群加入缓存
                //Base_SQLHelper.SQLHelperData _new = new Base_SQLHelper.SQLHelperData();
                //_new.Connection = new SQLiteConnection("Data Source=data/db/" + _GroupID.ToString() + ".db");
                //_new.command = new SQLiteCommand();
                //SQLPool[_GroupID] = _new;
                if (GroupConfig.HasConfig(_GroupID))//若存在对应ConfigINI文件则读入相关固化配置
                {
                    GroupState.GroupState[_GroupID].AllowRepeat = GroupConfig.AllowRepeat(_GroupID);
                    GroupState.GroupState[_GroupID].AllowReply = GroupConfig.AllowReply(_GroupID);
                    GroupState.GroupState[_GroupID].AllowR18 = GroupConfig.AllowR18(_GroupID);
                    GroupState.GroupState[_GroupID].Admin = GroupConfig.GetAllAdmin(_GroupID);
                    if (GroupConfig.Allowdd(_GroupID)) dd.Add(e);
                    
                    if (!GroupConfig.IsListen(_GroupID))//ConfigINI中记录为BlackList则加入屏蔽列表并退出
                    {
                        BlockList.Add(_GroupID);
                        return false;
                    }
                }
                else
                {
                    GroupConfig.NewGroupConfig(_GroupID);//不存在ConfigINI文件则新建
                }
                //return false;//缓存中未命中的情况下第一次不会发任何信息
            }
            if (!GroupState.GroupState[_GroupID].AllowReply) return false;//不允许回复直接退出
                        
            //正式命令处理部分
            Msg = e.Message.Text.Trim();
            if(rand.Next(100) > 95)
            {//随机无意义表情包
                _GroupID.SendGroupMessage(CQApi.CQCode_Image("sm/" + new Base_FileHelper().RandomGetImg("data/image/sm")));
                return true;
            }
            if(Msg.Length < 21)
            {//无AT的短语句匹配注册命令
                //匹配短语
                //首先尝试获取称呼,若存在称呼则删去后再匹配
                //if(SQL.UserExists(SQLPool[_GroupID],e.FromQQ) && Msg.Contains(SQL.GetCalled(SQLPool[_GroupID], e.FromQQ)))
                //{
                //    //TODO...
                //}
                if (SimpleContainer.IsRegistered<IMsgHandler>(Msg))
                {
                    Reply = SimpleContainer.Resolve<IMsgHandler>(Msg).Handler(e);
                    if(Reply != "")
                    {
                        _GroupID.SendGroupMessage(Reply);
                        return true;
                    }
                }
            }
            else //被AT后的处理
            {
                if(Msg.Substring(0,21) == @"[CQ:at,qq=3178223002]" && Msg.Length >= 22)
                {
                    String Order = Msg.Substring(22).Trim();
                    //_GroupID.SendGroupMessage(Order.Substring(0, Order.IndexOf("#") - 1));
                    if (Order.Contains("#") && Order.IndexOf("#") > 0)
                    {
                        Order = Order.Substring(0, Order.IndexOf("#"));
                    }
                    if (OrderContainer.IsRegistered<IMsgHandler>(Order))
                    {
                        Reply = OrderContainer.Resolve<IMsgHandler>(Order).Handler(e);
                        if(Reply != "")
                        {
                            _GroupID.SendGroupMessage(Reply);
                            return true;
                        }
                    }
                    //return false;
                    _GroupID.SendGroupMessage(CQApi.CQCode_Image("sm/EYHQ.jpg"));
                    return true;
                }
                    
            }
            ///复读机部分
            if (GroupState.GroupState[_GroupID].AllowRepeat)
            {
                if(GroupState.GroupState[_GroupID].LastRep != e.Message)
                {
                    if(GroupState.GroupState[_GroupID].FromQQ != e.FromQQ && GroupState.GroupState[_GroupID].Msg == e.Message)
                    {
                        _GroupID.SendGroupMessage(e.Message);
                        GroupState.GroupState[_GroupID].LastRep = e.Message;
                        return true;
                    }
                    
                }
                 GroupState.GroupState[_GroupID].FromQQ = e.FromQQ;
                 GroupState.GroupState[_GroupID].Msg = e.Message;
            }
            return false;
        }
        /// <summary>
        /// 每小时调用的定时任务
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimer(object source, ElapsedEventArgs e)//定时任务/每小时
        {
            foreach(CQGroupMessageEventArgs ee in dd)
            {
                if(MQTTHelper.ListenList.Count >= 1)//发送MQTT消息队列
                {
                    foreach(String _dd in MQTTHelper.ListenList)
                    {
                        ee.FromGroup.SendGroupMessage(_dd);
                    }
                }
            }
            MQTTHelper.ListenList.Clear();
        }
    }
}