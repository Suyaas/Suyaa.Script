using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil.Types
{
    /// <summary>
    /// 变量
    /// </summary>
    public sealed class IlVariable : IlType
    {
        // 静态类型
        private static IlType? _type;

        /// <summary>
        /// 静态类型
        /// </summary>
        public new static IlType Type { get => _type ??= new IlType(nameof(IlVariable)); }

        /// <summary>
        /// 变量名称
        /// </summary>
        public string VarName { get; }

        /// <summary>
        /// 变量
        /// </summary>
        public IlVariable(string varName) : base(nameof(IlVariable))
        {
            this.VarName = varName;
        }
    }
}
