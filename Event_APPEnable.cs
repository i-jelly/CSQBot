using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.EventArgs;

namespace cn.orua.qngel.Code
{
    public class Event_APPEnable : IAppEnable
    {
        public void AppEnable(object sender, CQAppEnableEventArgs e)
        {
            return;
            //throw new NotImplementedException();
        }
    }
}
