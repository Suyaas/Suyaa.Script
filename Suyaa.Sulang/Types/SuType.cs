using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// Su类型
    /// </summary>
    public class SuType : NamedSuable, ITypable
    {
        // il类型
        private readonly IlType _type;

        /// <summary>
        /// Su类型
        /// </summary>
        /// <param name="name"></param>
        public SuType(string name) : base(name)
        {
            _type = new IlType(name);
        }

        /// <summary>
        /// Su类型
        /// </summary>
        /// <param name="type"></param>
        public SuType(IlType type) : base(type.Name)
        {
            _type = type;
        }

        /// <summary>
        /// 获取Il类型
        /// </summary>
        /// <returns></returns>
        public virtual IlType GetIlType()
        {
            return _type;
        }
    }
}
