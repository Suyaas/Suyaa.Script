using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Sulang.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// 字符串
    /// </summary>
    public sealed class SuString : SuStructType
    {
        // 类型
        private static SuType _type = new SuType(IlConsts.String);

        /// <summary>
        /// 字符串
        /// </summary>
        public SuString() : base(_type)
        {
            // Set函数
            this.Methods.Add(new Set(this));
        }
    }
}
