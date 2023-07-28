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
