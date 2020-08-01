using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Native.Sdk.Cqp.EventArgs;
using cn.orua.qngel.Code;

namespace cn.orua.qngel.Code.Interface
{
    /// <summary>
    /// 任意信息处理函数的接口
    /// <para>
    /// </para>
    /// </summary>
    public interface IMsgHandler
    {
        /// <summary>
        /// 通用信息处理接口
        /// </summary>
        /// <param name="e"></param>
        /// <param name="b"></param>
        String Handler(CQGroupMessageEventArgs e, Base_SQLHelper.SQLHelperData b);
    }
}
