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
    public sealed class SuVariable : NamedSuable, ITypable
    {
        /// <summary>
        /// Su类型
        /// </summary>
        /// <param name="name"></param>
        public SuVariable(string name) : base(name)
        {
        }

        /// <summary>
        /// 获取Il类型
        /// </summary>
        /// <returns></returns>
        public IlType GetIlType()
        {
            return IlVariable.Type;
        }
    }
}
