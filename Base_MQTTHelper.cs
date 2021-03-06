﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace cn.orua.qngel.Code
{
    class Base_MQTTHelper
    {
        public MqttClient client;
        public List<String> ListenList = new List<String>();
        public bool IsInit = false;

        /// <summary>
        /// 获取JSON信息的结构
        /// </summary>
        public class LiveInfo
        {
            public String name { get; set; }
            public String title { get; set; }
            public String url { get; set; }
        }

        /// <summary>
        /// 连接MQTT服务器并订阅live/dd频道
        /// </summary>
        public void Init()
        {
            client = new MqttClient("jp.kizuna.vip");
            if(!client.IsConnected) client.Connect(Guid.NewGuid().ToString());
            client.Subscribe(new String[] { "live/dd", }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.MqttMsgPublishReceived += OnReciveMessage;
        }

        /// <summary>
        /// 收到MQTT的JSON信息的回调函数,将消息解析后加入MQTT消息队列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReciveMessage(Object sender, MqttMsgPublishEventArgs e)
        {
            String json = Encoding.ASCII.GetString(e.Message);
           
            LiveInfo info = JsonConvert.DeserializeObject<LiveInfo>(json);
            String In = info.name + "开播辣\n" + info.title + "\n" + "地址:" + info.url;
            if(!ListenList.Contains(In)) ListenList.Add(In);
        }
    }
}
