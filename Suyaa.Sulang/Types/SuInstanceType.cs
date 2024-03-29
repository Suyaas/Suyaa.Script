﻿using Suyaa.Sulang.Exceptions;
using Suyaa.Msil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Suyaa.Sulang.Types
{
    /// <summary>
    /// Su实例化对象
    /// </summary>
    public class SuInstanceType : SuStructType, IInstantiable
    {
        /// <summary>
        /// 字段
        /// </summary>
        public List<IlField> Fields { get; }

        /// <summary>
        /// Su实例化对象
        /// </summary>
        /// <param name="obj"></param>
        public SuInstanceType(ITypable obj) : base(obj)
        {
            this.Fields = new List<IlField>();
        }

        /// <summary>
        /// 检测字段名称是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ContainsKey(string name)
        {
            return this.Fields.Where(d => d.Name == name).Any();
        }

        /// <summary>
        /// 设置字段
        /// </summary>
        /// <param name="field"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void SetField(IlField field)
        {
            if (ContainsKey(field.Name)) throw new SuException($"Field '{field.Name}' already exists.");
            this.Fields.Add(field);
        }

        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="SuException"></exception>
        public virtual IlField? GetField(string name)
        {
            return this.Fields.Where(d => d.Name == name).FirstOrDefault();
            //?? throw new SuException($"Field '{name}' not exists.");
        }

        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IlField this[string name]
        {
            get => GetField(name) ?? throw new SuException($"Field '{name}' not exists.");
        }
    }
}
