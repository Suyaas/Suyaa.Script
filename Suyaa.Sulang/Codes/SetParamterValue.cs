using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Codes
{
    /// <summary>
    /// 设置参数值
    /// </summary>
    public class SetParamterValue : SuParserCode
    {
        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="line"></param>
        /// <param name="pos"></param>
        /// <param name="level"></param>
        /// <param name="code"></param>
        public SetParamterValue(int line, int pos, int level, string code) : base(line, pos, level, SuParserCodeType.SetParamterValue, code)
        {
            this.MethodId = 0;
        }
    }
}
