using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Sulang.Codes;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// 代码生效器
    /// </summary>
    public sealed class SuMaker : Disposable
    {
        // 结构体
        private List<ITypable> _types;
        private int _level;
        private SuMethodInfo? _methodInfo;
        private List<ITypable?> _paramters;

        /// <summary>
        /// 所属解析器
        /// </summary>
        public SuParser Parser { get; }

        /// <summary>
        /// 关联生效器
        /// </summary>
        public SuMaker? Asserter { get; }

        /// <summary>
        /// 关联方法
        /// </summary>
        public AddMethodCall? Method { get; }

        /// <summary>
        /// 关联结构体
        /// </summary>
        public ITypable? Object { get; }

        /// <summary>
        /// 代码集合
        /// </summary>
        public List<SuParserCode> Codes { get; }

        /// <summary>
        /// 代码生效器
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="codes"></param>
        public SuMaker(SuParser parser, List<SuParserCode> codes)
        {
            Parser = parser;
            Codes = codes;
            _types = new List<ITypable>();
            _level = -1;
            _paramters = new List<ITypable?>();
        }

        /// <summary>
        /// 代码生效器
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="asserter"></param>
        /// <param name="method"></param>
        /// <param name="obj"></param>
        /// <param name="codes"></param>
        public SuMaker(SuParser parser, SuMaker asserter, AddMethodCall method, ITypable? obj, List<SuParserCode> codes)
        {
            Parser = parser;
            Asserter = asserter;
            Method = method;
            Object = obj;
            Codes = codes;
            _types = new List<ITypable>();
            _level = -1;
            _paramters = new List<ITypable?>();
        }

        #region 断言生效

        // SetObject
        private void MakeObject(SetObject obj)
        {
            // 获取当前作用域
            var current = Suable.Current(this.Parser.Project.Assembly.CurrentMethod);
            // 设置全局对象
            if (obj.Code == "$")
            {
                _level = 0;
                var structType = Suable.Global;
                if (_types.Count <= _level)
                {
                    _types.Add(structType);
                }
                _types[_level] = structType;
                return;
            }
            // 设置全局变量
            if (obj.Code.StartsWith("$"))
            {
                _level = 0;
                var structType = current.Struct(current.Field(Suable.Global, obj.Code.Substring(1)));
                if (_types.Count <= _level)
                {
                    _types.Add(structType);
                }
                _types[_level] = structType;
                return;
            }
            // 设置外部对象
            if (obj.Code.StartsWith("@"))
            {
                _level = 0;
                var structType = current.ExternClass(this.Parser.Project.MsCorlib.GetIlExternClass(obj.Code.Substring(1)));
                if (_types.Count <= _level)
                {
                    _types.Add(structType);
                }
                _types[_level] = structType;
                return;
            }
            throw new SuCodeException(obj, $"Unsupported object '{obj.Code}'");
        }

        // SetChildObject
        private void MakeChildObject(SetChildObject obj)
        {
            if (obj.Code == "$") throw new SuCodeException(obj, $"Unsupported child object '{obj.Code}'");
            if (_level < 0) throw new SuCodeException(obj, $"Missing object '$'");
            // 获取当前作用域
            var current = Suable.Current(this.Parser.Project.Assembly.CurrentMethod);
            var struc = current.Struct(current.Field((IInstantiable)_types[_level], obj.Code));
            _level++;
            if (_types.Count <= _level)
            {
                _types.Add(struc);
            }
            else
            {
                _types[_level] = struc;
            }
        }

        // AddMethodCall
        private void MakeMethodCall(AddMethodCall method)
        {
            // 获取当前
            var struc = _level >= 0 ? _types[_level] : null;
            // 建立一个函数关联生效器
            using var maker = new SuMaker(
                this.Parser,
                this,
                method,
                struc,
                this.Parser.Codes.Where(d => d.MethodId == method.Id && d.Level != method.Level).ToList()
                );
            _types[_level] = maker.Make();
        }

        // AddParamter
        private void MakeParamter(AddParamter param)
        {
            if (_paramters.Any() && _paramters.Last() is null) throw new SuCodeException(param, $"Unexpected paramter '{param.Code}'.");
            _paramters.Add(null);
        }

        // SetParamterValue
        private void MakeParamterValue(SetParamterValue value)
        {
            if (_methodInfo is null) throw new SuCodeException(value, $"Method info create fail.");
            if (_paramters.Any() && _paramters.Last() != null) throw new SuCodeException(value, $"Unexpected paramter '{value.Code}'.");
            // 获取当前作用域
            var current = Suable.Current(this.Parser.Project.Assembly.CurrentMethod);
            // 字符串处理
            if (value.Code.Length >= 2 && value.Code.StartsWith("\"") && value.Code.EndsWith("\""))
            {
                _methodInfo.Declare<IlString>();
                _paramters[_paramters.Count - 1] = current.Value(value.Code.Substring(1, value.Code.Length - 2));
                return;
            }
            // 导入类
            if (value.Code.StartsWith("@"))
            {
                _methodInfo.Declare(IlType.Type);
                _paramters[_paramters.Count - 1] = current.Type(this.Parser.Project.MsCorlib.GetIlExternClass(value.Code.Substring(1)));
                return;
            }
            // 导入变量或字段
            var field = current.GetField(value.Code);
            if (field is null)
            {
                _methodInfo.Declare(IlConsts.Variable);
                _paramters[_paramters.Count - 1] = new SuVariable(value.Code);
            }
            else
            {
                var suField = current.Field(current, value.Code);
                _methodInfo.Declare(suField.GetIlType());
                _paramters[_paramters.Count - 1] = suField;
            }
        }

        // SetParamterFromCall
        private void MakeCallParamter(SetParamterFromCall value)
        {
            if (_methodInfo is null) throw new SuCodeException(value, $"Method info create fail.");
            if (_paramters.Any() && _paramters.Last() != null) throw new SuCodeException(value, $"Unexpected paramter '{value.Code}'.");
            _methodInfo.Declare(_types[_level].GetIlType());
            _paramters[_paramters.Count - 1] = Suable.Stack;
        }

        #endregion

        #region 生效代码

        // 生效代码
        private void Make(SuParserCode code)
        {
            switch (code)
            {
                case SetObject obj: MakeObject(obj); break;
                case SetChildObject obj: MakeChildObject(obj); break;
                case AddMethodCall method: MakeMethodCall(method); break;
                case AddParamter param: MakeParamter(param); break;
                case SetParamterValue value: MakeParamterValue(value); break;
                case SetParamterFromCall value: MakeCallParamter(value); break;
                default: throw new SuCodeException(code, $"Unexpected code '{code.Code}'.");
            }
        }

        // 添加关联函数
        private void MakeMethodBefore(AddMethodCall method)
        {
            var current = Suable.Current(this.Parser.Project.Assembly.CurrentMethod);
            if (this.Object is null)
            {
                if (!method.Code.StartsWith("@")) throw new SuCodeException(method, $"Missing object.");
                // 兼容外部对象
                string[] names = method.Code.Substring(1).Split('.');
                // 拼接类
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < names.Length - 1; i++)
                {
                    if (sb.Length > 0) sb.Append('.');
                    sb.Append(names[i]);
                }
                var cls = this.Parser.Project.MsCorlib.GetIlExternClass(sb.ToString());
                _methodInfo = current.Method(current.Struct(current.Type(cls)), names.Last()).Keyword(IlKeys.Static);
            }
            else
            {
                try
                {
                    // 判断父对象类型
                    //switch (this.Object)
                    //{
                    //    // 处理
                    //    case SuField suField:
                    //        _methodInfo = current.Method(current.Struct(suField), method.Code);
                    //        break;
                    //    // 默认使用SuStructType
                    //    default:
                    //        _methodInfo = current.Method((SuStructType)this.Object, method.Code);
                    //        break;
                    //}
                    _methodInfo = current.Method((SuStructType)this.Object, method.Code);
                }
                catch (Exception ex)
                {
                    throw new SuCodeException(method, ex.Message);
                }
                //var structType = (IStructable)this.Object;
                //_methodInfo = structType.GetMethod(this.Method.Code);
                //if (_methodInfo is null) throw new SuCodeException(this.Method, $"Method info '{this.Method.Code}' not found.");
                //if (this.Object is SuGlobal sg)
                //{
                //    _methodInfo = sg.GetMethod(this.Method.Code);
                //    if (_methodInfo is null) throw new SuCodeException(this.Method, $"Method info '{this.Method.Code}' not found.");
                //}
                //else
                //{
                //    _methodInfo = Suable.Method((SuStructType)this.Object, this.Method.Code);
                //}
            }
        }

        /// <summary>
        /// 生效代码
        /// </summary>
        public ITypable Make()
        {
            // 有关联函数，则先添加关联函数
            if (this.Method != null)
            {
                MakeMethodBefore(this.Method);
            }
            // 依次生效代码
            foreach (var code in this.Codes)
            {
                Make(code);
            }
            // 有关联函数，则先添加关联函数
            if (this.Method != null)
            {
                if (_methodInfo is null) throw new SuCodeException(this.Method, $"Method info create fail.");
                // 创建执行器
                var invoker = _methodInfo.CreateInvoker(this.Parser.Project.Assembly.CurrentMethod);
                foreach (var param in _paramters)
                {
                    if (param is null) throw new SuCodeException(this.Method, $"Method paramter not found.");
                    invoker.Param(param);
                }
                // 判断是否预执行
                if (invoker.IsPreInvoke)
                {
                    // 执行方法
                    invoker.Invoke();
                }
                else
                {
                    // 生成Call语句
                    this.Parser.Project.Assembly.Call(invoker);
                }
                return invoker.GetInvokeReutrnType();
            }
            if (this.Object is null) return new SuVoid();
            return this.Object;
        }

        #endregion

    }
}
