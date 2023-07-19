using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil.Values
{
    /// <summary>
    /// Il值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IlValue<T> : Assemblable
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
        public IlValue(T value)
        {
            Value = value;
        }
    }
}
