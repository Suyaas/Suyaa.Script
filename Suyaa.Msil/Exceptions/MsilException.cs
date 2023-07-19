using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Msil.Exceptions
{
    /// <summary>
    /// Msil异常
    /// </summary>
    public class MsilException : Exception
    {
        /// <summary>
        /// Msil异常
        /// </summary>
        /// <param name="message"></param>
        public MsilException(string message) : base(message) { }
    }
}
