using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Lang.Exceptions
{
    /// <summary>
    /// Su语言异常
    /// </summary>
    public class SuException : Exception
    {
        /// <summary>
        /// Su语言异常
        /// </summary>
        /// <param name="message"></param>
        public SuException(string message) : base(message) { }
    }
}
