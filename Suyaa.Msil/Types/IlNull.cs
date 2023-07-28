using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil.Types
{
    /// <summary>
    /// 空类型
    /// </summary>
    public class IlNull : IlType
    {
        /// <summary>
        /// IlInt32
        /// </summary>
        public IlNull() : base("null") { }
    }
}
