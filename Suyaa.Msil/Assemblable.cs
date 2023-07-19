using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// 可汇编对象
    /// </summary>
    public abstract class Assemblable : Disposable, IAssemblable
    {
        /// <summary>
        /// 转化为汇编指令
        /// </summary>
        /// <returns></returns>
        public abstract string ToAssembly();
    }
}
