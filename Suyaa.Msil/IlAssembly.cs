using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// 程序集
    /// </summary>
    public class IlAssembly : NamedAssemblable
    {

        /// <summary>
        /// 程序集
        /// </summary>
        /// <param name="name"></param>
        public IlAssembly(string name) : base(name) { }

        /// <summary>
        /// 输出汇编语言
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            return $".assembly '{Name}' {{}}";
        }
    }
}
