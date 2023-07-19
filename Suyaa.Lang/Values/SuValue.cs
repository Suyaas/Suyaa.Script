using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Lang.Values
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
    public abstract class SuValue<T> : SuValue
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
    }
}
