using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Msil.Values;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Functions;
using Suyaa.Sulang.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Sulang.Values
{
    /// <summary>
    /// Su字段值
    /// </summary>
    public class SuFieldValue : SuValue<SuField>
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        public SuStructType StructType { get; }

        /// <summary>
        /// Su值
        /// </summary>
        /// <param name="field"></param>
        /// <param name="type"></param>
        public SuFieldValue(SuField field, SuStructType type) : base(field, type)
        {
            StructType = type;
            // 继承函数
            foreach (var method in type.Methods)
            {
                var obj = (SuMethodInfo)Activator.CreateInstance(method.GetType(), this);
                this.Methods.Add(obj);
            }
        }

        /// <summary>
        /// 获取IlType
        /// </summary>
        /// <returns></returns>
        public override IlType GetIlType()
        {
            return StructType.GetIlType();
        }

        /// <summary>
        /// 获取代码字符串
        /// </summary>
        /// <returns></returns>
        public override string ToCodeString()
        {
            return $"({this.GetIlType().GetType().FullName} {this.Value.Name})";
        }
    }
}
