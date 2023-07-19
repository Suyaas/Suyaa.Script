using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// Il字段
    /// </summary>
    public sealed class IlField : NamedAssemblable
    {
        /// <summary>
        /// 字段类型
        /// </summary>
        public IlType Type { get; }

        public IlField(string name, IlType type) : base(name)
        {
            Type = type;
        }

        /// <summary>
        /// 获取汇编代码
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            return $"{this.Type.ToAssembly()} {this.Name}";
        }
    }
}
