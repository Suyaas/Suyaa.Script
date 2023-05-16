using Ssm.Jian.Engine;
using Suyaa.Script;
using Suyaa.Script.Apis;
using Suyaa.Script.Apis.Common;
using Suyaa.Script.Apis.Console;
using Suyaa.Script.Apis.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JianConsole
{
    /// <summary>
    /// 主程序
    /// </summary>
    public static class ShellHost
    {
        /// <summary>
        /// 运行
        /// </summary>
        public static void Run()
        {
            Console.Title = sy.Assembly.Name;
            SConsole.WriteLine($"[{sy.Assembly.Name}] Version {sy.Assembly.Version}", ConsoleColor.Yellow);
            SConsole.WriteLine();

            // 注册软件路径
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SConsole.WriteLine("[Windows模式]", ConsoleColor.Green);
                SConsole.WriteLine();
                SConsole.Log("初始化", "检测权限 ...");
                // Windows
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                var isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
                if (isElevated)
                {
                    SConsole.Log("初始化", "权限检测通过");
                    sy.Windows.RegisterFileAssociations(".jc", "Jian.Script", "ShengShengMan Jian Chinese Language Programming Script", $"\"{sy.Assembly.ExecutionFilePath}\" \"%1\"", $"{sy.IO.GetExecutionPath("script.ico")}");
                    SConsole.Log("结果", "注册成功");
                }
                else
                {
                    SConsole.WriteLine($"使用管理员身份运行可进行Windows文件关联注册！", ConsoleColor.Red);
                }
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                SConsole.WriteLine("[Windows模式]", ConsoleColor.Green);
                SConsole.WriteLine();
                SConsole.WriteLine($"使用命令行执行'{sy.Assembly.ExecutionFilePath}install.sh'可进行进行安装。");
                //System.Console.WriteLine("[Linux安装说明]");
                //System.Console.WriteLine();
                //System.Console.WriteLine($"使用命令行执行'{it.ExecPath}install.sh'可进行进行安装。");
                //System.Console.WriteLine();
                //// 输出帮助
                //Help();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                //// 输出帮助
                //Help();
            }
            else
            {
                SConsole.WriteLine($"不支持的操作系统'{RuntimeInformation.OSDescription}'", ConsoleColor.Red);
                Console.ReadKey();
                Environment.Exit(0);
            }

            SConsole.WriteLine();
            SConsole.WriteLine("[使用手册]", ConsoleColor.Green);
            SConsole.WriteLine();

            Console.ReadKey();
        }

        public static void Help()
        {

        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="path"></param>
        public static void Execute(string path)
        {
            Console.Title = $"{sy.Assembly.Name} Ver:{sy.Assembly.Version} - {path}";
            string code = sy.IO.ReadUtf8FileContent(path);
            ScriptParser.ScriptCalculateNames.Add("计算");
            using (var script = Suyaa.Script.ScriptParser.Parse(code))
            using (Ssm.Jian.Engine.ScriptFunctions funcs = new())
            {
                // 注册数值函数
                funcs.Reg<NumericRegistr, JianFuncAttribute>();
                // 注册控制台函数
                funcs.Reg<ConsoleRegistr, JianFuncAttribute>();
                // 注册文件函数
                funcs.Reg<FileRegistr, JianFuncAttribute>();
                // 注册文件夹函数
                funcs.Reg<FolderRegistr, JianFuncAttribute>();
                // 注册目录函数
                funcs.Reg<PathRegistr, JianFuncAttribute>();
                using (var engine = new Suyaa.Script.ScriptEngine(script, funcs))
                {
                    Console.WriteLine(engine.Execute());
                }
            }
        }
    }
}
