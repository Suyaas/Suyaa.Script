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
            this.Global = SuConsts.Global;
            this.MsCorlib = new MsCorlib();
            var proj = this.IlProject = new IlProject(name, folder);
            this.Assembly = new SuAssembly(this);
            // 添加外部程序集
            proj.ExternAssembly(this.MsCorlib);
            // 创建主程序集
            proj.Assembly(proj.Name);
        }

        /// <summary>
        /// 解析脚本
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public SuProject Parse(string script)
        {
            using var parser = new SuParser(this);
            parser.Parse(script);
            return this;
        }

        /// <summary>
        /// 输出
        /// </summary>
        public SuProject Output()
        {
            // 执行程序集
            this.Assembly.Invoke();
            // 程序输出
            this.IlProject.Output();
            return this;
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="path"></param>
        public string Build(Platforms platform, string? path = null)
        {
            // 程序输出
            return this.IlProject.Publish(platform, true, path);
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
