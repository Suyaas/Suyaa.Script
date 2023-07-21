using Suyaa.Sulang.Exceptions;
using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suyaa.Sulang.Functions;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// 全局对象$
    /// </summary>
    public sealed class SuGlobal : SuInstance, IInstantiable
    {
        /// <summary>
        /// 全局对象$
        /// </summary>
        public SuGlobal() : base(null, "$")
        {
            this.Methods.Add(new SuUse(this));
        }
    }
}
