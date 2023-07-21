using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// 程序集
    /// </summary>
    public class IlExternAssembly : NamedAssemblable
    {

        /// <summary>
        /// 程序集
        /// </summary>
        /// <param name="name"></param>
        public IlExternAssembly(string name) : base(name) { }

        /// <summary>
        /// 获取外部类
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IlExternClass GetIlExternClass(string name) => new IlExternClass(this, name);

        /// <summary>
        /// 输出汇编语言
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            return $".assembly extern {Name} {{}}";
        }
    }
}
