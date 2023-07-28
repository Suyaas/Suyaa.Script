using Suyaa.Msil;
using Suyaa.Msil.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// Su类型
    /// </summary>
    public sealed class SuVariable : SuType, ICodable
    {
        /// <summary>
        /// 变量名
        /// </summary>
        public string VarName { get; }

        /// <summary>
        /// Su类型
        /// </summary>
        /// <param name="varName"></param>
        public SuVariable(string varName) : base(IlConsts.Variable)
        {
            VarName = varName;
        }

        /// <summary>
        /// 获取代码
        /// </summary>
        /// <returns></returns>
        public string ToCodeString()
        {
            return this.VarName;
        }
    }
}
