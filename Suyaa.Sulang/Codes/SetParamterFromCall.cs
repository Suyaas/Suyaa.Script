using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Codes
{
    /// <summary>
    /// 设置参数值
    /// </summary>
    public class SetParamterFromCall : SuParserCode
    {
        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="line"></param>
        /// <param name="pos"></param>
        /// <param name="level"></param>
        public SetParamterFromCall(int line, int pos, int level) : base(line, pos, level, SuParserCodeType.SetParamterFromCall, "-")
        {
            this.MethodId = 0;
        }
    }
}
