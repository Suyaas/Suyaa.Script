using Suyaa.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace sychost.Apis.Console
{
    /// <summary>
    /// 控制台函数注册
    /// </summary>
    public class ConsoleRegistr : ScriptRegistrBase
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

        /// <summary>
        /// 运行程序
        /// </summary>
        /// <param name="file"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [Func]
        public string Exec(string file, string args)
        {
            return sy.Terminal.Execute(file, args);
        }

        /// <summary>
        /// 运行程序
        /// </summary>
        /// <param name="file"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [Func]
        public string ExecEncoding(string file, string args, string encoding)
        {
            return sy.Terminal.Execute(file, args, Encoding.GetEncoding(encoding));
        }
    }
}
