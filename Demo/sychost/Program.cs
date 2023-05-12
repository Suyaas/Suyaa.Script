// See https://aka.ms/new-console-template for more information
using sychost;
using System.Text;

if (args.Length > 0)
{
    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    // 执行脚本
    ShellHost.Execute(args[0]);
}
else
{
    // 标准运行
    ShellHost.Run();
}