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
            // ��������Ŀ¼
            string path = sy.IO.GetFullPath("./msil");
            sy.IO.CreateFolder(path);
            // ����il��Ŀ
            using var proj = new IlProject("test", path);
            // ����mscorlib����
            var msCorlib = proj.ExternAssembly<MsCorlib>();
            // ����������
            proj.Assembly(proj.Name);
            // ����Program��
            proj.Class("Program")
                .Keyword(IlKeys.Private, IlKeys.Auto, IlKeys.Ansi, IlKeys.Beforefieldinit)
                .Keyword(IlKeys.Abstract, IlKeys.Sealed)
                .Extends(msCorlib.Object)
                // ����Main����
                .Method("Main")
                    .Keyword(IlKeys.Private, IlKeys.Hidebysig, IlKeys.Static)
                    .Param<IlArray<IlString>>("args")
                    .Attach(IlKeys.Cil, IlKeys.Managed)
                    // ��Ӽ򵥵����ָ��
                    .Ldstr(new IlStringValue("This is a first dynamic msil program."))
                    .Call(new IlMethodInvoker(msCorlib.GetIlExternClass("System.Console").GetIlType(), "WriteLine") { IsStatic = true }.Param<IlString>())
                    .Ret();
            // ���il��Ŀ�ļ�
            proj.Output();
            // ��������
            _output.WriteLine("# debug���룬��Ҫ��װ.net����");
            _output.WriteLine(proj.Build("output/debug"));
            _output.WriteLine("# release���룬��Ҫ��װ.net����");
            _output.WriteLine(proj.Release("output/release/dotnet"));
            _output.WriteLine("# releaseָ��ƽ̨���룬����Ҫ��װ.net�������ļ��϶�");
            _output.WriteLine(proj.Release(Platforms.Win_x64, "output/release/win_x64"));
            _output.WriteLine("# ��׼����������Ҫ��װ.net���������ļ���ߴ磬�Ϻü�����");
            _output.WriteLine(proj.Publish(Platforms.Win_x64, false, "output/publish/win_x64"));
            _output.WriteLine("# Aot����������Ҫ��װ.net���������ļ�С�ߴ磬���ֹ�����ϵͳ������");
            _output.WriteLine(proj.Publish(Platforms.Win_x64, true, "output/aot/win_x64"));
        }
    }
}