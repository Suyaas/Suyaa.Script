using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Codes
{
    /// <summary>
    /// 添加方法调用
    /// </summary>
    public class AddMethodCall : SuParserCode
    {
        // 种子
        private static long _methodIder = 0;

        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="line"></param>
        /// <param name="pos"></param>
        /// <param name="level"></param>
        /// <param name="code"></param>
        public AddMethodCall(int line, int pos, int level, string code) : base(line, pos, level, SuParserCodeType.AddMethodCall, code)
        {
            this.MethodId = ++_methodIder;
        }
    }
}
