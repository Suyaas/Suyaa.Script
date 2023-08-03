using Suyaa.Msil;
using Suyaa.Msil.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// Su方法
    /// </summary>
    public class SuMethodInfo : NamedSuable, IKeywordsable
    {
        /// <summary>
        /// 所属结构体
        /// </summary>
        public ITypable Object { get; }

        /// <summary>
        /// 返回类型
        /// </summary>
        public ITypable ReturnType { get; private set; }

        /// <summary>
        /// 方法参数申明
        /// </summary>
        public List<IlType> Declares { get; }

        /// <summary>
        /// 关键字
        /// </summary>
        public List<string> Keywords { get; }

        #region 快捷函数

        /// <summary>
        /// 设置关键字
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public SuMethodInfo Keyword(params string[] keywords)
        {
            foreach (var key in keywords)
            {
                if (!this.Keywords.Contains(key)) this.Keywords.Add(key);
            }
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public SuMethodInfo Declare(params IlType[] types)
        {
            Declares.AddRange(types.ToList());
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <returns></returns>
        public SuMethodInfo Declare<T>()
            where T : IlType
        {
            Declares.Add(sy.Assembly.Create<T>());
            return this;
        }

        /// <summary>
        /// 设定返回参数
        /// </summary>
        /// <returns></returns>
        public SuMethodInfo Return(ITypable type)
        {
            this.ReturnType = type;
            return this;
        }

        #endregion

        /// <summary>
        /// Su方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        public SuMethodInfo(ITypable obj, string name) : base(name)
        {
            this.Object = obj;
            this.Declares = new List<IlType>();
            this.Keywords = new List<string>();
            this.ReturnType = new SuVoid();
        }

        /// <summary>
        /// 创建一个执行器
        /// </summary>
        /// <returns></returns>
        public virtual SuMethodInvoker CreateInvoker(IlMethod method, SuParserCode code)
        {
            var invoker = new SuMethodInvoker(method, code, this.Object, this.Name);
            invoker.Declares.AddRange(this.Declares);
            invoker.Keywords.AddRange(this.Keywords);
            invoker.Return(this.ReturnType);
            return invoker;
        }

    }
}
