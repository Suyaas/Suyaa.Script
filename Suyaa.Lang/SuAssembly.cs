using Suyaa.Lang.Exceptions;
using Suyaa.Msil;
using Suyaa.Msil.ExternAssemblies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Lang
{
    /// <summary>
    /// Su程序集
    /// </summary>
    public sealed class SuAssembly : Disposable
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public List<IMsilable> Codes { get; set; }

        /// <summary>
        /// 外部程序集
        /// </summary>
        public List<IlExternAssembly> ExternAssemblies { get; set; }

        /// <summary>
        /// Su程序集
        /// </summary>
        public SuAssembly()
        {
            this.Codes = new List<IMsilable>();
            this.ExternAssemblies = new List<IlExternAssembly>();
        }

        /// <summary>
        /// 创建Msil项目
        /// </summary>
        /// <param name="name"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public IlProject CreateIlProject(string name, string folder)
        {
            IlProject proj = new IlProject(name, folder);
            // 添加外部程序集
            foreach (var assembly in this.ExternAssemblies)
            {
                proj.Assemblables.Add(assembly);
            }
            return proj;
        }
    }
}
