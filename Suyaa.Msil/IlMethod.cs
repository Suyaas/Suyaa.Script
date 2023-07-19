using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Msil
{
    /// <summary>
    /// IL方法
    /// </summary>
    public class IlMethod : NamedAssemblable
    {
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
        public IlMethod Paramter(params IlField[] paramters)
        {
            this.Paramters.AddRange(paramters.ToList());
            return this;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IlMethod Paramter<T>(string name)
            where T : IlType
        {
            this.Paramters.Add(new IlField(name, sy.Assembly.Create<T>()));
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
            return this;
        }

        /// <summary>
        /// call
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public IlMethod Call<T>(T func)
            where T : IAssemblableInvoker
        {
            IlInstruction instruction = new IlInstruction("call");
            instruction.Paramters.Add(func);
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
            this.Keywords = new List<string>();
            this.Attachs = new List<string>();
            this.ClassType = type;
            this.Paramters = new List<IlField>();
            this.Instructions = new List<IlInstruction>();
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
                invoker.Paramter(field.Type);
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
