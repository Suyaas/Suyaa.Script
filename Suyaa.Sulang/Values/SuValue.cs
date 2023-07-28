using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Msil.Values;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Values
{
    /// <summary>
    /// Su值
    /// </summary>
    public class SuValue<T> : SuStructType, ITypable, ICodable
        where T : notnull
    {
        // 类型
        private readonly IlValue<T> _ilValue;

        /// <summary>
        /// 值
        /// </summary>
        public T Value => _ilValue.Value;

        /// <summary>
        /// Su值
        /// </summary>
        /// <param name="value"></param>
        public SuValue(T value) : base("value")
        {
            _ilValue = new IlValue<T>(value);
        }

        /// <summary>
        /// Su值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public SuValue(T value, ITypable type) : base(type)
        {
            _ilValue = new IlValue<T>(value);
        }

        /// <summary>
        /// Su值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public SuValue(T value, IlType type) : base(type)
        {
            _ilValue = new IlValue<T>(value);
        }

        /// <summary>
        /// 获取IlType
        /// </summary>
        /// <returns></returns>
        public override IlType GetIlType()
        {
            return _ilValue.Type;
        }

        /// <summary>
        /// 获取代码字符串
        /// </summary>
        /// <returns></returns>
        public virtual string ToCodeString()
        {
            if (this.Value is string str)
            {
                return $"\"{this.Value}\"";
            }
            return $"{this.Value}";
        }
    }
}
