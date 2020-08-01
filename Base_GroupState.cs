using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Data;

public class RepMsg
{
    public Int64 FromQQ;
    public String Msg;
    public String LastRep;
    public bool AllowReply;
    public bool AllowRepeat;
    public bool AllowR18;
    public String[] Admin;
}


namespace cn.orua.qngel.Code
{
    public class Base_GroupState
    {
        public Dictionary<long, RepMsg> GroupState = new Dictionary<long, RepMsg>();

        private Base_Config _Config = new Base_Config();

        public bool AllowReply(long GroupID)
        {
            if (GroupState.ContainsKey(GroupID))
            {
                if (!GroupState[GroupID].AllowReply)
                {
                    GroupState[GroupID].AllowReply = true;
                    _Config.SetAllowReply(GroupID,true);
                    return true;
                }
            }
            return false;
        }

        public bool DisAllowReply(long GroupID)
        {
            if (GroupState.ContainsKey(GroupID))
            {
                if (GroupState[GroupID].AllowReply)
                {
                    GroupState[GroupID].AllowReply = false;
                    _Config.SetAllowReply(GroupID, false);
                    return true;
                }
            }
            return false;
        }

        public bool AllowRepeat(long GroupID)
        {
            if (GroupState.ContainsKey(GroupID))
            {
                if (!GroupState[GroupID].AllowRepeat)
                {
                    GroupState[GroupID].AllowRepeat = true;
                    _Config.SetAllowRepeat(GroupID, true);
                    return true;
                }
            }
            return false;
        }

        public bool DisAllowRepeat(long GroupID)
        {
            if (GroupState.ContainsKey(GroupID))
            {
                if (GroupState[GroupID].AllowRepeat)
                {
                    GroupState[GroupID].AllowRepeat = false;
                    _Config.SetAllowRepeat(GroupID, false);
                    return true;
                }
            }
            return false;
        }

        public bool AllowR18(long GroupID)
        {
            if (GroupState.ContainsKey(GroupID))
            {
                if (!GroupState[GroupID].AllowR18)
                {
                    GroupState[GroupID].AllowR18 = true;
                    _Config.SetAllowR18(GroupID, true);
                    return true;
                }
            }
            return false;
        }

        public bool DisAllowR18(long GroupID)
        {
            if (GroupState.ContainsKey(GroupID))
            {
                if (GroupState[GroupID].AllowR18)
                {
                    GroupState[GroupID].AllowR18 = false;
                    _Config.SetAllowR18(GroupID, false);
                    return true;
                }
            }
            return false;
        }

        public void AddNewGroupToList(long GroupID)
        {
            RepMsg _new = new RepMsg();
            _new.AllowRepeat = true;
            _new.AllowReply = true;
            _new.FromQQ = 0;
            _new.LastRep = "";
            _new.Msg = "";
            GroupState.Add(GroupID, _new);
        }
    }
}
