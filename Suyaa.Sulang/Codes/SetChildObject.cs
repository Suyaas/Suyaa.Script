using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Codes
{
    /// <summary>
    /// 设置对象
    /// </summary>
    public class SetChildObject : SuParserCode
    {
        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="line"></param>
        /// <param name="pos"></param>
        /// <param name="level"></param>
        /// <param name="code"></param>
        public SetChildObject(int line, int pos, int level, string code) : base(line, pos, level, SuParserCodeType.SetChildObject, code)
        {
            this.MethodId = 0;
        }
    }
}
