using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Codes
{
    /// <summary>
    /// 设置对象
    /// </summary>
    public class SetObject : SuParserCode
    {
        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="line"></param>
        /// <param name="pos"></param>
        /// <param name="level"></param>
        /// <param name="code"></param>
        public SetObject(int line, int pos, int level, string code) : base(line, pos, level, SuParserCodeType.SetObject, code)
        {
            this.MethodId = 0;
        }
    }
}
