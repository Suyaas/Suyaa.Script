using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// IL外部类
    /// </summary>
    public class IlExternType : NamedAssemblable, IAssemblableType
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
        public IlExternType(IlExternAssembly assembly, string name) : base(name)
        {
            this.Assembly = assembly;
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        public IlType GetIlType() => new IlType($"[{this.Assembly.Name}]{this.Name}");

        /// <summary>
        /// 获取汇编代码
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            return $"[{this.Assembly.Name}]{this.Name}";
        }
    }
}
