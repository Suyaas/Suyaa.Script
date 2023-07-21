using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Sulang.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Values
{
    /// <summary>
    /// Su值
    /// </summary>
    public abstract class SuValue : Suable
    {

    }

    /// <summary>
    /// Su值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SuValue<T> : SuValue, ITypable
        where T : notnull
    {
        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Il值
        /// </summary>
        /// <param name="value"></param>
        public SuValue(T value)
        {
            Value = value;
        }

        /// <summary>
        /// 获取Il类型
        /// </summary>
        /// <returns></returns>
        public IlType GetIlType()
        {
            switch (this.Value)
            {
                case string _: return sy.Assembly.Create<IlString>();
                default: throw new SuException($"Unsupported type '{typeof(T).FullName}'");
            }
        }
    }
}
