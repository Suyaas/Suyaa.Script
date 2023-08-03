using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Codes
{
    /// <summary>
    /// 未定位代码
    /// </summary>
    public class NoCode : SuParserCode
    {
        /// <summary>
        /// 未定位代码
        /// </summary>
        public NoCode() : base(0, 0, 0, SuParserCodeType.Unknow, "")
        {
            this.MethodId = 0;
        }
    }
}
