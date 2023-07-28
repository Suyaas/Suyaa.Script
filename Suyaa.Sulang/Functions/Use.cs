using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suyaa.Msil.Values;
using Suyaa.Msil.Types;

namespace Suyaa.Sulang.Functions
{
    /// <summary>
    /// 使用语句
    /// </summary>
    public sealed class Use : SuMethodInfo
    {
        /// <summary>
        /// 所属对象
        /// </summary>
        public SuGlobal Global { get; }

        ///// <summary>
        ///// 类型
        ///// </summary>
        //public IlType Type { get; }

        /// <summary>
        /// 使用语句
        /// </summary>
        /// <param name="sg"></param>
        public Use(SuGlobal sg) : base(sg, "Use")
        {
            Global = sg;
            //Type = type;
            this.Declare(new IlType(nameof(IlType)));
        }

        /// <summary>
        /// 创建执行器
        /// </summary>
        /// <returns></returns>
        public override SuMethodInvoker CreateInvoker(IlMethod method)
        {
            return new SuUseInvoker(method, this);
        }
    }

    /// <summary>
    /// Use方法执行器
    /// </summary>
    public class SuUseInvoker : SuMethodInvoker
    {

        /// <summary>
        /// Su方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="suUse"></param>
        public SuUseInvoker(IlMethod method, Use suUse) : base(method, suUse.Global, suUse.Name)
        {
        }

        /// <summary>
        /// 获取执行返回类型
        /// </summary>
        /// <returns></returns>
        public override ITypable GetInvokeReutrnType()
        {
            return this.Object;
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Invoke()
        {
            var gbl = (SuGlobal)this.Object;
            if (gbl.ContainsKey(this.Name))
            {
                throw new SuException($"Field '{this.Name}' is already exists.");
            }
            if (this.Paramters.Count != 2) throw new SuException("Method invoke does not match use(IlVariable, IlType)");
            if (!(this.Paramters[0] is SuVariable variable)) throw new SuException("Method invoke does not match use(IlVariable, IlType)");
            if (!(this.Paramters[1] is SuType type)) throw new SuException("Method invoke does not match use(IlVariable, IlType)");
            gbl.Fields.Add(new IlField(variable.Name, type.GetIlType()).Keyword(SuKeys.Class));
        }
    }
}
