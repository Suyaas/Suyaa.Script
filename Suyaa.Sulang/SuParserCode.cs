using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// 代码描述类型
    /// </summary>
    public enum SuParserCodeType : int
    {
        /// <summary>
        /// 未知类型
        /// </summary>
        Unknow = 0x00,
        /// <summary>
        /// 设置对象
        /// </summary>
        SetObject = 0x01,
        /// <summary>
        /// 设置子对象
        /// </summary>
        SetChildObject = 0x02,
        /// <summary>
        /// 添加方法调用
        /// </summary>
        AddMethodCall = 0x11,
        /// <summary>
        /// 添加方法参数
        /// </summary>
        AddParamter = 0x21,
        /// <summary>
        /// 设置参数值
        /// </summary>
        SetParamterValue = 0x31,
    }

    /// <summary>
    /// 代码描述
    /// </summary>
    public abstract class SuParserCode : Disposable
    {

        /// <summary>
        /// 源代码行标
        /// </summary>
        public int Line { get; }

        /// <summary>
        /// 源代码行中位置
        /// </summary>
        public int Pos { get; }

        /// <summary>
        /// 代码层级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 方法编号
        /// </summary>
        public long MethodId { get; set; }

        /// <summary>
        /// 代码类型
        /// </summary>
        public SuParserCodeType Type { get; }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// 代码描述
        /// </summary>
        /// <param name="line"></param>
        /// <param name="pos"></param>
        /// <param name="level"></param>
        /// <param name="type"></param>
        /// <param name="code"></param>
        public SuParserCode(int line, int pos, int level, SuParserCodeType type, string code)
        {
            this.Line = line;
            this.Pos = pos;
            this.Level = level;
            this.Type = type;
            this.Code = code;
        }

        /// <summary>
        /// 转化为字符串
        /// </summary>
        /// <returns></returns>
        public string ToCodeString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(new string(' ', this.Level * 4));
            sb.Append($"[0x{this.MethodId.ToString("x").PadLeft(8, '0')}] {this.Type} {this.Code} :");
            sb.Append($" Line {this.Line}");
            sb.Append($" Pos {this.Pos}");
            sb.Append($" Level {this.Level}");
            return sb.ToString();
        }

    }
}
