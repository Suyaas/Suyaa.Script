using sychost.Apis.Console;
using sychost.Apis.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace sychost
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
                    sy.Windows.RegisterFileAssociations(".syc", "Suyaa.Script", "Suyaa Script Shell File", $"\"{sy.Assembly.ExecutionFilePath}\" \"%1\"", sy.IO.GetExecutionPath("suyaa.ico"));
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
            string code = sy.IO.ReadUtf8FileContent(path);
            using (var script = Suyaa.Script.ScriptParser.Parse(code))
            using (var funcs = new Suyaa.Script.ScriptFunctions())
            {
                funcs.Reg<ConsoleRegistr>();
                funcs.Reg<FileRegistr>();
                funcs.Reg<FolderRegistr>();
                funcs.Reg<PathRegistr>();
                using (var engine = new Suyaa.Script.ScriptEngine(script, funcs))
                {
                    Console.WriteLine(engine.Execute());
                }
            }
        }
    }
}
