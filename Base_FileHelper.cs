using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cn.orua.qngel.Code
{
    class Base_FileHelper
    {
        /// <summary>
        /// 获取e路径下随机一个文件的完整可调用路径
        /// </summary>
        public String RandomGetImg(String e)
        {
            if (!Directory.Exists(e)) return "";
            FileInfo[] files = new DirectoryInfo(e).GetFiles();
            return files[new Random().Next(files.Length)].Name;
        }
    }
}
