using Suyaa.Sulang.Exceptions;
using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// Su字段
    /// </summary>
    public sealed class SuField : NamedSuable, ITypable
    {

        /// <summary>
        /// 所属对象
        /// </summary>
        public IInstantiable Object { get; }

        /// <summary>
        /// Su结构体
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        public SuField(IInstantiable obj, string name) : base(name)
        {
            Object = obj;
        }

        /// <summary>
        /// 获取Il字段信息
        /// </summary>
        /// <returns></returns>
        public IlField? GetIlField()
        {
            return this.Object.GetField(this.Name);
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        public IlType GetIlType()
        {
            return GetIlField()?.Type ?? IlConsts.Null;
        }
    }
}
