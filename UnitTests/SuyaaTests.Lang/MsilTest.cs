using Microsoft.VisualStudio.TestPlatform.Utilities;
using Suyaa.Msil;
using Suyaa.Msil.ExternAssemblies;
using Suyaa.Msil.Types;
using Suyaa.Msil.Values;
using Xunit.Abstractions;

namespace SuyaaTests.Lang
{
    public class MsilTest
    {
        private readonly ITestOutputHelper _output;

        public MsilTest(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void Output()
        {
            // 创建工作目录
            string path = sy.IO.GetFullPath("./msil");
            sy.IO.CreateFolder(path);
            // 建立il项目
            using var proj = new IlProject("test", path);
            // 引入mscorlib程序集
            var msCorlib = proj.ExternAssembly<MsCorlib>();
            // 创建主程序集
            proj.Assembly(proj.Name);
            // 创建Program类
            proj.Class("Program")
                .Keyword(IlKeys.Private, IlKeys.Auto, IlKeys.Ansi, IlKeys.Beforefieldinit)
                .Keyword(IlKeys.Abstract, IlKeys.Sealed)
                .Extends(msCorlib.Object)
                // 创建Main函数
                .Method("Main")
                    .Keyword(IlKeys.Private, IlKeys.Hidebysig, IlKeys.Static)
                    .Param<IlArray<IlString>>("args")
                    .Attach(IlKeys.Cil, IlKeys.Managed)
                    // 添加简单的输出指令
                    .Ldstr(new IlStringValue("This is a first dynamic msil program."))
                    .Call(new IlMethodInvoker(msCorlib.GetIlExternClass("System.Console").GetIlType(), "WriteLine") { IsStatic = true }.Param<IlString>())
                    .Ret();
            // 输出il项目文件
            proj.Output();
            // 构建程序
            _output.WriteLine("# debug编译，需要安装.net环境");
            _output.WriteLine(proj.Build("output/debug"));
            _output.WriteLine("# release编译，需要安装.net环境");
            _output.WriteLine(proj.Release("output/release/dotnet"));
            _output.WriteLine("# release指定平台编译，不需要安装.net环境，文件较多");
            _output.WriteLine(proj.Release(Platforms.Win_x64, "output/release/win_x64"));
            _output.WriteLine("# 标准发布，不需要安装.net环境，单文件大尺寸，较好兼容性");
            _output.WriteLine(proj.Publish(Platforms.Win_x64, false, "output/publish/win_x64"));
            _output.WriteLine("# Aot发布，不需要安装.net环境，单文件小尺寸，部分功能与系统不兼容");
            _output.WriteLine(proj.Publish(Platforms.Win_x64, true, "output/aot/win_x64"));
        }
    }
}