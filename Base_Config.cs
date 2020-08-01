using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;

using Native.Tool.IniConfig;
using Native.Tool.IniConfig.Linq;

namespace cn.orua.qngel.Code
{
    public class Base_Config
    {
        private XmlDocument config = new XmlDocument();
        public class GroupConfig
        {
            public bool IsListen;
            public bool AllowReply;
            public bool AllowRepeat;
        }

        public bool HasConfig(long GroupID)
        {
            if (!System.IO.File.Exists("conf/Config.xml"))
            {
                return false;
            }
            
            try
            {
                config.Load("conf/Config.xml");
                String _ = config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("AllowRepeat").InnerText;
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public void NewGroupConfig(long GroupID)
        {
            config.Load("conf/Config.xml");
            XmlNode xmlNode = config.SelectSingleNode("Groups");
            XmlElement Group = config.CreateElement("G" + GroupID.ToString());

            XmlElement IsListen = config.CreateElement("IsListen");
            XmlElement AllowReply = config.CreateElement("AllowReply");
            XmlElement AllowRepeat = config.CreateElement("AllowRepeat");
            XmlElement AllowR18 = config.CreateElement("AllowR18");
            XmlElement dd = config.CreateElement("dd");
            XmlElement Admin = config.CreateElement("Admin");
            IsListen.InnerText = "True";
            AllowRepeat.InnerText = "True";
            AllowReply.InnerText = "True";
            AllowR18.InnerText = "False";
            dd.InnerText = "True";
            Admin.InnerText = "0,";
            Group.AppendChild(IsListen);
            Group.AppendChild(AllowReply);
            Group.AppendChild(AllowRepeat);
            Group.AppendChild(AllowR18);
            Group.AppendChild(dd);
            Group.AppendChild(Admin);
            xmlNode.AppendChild(Group);
            config.Save("conf/Config.xml");
        }

        public bool IsListen(long GroupID)
        {
            config.Load("conf/Config.xml");
            return config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("IsListen").InnerText == "True";
        }

        public void SetIsListen(long GroupID, bool T)
        {
            config.Load("conf/Config.xml");
            config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("IsListen").InnerText = T.ToString();
            config.Save("conf/Config.xml");
        }

        public bool AllowReply(long GroupID)
        {
            config.Load("conf/Config.xml");
            return config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("AllowReply").InnerText == "True";
        }

        public void SetAllowReply(long GroupID, bool T)
        {
            config.Load("conf/Config.xml");
            config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("AllowReply").InnerText = T.ToString();
            config.Save("conf/Config.xml");
        }

        public bool AllowRepeat(long GroupID)
        {
            config.Load("conf/Config.xml");
            return config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("AllowRepeat").InnerText == "True";
        }

        public void SetAllowRepeat(long GroupID, bool T)
        {
            config.Load("conf/Config.xml");
            config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("AllowRepeat").InnerText = T.ToString();
            config.Save("conf/Config.xml");
        }

        public bool AllowR18(long GroupID)
        {
            config.Load("conf/Config.xml");
            return config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("AllowR18").InnerText == "True";
        }

        public void SetAllowR18(long GroupID, bool T)
        {
            config.Load("conf/Config.xml");
            config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("AllowR18").InnerText = T.ToString();
            config.Save("conf/Config.xml");
        }
        public bool Allowdd(long GroupID)
        {
            config.Load("conf/Config.xml");
            return config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("dd").InnerText == "True";
        }
        public bool Setdd(long GroupID)
        {
            config.Load("conf/Config.xml");
            return config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("dd").InnerText == "True";
        }

        public String[] GetAllAdmin(long GroupID)
        {
            config.Load("conf/Config.xml");
            return config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("Admin").InnerText.Split(",".ToCharArray());
        }

        public void AddAdmin(long GroupID,long AccountID)
        {
            config.Load("conf/Config.xml");
            config.SelectSingleNode("Groups").SelectSingleNode("G" + GroupID.ToString()).SelectSingleNode("Admin").InnerText += AccountID.ToString() + ",";
            config.Save("conf/Config.xml");
        }

    }
}
