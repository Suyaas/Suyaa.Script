using Suyaa.Msil;
using Suyaa.Msil.Types;
using Suyaa.Sulang.Codes;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// 代码生效器
    /// </summary>
    public sealed class SuMaker : Disposable
    {
        // 结构体
        private List<SuStruct> _structs;
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
        public SuStruct? Struct { get; }

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
            _structs = new List<SuStruct>();
            _level = -1;
            _paramters = new List<ITypable?>();
        }

        /// <summary>
        /// 代码生效器
        /// </summary>
        /// <param name="parser"></param>
        /// <param name="asserter"></param>
        /// <param name="method"></param>
        /// <param name="struc"></param>
        /// <param name="codes"></param>
        public SuMaker(SuParser parser, SuMaker asserter, AddMethodCall method, SuStruct? struc, List<SuParserCode> codes)
        {
            Parser = parser;
            Asserter = asserter;
            Method = method;
            Struct = struc;
            Codes = codes;
            _structs = new List<SuStruct>();
            _level = -1;
            _paramters = new List<ITypable?>();
        }

        #region 断言生效

        // SetObject
        private void MakeObject(SetObject obj)
        {
            if (obj.Code != "$") throw new SuCodeException(obj, $"Unsupported object '{obj.Code}'");
            _level = 0;
            if (_structs.Count <= _level)
            {
                _structs.Add(this.Parser.Project.Global);
            }
        }

        // SetChildObject
        private void MakeChildObject(SetChildObject obj)
        {
            if (obj.Code == "$") throw new SuCodeException(obj, $"Unsupported child object '{obj.Code}'");
            if (_level < 0) throw new SuCodeException(obj, $"Missing object '$'");
            var struc = new SuStruct((IInstantiable)_structs[_level], obj.Code);
            _level++;
            if (_structs.Count <= _level)
            {
                _structs.Add(struc);
            }
            else
            {
                _structs[_level] = struc;
            }
        }

        // AddMethodCall
        private void MakeMethodCall(AddMethodCall method)
        {
            // 建立一个函数关联生效器
            using var maker = new SuMaker(
                this.Parser,
                this,
                method,
               _level >= 0 ? _structs[_level] : null,
                this.Parser.Codes.Where(d => d.MethodId == method.MethodId && d.Level != method.Level).ToList()
                );
            maker.Make();
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
            // 字符串处理
            if (value.Code.Length >= 2 && value.Code.StartsWith("\"") && value.Code.EndsWith("\""))
            {
                _methodInfo.Declare<IlString>();
                _paramters[_paramters.Count - 1] = Suable.Value(value.Code.Substring(1, value.Code.Length - 2));
                return;
            }
            // 导入类
            if (value.Code.StartsWith("@"))
            {
                _methodInfo.Declare(IlType.Type);
                _paramters[_paramters.Count - 1] = Suable.Type(this.Parser.Project.MsCorlib.GetIlExternClass(value.Code.Substring(1)).GetIlType());
                return;
            }
            // 导入变量
            _methodInfo.Declare(IlVariable.Type);
            _paramters[_paramters.Count - 1] = Suable.Variable(value.Code);
        }

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
                default: throw new SuCodeException(code, $"Unexpected code '{code.Code}'.");
            }
        }

        /// <summary>
        /// 生效代码
        /// </summary>
        public void Make()
        {
            // 有关联函数，则先添加关联函数
            if (this.Method != null)
            {
                if (this.Struct is null)
                {
                    if (!this.Method.Code.StartsWith("@")) throw new SuCodeException(this.Method, $"Missing object.");
                    // 兼容外部对象
                    string[] names = this.Method.Code.Substring(1).Split('.');
                    // 拼接类
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < names.Length - 1; i++)
                    {
                        if (sb.Length > 0) sb.Append('.');
                        sb.Append(names[i]);
                    }
                    var cls = this.Parser.Project.MsCorlib.GetIlExternClass(sb.ToString());
                    _methodInfo = Suable.Method(Suable.Struct(null, cls.GetIlType().Name), names.Last()).Keyword(IlKeys.Static);
                }
                else
                {
                    if (this.Struct is SuGlobal sg)
                    {
                        _methodInfo = sg.GetMethod(this.Method.Code);
                    }
                    else
                    {
                        _methodInfo = Suable.Method(this.Struct, this.Method.Code);
                    }
                }
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
                var invoker = _methodInfo.CreateInvoker();
                foreach (var param in _paramters)
                {
                    if (param is null) throw new SuCodeException(this.Method, $"Method paramter not found.");
                    invoker.Param(param);
                }
                this.Asserter?.Parser.Project.Assembly.Call(invoker);
            }
        }

        #endregion

    }
}
