using Ssm.Jian.Engine;
using Suyaa.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Script.Apis.Common
{
    /// <summary>
    /// 控制台函数注册
    /// </summary>
    public class NumericRegistr : ScriptRegistrBase
    {
        /// <summary>
        /// 大于
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        [Func("大于")]
        [JianFunc("大于")]
        public bool Greater(double value1, double value2)
        {
            return value1 > value2;
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        [Func]
        [JianFunc("大于等于")]
        public bool GreaterEqual(double value1, double value2)
        {
            return value1 > value2;
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        [Func]
        [JianFunc("小于")]
        public bool Less(double value1, double value2)
        {
            return value1 < value2;
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        [Func]
        [JianFunc("小于等于")]
        public bool LessEqual(double value1, double value2)
        {
            return value1 <= value2;
        }
    }
}
