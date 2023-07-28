using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil.Types
{
    /// <summary>
    /// 名称
    /// </summary>
    public class IlName : NamedAssemblable
    {
        /// <summary>
        /// 名称
        /// </summary>
        /// <param name="name"></param>
        public IlName(string name) : base(name)
        {
        }

        /// <summary>
        /// 获取Il汇编代码
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            return this.Name;
        }
    }
}
