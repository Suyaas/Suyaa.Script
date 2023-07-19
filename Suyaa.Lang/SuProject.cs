﻿using Suyaa.Lang.Types;
using Suyaa.Msil;
using Suyaa.Msil.ExternAssemblies;
using Suyaa.Msil.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Lang
{
    /// <summary>
    /// Su项目
    /// </summary>
    public class SuProject : Disposable
    {
        /// <summary>
        /// 全局变量
        /// </summary>
        public SuGlobal Global { get; }

        /// <summary>
        /// mscorlib
        /// </summary>
        public MsCorlib MsCorlib { get; }

        /// <summary>
        /// mscorlib
        /// </summary>
        public IlProject IlProject { get; }

        /// <summary>
        /// mscorlib
        /// </summary>
        public SuAssembly Assembly { get; }

        /// <summary>
        /// 创建并返回一个类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IlClass Class(string name)
        {
            var cls = this.IlProject.Class(name);
            return cls;
        }

        /// <summary>
        /// Su项目
        /// </summary>
        public SuProject(string name, string folder)
        {
            this.Global = new SuGlobal();
            this.MsCorlib = new MsCorlib();
            var proj = this.IlProject = new IlProject(name, folder);
            this.Assembly = new SuAssembly(this);
            // 添加外部程序集
            proj.ExternAssembly(this.MsCorlib);
            // 创建主程序集
            proj.Assembly(proj.Name);
        }

        /// <summary>
        /// 输出
        /// </summary>
        public void Output()
        {
            // 执行程序集
            this.Assembly.Invoke();
            // 程序输出
            this.IlProject.Output();
        }

        // 构建
        public void Build()
        {

        }

        #region 释放资源

        /// <summary>
        /// 托管资源释放
        /// </summary>
        protected override void OnManagedDispose()
        {
            this.Assembly.Dispose();
            this.IlProject.Dispose();
            this.MsCorlib.Dispose();
            this.Global.Dispose();
            base.OnManagedDispose();
        }

        #endregion
    }
}
