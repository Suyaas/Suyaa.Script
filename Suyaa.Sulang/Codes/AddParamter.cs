using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Codes
{
    /// <summary>
    /// 添加参数
    /// </summary>
    public class AddParamter : SuParserCode
    {
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="line"></param>
        /// <param name="pos"></param>
        /// <param name="level"></param>
        /// <param name="methodId"></param>
        public AddParamter(int line, int pos, int level, long methodId) : base(line, pos, level, SuParserCodeType.AddParamter, "-")
        {
            this.MethodId = methodId;
        }
    }
}
