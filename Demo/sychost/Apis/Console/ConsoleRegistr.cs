using Suyaa.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sychost.Apis.Console
{
    /// <summary>
    /// 控制台函数注册
    /// </summary>
    public class ConsoleRegistr : ScriptFunctionRegistrBase
    {
        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="content"></param>
        [Func]
        public void Print(string content)
            => SConsole.Write(content);

        /// <summary>
        /// 换行输出
        /// </summary>
        /// <param name="content"></param>
        [Func]
        public void Println(string content)
            => SConsole.WriteLine(content);
    }
}
