using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// 可汇编对象
    /// </summary>
    public abstract class NamedAssemblable : Assemblable
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 可汇编对象
        /// </summary>
        /// <param name="name"></param>
        public NamedAssemblable(string name)
        {
            Name = name;
        }
    }
}
