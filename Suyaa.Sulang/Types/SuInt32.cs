using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Sulang.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// Int32
    /// </summary>
    public sealed class SuInt32 : SuStructType, ITypable
    {
        /// <summary>
        /// Int32
        /// </summary>
        public SuInt32() : base(new SuType(IlConsts.Int32))
        {
            // Set函数
            this.Methods.Add(new Set(this));
            // Add函数
            this.Methods.Add(new Add(this));
        }
    }
}
