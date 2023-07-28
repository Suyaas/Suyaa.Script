using Suyaa.Exceptions;
using Suyaa.Msil.Exceptions;
using Suyaa.Sulang.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil.Values
{
    /// <summary>
    /// Il值
    /// </summary>
    public abstract class IlValue : Assemblable
    {
        /// <summary>
        /// 类型
        /// </summary>
        public IlType Type { get; }

        /// <summary>
        /// Il值
        /// </summary>
        /// <param name="name"></param>
        protected IlValue(IlType type)
        {
            Type = type;
        }

        /// <summary>
        /// Il值
        /// </summary>
        /// <param name="name"></param>
        protected IlValue(Type type)
        {
            Type = type.GetTypeCode() switch
            {
                TypeCode.String => IlConsts.String,
                TypeCode.Int32 => IlConsts.Int32,
                _ => new IlType(type.FullName),
            };
        }
    }

    /// <summary>
    /// Il值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IlValue<T> : IlValue
        where T : notnull
    {
        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Il值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public IlValue(IlType type, T value) : base(type)
        {
            Value = value;
        }

        /// <summary>
        /// Il值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public IlValue(T value) : base(typeof(T))
        {
            Value = value;
        }

        /// <summary>
        /// 转化为汇编代码
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            var type = typeof(T);
            return type.GetTypeCode() switch
            {
                TypeCode.String => $"\"{this.Value}\"",
                TypeCode.Int32 => Convert.ToString(this.Value),
                _ => throw new TypeNotSupportedException(type),
            };
        }
    }
}
