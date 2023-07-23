using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Helpers
{
    /// <summary>
    /// 状态助手
    /// </summary>
    public static class SuParserStatusHelper
    {
        /// <summary>
        /// 是否注释状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool IsNote(this SuParserStatus status)
        {
            return (status & SuParserStatus.Note) == SuParserStatus.Note;
        }
    }
}
