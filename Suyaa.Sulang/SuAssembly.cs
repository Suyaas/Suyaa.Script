using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using Suyaa.Msil;
using Suyaa.Msil.ExternAssemblies;
using Suyaa.Msil.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// Su程序集
    /// </summary>
    public sealed class SuAssembly : Disposable, IInvokable
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public List<IInvokable> Invokers { get; set; }

        /// <summary>
        /// Su项目
        /// </summary>
        public SuProject Project { get; }

        /// <summary>
        /// Su程序集
        /// </summary>
        public SuAssembly(SuProject project)
        {
            this.Invokers = new List<IInvokable>();
            Project = project;
        }

        // Program 类
        private IlClass? _program;

        /// <summary>
        /// Program 类
        /// </summary>
        public IlClass Program => _program ??= this.Project.IlProject.Class("Program")
            .Keyword(IlKeys.Private, IlKeys.Auto, IlKeys.Ansi, IlKeys.Beforefieldinit)
            .Keyword(IlKeys.Abstract, IlKeys.Sealed)
            .Extends(this.Project.MsCorlib.Object);

        // Program 类
        private IlMethod? _main;

        /// <summary>
        /// Main 方法
        /// </summary>
        public IlMethod Main => _main ??= this.Program.Method("Main")
            .Keyword(IlKeys.Private, IlKeys.Hidebysig, IlKeys.Static)
            .Param<IlArray<IlString>>("args")
            .Attach(IlKeys.Cil, IlKeys.Managed);

        /// <summary>
        /// 当前类
        /// </summary>
        public IlClass CurrentClass => this.Program;

        /// <summary>
        /// 当前类
        /// </summary>
        public IlMethod CurrentMethod => this.Main;

        /// <summary>
        /// 添加执行语句
        /// </summary>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public SuAssembly Call(IInvokable invoker)
        {
            this.Invokers.Add(invoker);
            return this;
        }

        /// <summary>
        /// 执行
        /// </summary>
        public void Invoke()
        {
#if DEBUG
            // 输出临时的sup文件
            string tempPath = sy.IO.GetFullPath("./parser");
            sy.IO.CreateFolder(tempPath);
            sy.IO.WriteUtf8FileContent(sy.IO.CombinePath(tempPath, "temp.sui"), ToCodeString());
#endif
            // 依次执行解析方法
            foreach (var invoker in Invokers) invoker.Invoke();
            // 添加结束指令
            this.CurrentMethod.Ret();
        }

        /// <summary>
        /// 获取执行返回结果类型
        /// </summary>
        /// <returns></returns>
        public ITypable GetInvokeReutrnType()
        {
            return SuConsts.Void;
        }

        /// <summary>
        /// 获取代码字符串
        /// </summary>
        /// <returns></returns>
        public string ToCodeString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var invoker in this.Invokers)
            {
                sb.Append(invoker.ToCodeString());
            }
            return sb.ToString();
        }
    }
}
