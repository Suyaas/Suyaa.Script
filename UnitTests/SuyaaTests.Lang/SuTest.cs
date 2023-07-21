using Microsoft.VisualStudio.TestPlatform.Utilities;
using Suyaa.Sulang;
using Suyaa.Sulang.Functions;
using Suyaa.Sulang.Types;
using Suyaa.Sulang.Values;
using Suyaa.Msil;
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
            string path = sy.IO.GetFullPath("./sulang/");
            sy.IO.CreateFolder(path);
            using var sp = new SuProject("test", path);
            sp.Assembly
                .Call(new SuUse(sp.Global, "console", sp.MsCorlib.GetIlExternClass("System.Console").GetIlType()))
                .Call(new SuMethod(new SuStruct(sp.Global, "console"), "WriteLine").Param(new SuStringValue("Hello Ms. Su!")));
            sp.Output();
            _output.WriteLine(sp.Build(Platforms.Win_x64));
        }
    }
}