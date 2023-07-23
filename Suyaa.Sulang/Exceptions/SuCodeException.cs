using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Exceptions
{
    /// <summary>
    /// Su代码异常
    /// </summary>
    public class SuCodeException : Exception
    {
        /// <summary>
        /// Su代码片段
        /// </summary>
        public SuParserCode Code { get; }

        /// <summary>
        /// Su代码异常
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public SuCodeException(SuParserCode code, string message) : base($"Line {code.Line} {code.Type} error: {message}")
        {
            Code = code;
        }
    }
}
