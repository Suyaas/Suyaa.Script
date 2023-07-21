﻿using Suyaa.Sulang.Values;
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
    public class SuMethodInfo : NamedSuable
    {
        /// <summary>
        /// 所属结构体
        /// </summary>
        public SuStruct Object { get; }

        /// <summary>
        /// 返回类型
        /// </summary>
        public IlType? ReturnType { get; private set; }

        /// <summary>
        /// 方法参数申明
        /// </summary>
        public List<IlType> Declares { get; }

        #region 快捷函数

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

        #endregion

        /// <summary>
        /// Su方法
        /// </summary>
        /// <param name="struc"></param>
        /// <param name="name"></param>
        public SuMethodInfo(SuStruct struc, string name) : base(name)
        {
            this.Object = struc;
            this.Declares = new List<IlType>();
        }

        /// <summary>
        /// 创建一个执行器
        /// </summary>
        /// <returns></returns>
        public virtual SuMethodInvoker CreateInvoker()
        {
            var invoker = new SuMethodInvoker(this.Object, this.Name);
            invoker.Declares.AddRange(this.Declares);
            return invoker;
        }

    }
}
