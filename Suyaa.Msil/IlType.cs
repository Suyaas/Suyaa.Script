using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// IL外部类
    /// </summary>
    public class IlType : NamedAssemblable, IAssemblableType
    {
        private static IlType? _type;

        /// <summary>
        /// 静态类型
        /// </summary>
        public static IlType Type { get => _type ??= new IlType(nameof(IlType)); }

        /// <summary>
        /// IL外部类
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="name"></param>
        public IlType(string name) : base(name)
        {

        }

        /// <summary>
        /// 获取汇编代码
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            return this.Name;
        }
    }
}
