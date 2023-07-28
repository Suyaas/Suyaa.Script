using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil.Types
{
    /// <summary>
    /// IL外部类
    /// </summary>
    public class IlExternClass : IlType
    {
        /// <summary>
        /// 外部程序集
        /// </summary>
        public IlExternAssembly Assembly { get; }

        /// <summary>
        /// IL外部类
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="name"></param>
        public IlExternClass(IlExternAssembly assembly, string name) : base(name)
        {
            Assembly = assembly;
        }

        /// <summary>
        /// 获取汇编代码
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            return $"[{Assembly.Name}]{Name}";
        }
    }
}
