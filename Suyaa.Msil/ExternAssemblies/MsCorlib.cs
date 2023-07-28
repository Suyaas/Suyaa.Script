using System;
using System.Collections.Generic;
using System.Text;
using Suyaa.Msil.Types;

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
        public IlExternClass Object { get; }

        /// <summary>
        /// mscorlib
        /// </summary>
        public MsCorlib() : base("mscorlib")
        {
            this.Object = new IlExternClass(this, "System.Object");
        }
    }
}
