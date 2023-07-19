using Microsoft.VisualStudio.TestPlatform.Utilities;
using Suyaa.Lang;
using Suyaa.Lang.Functions;
using Suyaa.Lang.Types;
using Suyaa.Lang.Values;
using Suyaa.Msil;
using Suyaa.Msil.Consts;
using Suyaa.Msil.ExternAssemblies;
using Suyaa.Msil.Types;
using Xunit.Abstractions;

namespace SuyaaTests.Lang
{
    public class SuTest
    {
        private readonly ITestOutputHelper _output;

        public SuTest(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void Output()
        {
            // 创建工作目录
            string path = sy.IO.GetFullPath("./su/");
            sy.IO.CreateFolder(path);
            using var sp = new SuProject("test", path);
            sp.Assembly
                .Call(new SuUse(sp.Global, "console", sp.MsCorlib.GetIlExternType("System.Console").GetIlType()))
                .Call(new SuCall(
                    sp.Assembly.CurrentMethod,
                    new IlMethodInvoker(sp.Global["console"].Type, "WriteLine") { IsStatic = true }.Paramter<IlString>()
                    ).Param(new SuStringValue("Hello"))
                );
            sp.Output();
        }
    }
}