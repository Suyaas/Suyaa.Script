// See https://aka.ms/new-console-template for more information
using sychost;

if (args.Length > 0)
{
    // 执行脚本
    ShellHost.Execute(args[0]);
}
else
{
    // 标准运行
    ShellHost.Run();
}