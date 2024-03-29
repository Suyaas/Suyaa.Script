﻿using Suyaa.Msil.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// IL方法
    /// </summary>
    public class IlMethod : NamedAssemblable, IKeywordsable
    {
        // 堆栈数量
        private int _stack;

        /// <summary>
        /// 当前堆栈数量
        /// </summary>
        public int Stack => _stack;

        /// <summary>
        /// 关键字
        /// </summary>
        public List<string> Keywords { get; }

        /// <summary>
        /// 附加关键字
        /// </summary>
        public List<string> Attachs { get; }

        /// <summary>
        /// 所属类
        /// </summary>
        public IlType ClassType { get; }

        /// <summary>
        /// 返回类型
        /// </summary>
        public IAssemblableType? ReturnType { get; private set; }

        /// <summary>
        /// 参数
        /// </summary>
        public List<IlField> Paramters { get; }

        /// <summary>
        /// 指令集合
        /// </summary>
        public List<IlInstruction> Instructions { get; }

        /// <summary>
        /// 字段集合
        /// </summary>
        public List<IlField> Fields { get; }

        #region 快捷函数

        /// <summary>
        /// 设置关键字
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public IlMethod Keyword(params string[] keywords)
        {
            foreach (var key in keywords)
            {
                if (!this.Keywords.Contains(key)) this.Keywords.Add(key);
            }
            return this;
        }

        /// <summary>
        /// 设置附加关键字
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public IlMethod Attach(params string[] keywords)
        {
            foreach (var key in keywords)
            {
                if (!this.Attachs.Contains(key)) this.Attachs.Add(key);
            }
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public IlMethod Param(params IlField[] paramters)
        {
            this.Paramters.AddRange(paramters.ToList());
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IlMethod Param<T>(string name)
            where T : IlType
        {
            this.Paramters.Add(new IlField(name, sy.Assembly.Create<T>()));
            return this;
        }


        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public IlMethod Field(string name, IlType type)
        {
            this.Fields.Add(new IlField(name, type));
            return this;
        }

        #endregion

        #region 指令操作

        /// <summary>
        /// ldstr
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IlMethod Ldstr(IAssemblable content)
        {
            IlInstruction instruction = new IlInstruction("ldstr");
            instruction.Paramters.Add(content);
            this.Instructions.Add(instruction);
            _stack++;
            return this;
        }

        /// <summary>
        /// ldloca.s
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IlMethod Ldloca_s(IAssemblable content)
        {
            IlInstruction instruction = new IlInstruction("ldloca.s");
            instruction.Paramters.Add(content);
            this.Instructions.Add(instruction);
            _stack++;
            return this;
        }

        /// <summary>
        /// ldloc.s
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IlMethod Ldloc_s(IAssemblable content)
        {
            IlInstruction instruction = new IlInstruction("ldloc.s");
            instruction.Paramters.Add(content);
            this.Instructions.Add(instruction);
            _stack++;
            return this;
        }

        /// <summary>
        /// ldloc.x
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IlMethod Ldloc(int index)
        {
            IlInstruction instruction = new IlInstruction($"ldloc.{index}");
            this.Instructions.Add(instruction);
            _stack++;
            return this;
        }

        /// <summary>
        /// ldc.i4.x
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IlMethod Ldc_i4(int value)
        {
            IlInstruction instruction = new IlInstruction($"ldc.i4.{value}");
            this.Instructions.Add(instruction);
            _stack++;
            return this;
        }

        /// <summary>
        /// stloc.s
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IlMethod Stloc_s(IAssemblable content)
        {
            IlInstruction instruction = new IlInstruction("stloc.s");
            instruction.Paramters.Add(content);
            this.Instructions.Add(instruction);
            _stack--;
            return this;
        }

        /// <summary>
        /// stloc.x
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IlMethod Stloc(int index)
        {
            IlInstruction instruction = new IlInstruction($"stloc.{index}");
            this.Instructions.Add(instruction);
            _stack--;
            return this;
        }

        /// <summary>
        /// call
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public IlMethod Call<T>(T func)
            where T : IlMethodInvoker
        {
            IlInstruction instruction = new IlInstruction("call");
            instruction.Paramters.Add(func);
            this.Instructions.Add(instruction);
            _stack = func.ReturnType is null ? 0 : 1;
            return this;
        }

        /// <summary>
        /// add
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IlMethod Add()
        {
            IlInstruction instruction = new IlInstruction("add");
            this.Instructions.Add(instruction);
            return this;
        }

        /// <summary>
        /// sub
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IlMethod Sub()
        {
            IlInstruction instruction = new IlInstruction("sub");
            this.Instructions.Add(instruction);
            return this;
        }

        /// <summary>
        /// mul
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IlMethod Mul()
        {
            IlInstruction instruction = new IlInstruction("mul");
            this.Instructions.Add(instruction);
            return this;
        }

        /// <summary>
        /// div
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IlMethod Div()
        {
            IlInstruction instruction = new IlInstruction("div");
            this.Instructions.Add(instruction);
            return this;
        }

        /// <summary>
        /// div.un
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IlMethod Div_Un()
        {
            IlInstruction instruction = new IlInstruction("div.un");
            this.Instructions.Add(instruction);
            return this;
        }

        /// <summary>
        /// nop
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IlMethod Nop()
        {
            IlInstruction instruction = new IlInstruction("nop");
            this.Instructions.Add(instruction);
            return this;
        }

        /// <summary>
        /// pop
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IlMethod Pop()
        {
            IlInstruction instruction = new IlInstruction("pop");
            this.Instructions.Add(instruction);
            return this;
        }

        /// <summary>
        /// ret
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IlMethod Ret()
        {
            IlInstruction instruction = new IlInstruction("ret");
            this.Instructions.Add(instruction);
            return this;
        }

        #endregion

        /// <summary>
        /// IL方法
        /// </summary>
        /// <param name="name"></param>
        public IlMethod(IlType type, string name) : base(name)
        {
            _stack = 0;
            this.Keywords = new List<string>();
            this.Attachs = new List<string>();
            this.ClassType = type;
            this.Paramters = new List<IlField>();
            this.Instructions = new List<IlInstruction>();
            this.Fields = new List<IlField>();
        }

        /// <summary>
        /// 获取汇编指令
        /// </summary>
        /// <returns></returns>
        public override string ToAssembly()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(".method");
            // 添加关键字
            foreach (var key in this.Keywords)
            {
                sb.Append(' ');
                sb.Append(key);
            }
            // 添加返回类型
            sb.Append(' ');
            if (this.ReturnType is null)
            {
                sb.Append(IlKeys.Void);
            }
            else
            {
                sb.Append(this.ReturnType.ToAssembly());
            }
            // 添加方法名
            sb.Append(' ');
            sb.Append(this.Name);
            // 添加参数信息
            sb.Append(" (");
            bool isFirstField = true;
            foreach (var field in this.Paramters)
            {
                if (isFirstField)
                {
                    isFirstField = false;
                }
                else
                {
                    sb.Append(", ");
                }
                sb.Append(field.ToAssembly());
            }
            sb.Append(')');
            // 添加附加关键字
            foreach (var key in this.Attachs)
            {
                sb.Append(' ');
                sb.Append(key);
            }
            sb.AppendLine(" {");
            sb.AppendLine(".entrypoint");
            // 添加变量初始化
            if (this.Fields.Any())
            {
                sb.AppendLine(".locals init (");
                StringBuilder sbField = new StringBuilder();
                foreach (var field in this.Fields)
                {
                    if (sbField.Length > 0) sbField.AppendLine(",");
                    sbField.Append(field.ToAssembly());
                }
                sb.AppendLine(sbField.ToString());
                sb.AppendLine(")");
                sbField.Clear();
            }
            // 添加指令集
            foreach (var instruction in this.Instructions)
            {
                sb.AppendLine(instruction.ToAssembly());
            }
            sb.Append('}');
            return sb.ToString();
        }

        /// <summary>
        /// 获取执行器
        /// </summary>
        /// <returns></returns>
        public IlMethodInvoker ToInvoker()
        {
            var invoker = new IlMethodInvoker(this.ClassType, this.Name);
            foreach (var field in this.Paramters)
            {
                invoker.Param(field.Type);
            }
            return invoker;
        }

        #region 释放资源

        /// <summary>
        /// 托管资源释放
        /// </summary>
        protected override void OnManagedDispose()
        {
            // 释放可汇编对象
            this.Keywords.Clear();
            for (int i = 0; i < Paramters.Count; i++) this.Paramters[i].Dispose();
            this.Paramters.Clear();
            this.Attachs.Clear();
            this.ReturnType?.Dispose();
            for (int i = 0; i < Instructions.Count; i++) this.Instructions[i].Dispose();
            this.Instructions.Clear();
            base.OnManagedDispose();
        }

        #endregion
    }
}
