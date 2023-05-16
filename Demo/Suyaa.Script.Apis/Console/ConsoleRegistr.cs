using Ssm.Jian.Engine;
using Suyaa.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Script.Apis.Console
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
        [JianFunc("输出")]
        public void Print(string content)
            => SConsole.Write(content);

        /// <summary>
        /// 换行输出
        /// </summary>
        /// <param name="content"></param>
        [Func]
        [JianFunc("换行输出")]
        public void Println(string content)
            => SConsole.WriteLine(content);

        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="content"></param>
        [Func]
        [JianFunc("读取字符输入")]
        public int ReadKey()
            => System.Console.ReadKey().KeyChar;

        /// <summary>
        /// 运行程序
        /// </summary>
        /// <param name="file"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        [Func]
        [JianFunc("执行")]
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
        [JianFunc("带编码执行")]
        public string ExecEncoding(string file, string args, string encoding)
        {
            return sy.Terminal.Execute(file, args, Encoding.GetEncoding(encoding));
        }
    }
}
