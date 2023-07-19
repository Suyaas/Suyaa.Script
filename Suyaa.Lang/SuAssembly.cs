using Suyaa.Lang.Exceptions;
using Suyaa.Lang.Types;
using Suyaa.Msil;
using Suyaa.Msil.ExternAssemblies;
using Suyaa.Msil.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Lang
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
            foreach (var invoker in Invokers) invoker.Invoke();
        }
    }
}
