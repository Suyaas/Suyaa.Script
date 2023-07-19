using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil.ExternAssemblies
{
    /// <summary>
    /// mscorlib
    /// </summary>
    public class MsCorlib : IlExternAssembly
    {
        /// <summary>
        /// 对象
        /// </summary>
        public IlExternType Object { get; }

        /// <summary>
        /// mscorlib
        /// </summary>
        public MsCorlib() : base("mscorlib")
        {
            this.Object = new IlExternType(this, "System.Object");
        }
    }
}
