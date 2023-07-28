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
        private static long _ider = 0;

        /// <summary>
        /// 唯一标识
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// 设置对象
        /// </summary>
        /// <param name="line"></param>
        /// <param name="pos"></param>
        /// <param name="level"></param>
        /// <param name="code"></param>
        public AddMethodCall(int line, int pos, int level, string code) : base(line, pos, level, SuParserCodeType.AddMethodCall, code)
        {
            this.Id = ++_ider;
        }

        /// <summary>
        /// 转化为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToCodeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(new string(' ', this.Level * 4));
            sb.Append($"[0x{this.MethodId.ToString("x").PadLeft(8, '0')}] {this.Type} {this.Code} :");
            sb.Append($" Line {this.Line}");
            sb.Append($" Pos {this.Pos}");
            sb.Append($" Level {this.Level}");
            sb.Append($" Id {this.Id}");
            return sb.ToString();
        }
    }
}
