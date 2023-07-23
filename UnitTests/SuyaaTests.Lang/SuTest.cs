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
                .Call(sp.Global.GetMethod("Use")
                    .CreateInvoker()
                    .Param(Suable.Variable("console"))
                    .Param(Suable.Type(sp.MsCorlib.GetIlExternClass("System.Console").GetIlType()))
                )
                .Call(Suable.Method(Suable.Struct(sp.Global, "console"), "WriteLine")
                    .Declare<IlString>()
                    .CreateInvoker()
                    .Param(Suable.Value("Helle Suyaa!"))
                );
            sp.Output();
            _output.WriteLine(sp.Build(Platforms.Win_x64));
        }

        [Fact]
        public void Script()
        {
            // 创建工作目录
            string path = sy.IO.GetFullPath("./sulang/");
            sy.IO.CreateFolder(path);
            string filePath = sy.IO.CombinePath(path, "test.su");
            string script = sy.IO.ReadUtf8FileContent(filePath);
            using var sp = new SuProject("test", path);
            //sp.Parse(script).Output();
            _output.WriteLine(sp.Parse(script).Output().Build(Platforms.Win_x64));
        }
    }
}