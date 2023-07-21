using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// 带名称的Su可处理对象
    /// </summary>
    public abstract class NamedSuable : Suable
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 可汇编对象
        /// </summary>
        /// <param name="name"></param>
        public NamedSuable(string name)
        {
            Name = name;
        }
    }
}
