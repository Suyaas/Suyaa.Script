using Suyaa.Msil;
using Suyaa.Sulang.Codes;
using Suyaa.Sulang.Exceptions;
using Suyaa.Sulang.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace Suyaa.Sulang
{
    /// <summary>
    /// Su语法解析状态
    /// </summary>
    public enum SuParserStatus : int
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None = 0x000,

        /// <summary>
        /// 注释
        /// </summary>
        Note = 0x100,
        /// <summary>
        /// 行注释
        /// </summary>
        LineNote = Note + 1,
        /// <summary>
        /// 块注释
        /// </summary>
        BlockNote = Note + 2,

        /// <summary>
        /// 方法名称
        /// </summary>
        MethodName = 0x20,
        /// <summary>
        /// 方法参数
        /// </summary>
        MethodParam = 0x40,
        /// <summary>
        /// 方法完成
        /// </summary>
        MethodFinish = 0x80,

        /// <summary>
        /// 字符串
        /// </summary>
        String = 0x800,
        /// <summary>
        /// 字符串转义
        /// </summary>
        StringEscape = String + 1,

        /// <summary>
        /// 数值
        /// </summary>
        Numeric = 0x1000,
        /// <summary>
        /// 浮点数值
        /// </summary>
        FloatNumeric = Numeric + 1,
    }

    /// <summary>
    /// Su语法解析器
    /// </summary>
    public sealed class SuParser : Disposable
    {
        // 字符串缓存
        private StringBuilder _cache;
        private StringBuilder _code;
        // 代码层级
        private int _level;
        private int _line;
        private int _pos;
        private long _methodId;
        // 状态
        private List<SuParserStatus> _statuses;
        // 调用链
        private List<long> _methods;

        /// <summary>
        /// Su项目
        /// </summary>
        public SuProject Project { get; }

        /// <summary>
        /// 代码段集合
        /// </summary>
        public List<SuParserCode> Codes { get; }

        /// <summary>
        /// Su语法解析器
        /// </summary>
        /// <param name="project"></param>
        public SuParser(SuProject project)
        {
            Project = project;
            _cache = new StringBuilder();
            _code = new StringBuilder();
            Codes = new List<SuParserCode>();
            _methods = new List<long>() { 0 };
            _statuses = new List<SuParserStatus>() { SuParserStatus.None };
        }

        // 初始化
        private void Init()
        {
            // 变量初始化
            _cache.Clear();
            _code.Clear();
            _level = 0;
            _line = 1;
            _pos = 0;
            _methods[0] = 0;
            _statuses[0] = SuParserStatus.None;
        }

        #region 格式化脚本

        // 反斜杠 \
        private void ParseBackslash(char chr)
        {
            var status = _statuses[_level];
            // 处理注释
            if (status.IsNote())
            {
                _cache.Append(chr);
                return;
            }
            // 如果处于转义中，则转为常规字符串
            if (status == SuParserStatus.StringEscape)
            {
                _code.Append(chr);
                _statuses[_level] = SuParserStatus.String;
                return;
            }
            // 在字符串中，则进行转义
            if (status == SuParserStatus.String)
            {
                _code.Append(chr);
                _statuses[_level] = SuParserStatus.StringEscape;
                return;
            }
            throw new SuException($"Unexpected character '{chr}'.");
        }

        // 斜杠 /
        private void ParseSlash(char chr)
        {
            var status = _statuses[_level];
            // 如果处于转义中，则抛出异常
            if (status == SuParserStatus.StringEscape) throw new SuException($"Unexpected character '{chr}'.");
            // 在字符串中则正常添加内容
            if (status == SuParserStatus.String)
            {
                _code.Append(chr);
                return;
            }
            // 退出块注释
            if (status == SuParserStatus.BlockNote && _cache.Length >= 3 && _cache[_cache.Length - 1] == '*')
            {
                _cache.Clear();
                _statuses[_level] = SuParserStatus.None;
                return;
            }
            _cache.Append(chr);
            // 进入行注释
            if (_cache.Length == 2)
            {
                _statuses[_level] = SuParserStatus.LineNote;
                return;
            }
        }

        // 星号 *
        private void ParseAsterisk(char chr)
        {
            var status = _statuses[_level];
            // 如果处于转义中，则抛出异常
            if (status == SuParserStatus.StringEscape) throw new SuException($"Unexpected character '{chr}'.");
            // 处理注释
            if (status.IsNote())
            {
                _cache.Append(chr);
                return;
            }
            // 在字符串中则正常添加内容
            if (status == SuParserStatus.String)
            {
                _code.Append(chr);
                return;
            }
            // 当前面的字符只存在/时，进入块注释
            if (_cache.Length == 1 && _cache[0] == '/')
            {
                _cache.Append(chr);
                _statuses[_level] = SuParserStatus.BlockNote;
                return;
            }
            // 添加
            _code.Append(chr);
        }

        // 引号 "
        private void ParseQuotation(char chr)
        {
            var status = _statuses[_level];
            // 处理注释
            if (status.IsNote())
            {
                _cache.Append(chr);
                return;
            }
            // 如果处于转义中，则取消转义
            if (status == SuParserStatus.StringEscape)
            {
                _cache.Append(chr);
                _statuses[_level] = SuParserStatus.String;
                return;
            }
            // 在字符串中则结束字符串定义，恢复到无状态
            if (status == SuParserStatus.String)
            {
                _code.Append(chr);
                _statuses[_level] = SuParserStatus.None;
                return;
            }
            // 如果处于非空或非参数状态，则抛出异常
            if (status != SuParserStatus.None && status != SuParserStatus.MethodParam) throw new SuException($"Unexpected character '{chr}'.");
            // 如果已经有内容，则报错
            if (_code.Length > 0) throw new SuException($"Unexpected character '{chr}'.");
            // 添加内容并设置字符串模式
            _code.Append(chr);
            _statuses[_level] = SuParserStatus.String;
        }

        // 左括号 (
        private void ParseLeftBracket(char chr)
        {
            var status = _statuses[_level];
            // 如果处于转义中，则抛出异常
            if (status == SuParserStatus.StringEscape) throw new SuException($"Unexpected character '{chr}'.");
            // 处理注释
            if (status.IsNote())
            {
                _cache.Append(chr);
                return;
            }
            // 处理字符串
            if (status == SuParserStatus.String)
            {
                _code.Append(chr);
                return;
            }
            // 如果处于非函数名称状态，则抛出异常
            if (status != SuParserStatus.MethodName) throw new SuException($"Unexpected character '{chr}'.");
            // 函数必须是.开头或者$@
            if (_code[0] != '.' && !(_code[0] == '$' && _code[1] == '@')) throw new SuException($"Unexpected character '{chr}'.");
            string funName;
            // 将外部调用和常规调用分开处理
            if (_code[0] == '$' && _code[1] == '@')
            {
                string[] codes = _code.ToString(1, _code.Length - 1).Split('.');
                if (codes.Length <= 1) throw new SuException($"Unexpected character '{chr}'.");
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < codes.Length - 1; i++)
                {
                    if (sb.Length > 0) sb.Append('.');
                    sb.Append(codes[i]);
                }
                // 添加$对象
                this.Codes.Add(new SetObject(_line, _pos, _level, sb.ToString()) { MethodId = _methodId });
                funName = codes.Last();
            }
            else
            {
                funName = _code.ToString(1, _code.Length - 1);
            }
            // 添加代码调用
            var methodCall = new AddMethodCall(_line, _pos, _level, funName) { MethodId = _methodId };
            this.Codes.Add(methodCall);
            // 常规添加字符，同时代码层级+1
            //_code.Append(chr);
            // 设置状态为参数
            _statuses[_level] = SuParserStatus.MethodParam;
            _level++;
            if (_level >= _statuses.Count) _statuses.Add(SuParserStatus.None);
            // 设置调用链
            if (_methods.Count <= _level)
            {
                _methods.Add(methodCall.Id);
            }
            else
            {
                _methods[_level] = methodCall.Id;
            }
            _methodId = methodCall.Id;
            // 设置状态为参数
            _statuses[_level] = SuParserStatus.None;
            // 清理代码缓存
            _code.Clear();
        }

        // 右括号 )
        private void ParseRightBracket(char chr)
        {
            var status = _statuses[_level];
            // 如果处于转义中，则抛出异常
            if (status == SuParserStatus.StringEscape) throw new SuException($"Unexpected character '{chr}'.");
            // 处理注释
            if (status.IsNote())
            {
                _cache.Append(chr);
                return;
            }
            // 处理字符串
            if (status == SuParserStatus.String)
            {
                _code.Append(chr);
                return;
            }
            // 处理函数传参
            if (status == SuParserStatus.MethodFinish)
            {
                if (_code.Length > 0) throw new SuException($"Unexpected character '{chr}'.");
                this.Codes.Add(new SetParamterFromCall(_line, _pos, _level) { MethodId = _methodId });
            }
            else
            {
                // 添加未处理的参数
                if (_code.Length > 0)
                {
                    var lastCode = this.Codes.Last();
                    // 层级有变化，则先添加一个参数添加操作
                    if (lastCode.Level != _level)
                    {
                        this.Codes.Add(new AddParamter(_line, _pos, _level, _methodId) { MethodId = _methodId });
                    }
                    this.Codes.Add(new SetParamterValue(_line, _pos, _level, _code.ToString()) { MethodId = _methodId });
                    // 清理代码缓存
                    _code.Clear();
                    // 设置本层状态
                    _statuses[_level] = SuParserStatus.None;
                }
            }
            // 常规添加字符，同时代码层级-1
            //_code.Append(chr);
            _level--;
            // 设置本层状态
            _statuses[_level] = SuParserStatus.MethodFinish;
            // 设置当前方法
            _methodId = _methods[_level];
        }

        // 换行 \n
        private void ParseWrap(char chr)
        {
            var status = _statuses[_level];
            // 如果处于转义中，则抛出异常
            if (status == SuParserStatus.StringEscape) throw new SuException($"Unexpected character '\\n'.");
            // 如果在行注释中，则取消注释
            if (status == SuParserStatus.LineNote)
            {
                _cache.Clear();
                _statuses[_level] = SuParserStatus.None;
                _line++;
                _pos = 0;
                return;
            }
            // 当在块注释中，添加换行
            if (status == SuParserStatus.BlockNote)
            {
                _cache.Append(chr);
                _line++;
                _pos = 0;
                return;
            }
            // 行数+1
            _line++;
            _pos = 0;
        }

        // 美元符号 $
        private void ParseDollar(char chr)
        {
            var status = _statuses[_level];
            // 如果处于转义中，则抛出异常
            if (status == SuParserStatus.StringEscape) throw new SuException($"Unexpected character '{chr}'.");
            // 处理注释
            if (status.IsNote())
            {
                _cache.Append(chr);
                return;
            }
            // 在字符串中则正常添加内容
            if (status == SuParserStatus.String)
            {
                _code.Append(chr);
                return;
            }
            // 如果处于非空或非参数状态，则抛出异常
            if (status != SuParserStatus.None && status != SuParserStatus.MethodParam && status != SuParserStatus.MethodFinish) throw new SuException($"Unexpected character '{chr}'.");
            if (_level > 0)
            {
                // 添加一个参数定义
                this.Codes.Add(new AddParamter(_line, _pos, _level, _methodId) { MethodId = _methodId });
            }
            // 添加代码
            _code.Append(chr);
            // 设置状态为方法名称
            _statuses[_level] = SuParserStatus.MethodName;
        }

        // 点号 .
        private void ParseDot(char chr)
        {
            var status = _statuses[_level];
            // 如果处于转义中，则抛出异常
            if (status == SuParserStatus.StringEscape) throw new SuException($"Unexpected character '{chr}'.");
            // 处理注释
            if (status.IsNote())
            {
                _cache.Append(chr);
                return;
            }
            // 在字符串中则正常添加内容
            if (status == SuParserStatus.String)
            {
                _code.Append(chr);
                return;
            }
            // 在方法名称中，则处理对象操作
            if (status == SuParserStatus.MethodName)
            {
                if (_code.Length <= 0) throw new SuException($"Unexpected character '{chr}'.");
                // 兼容$对象
                if (_code.Length == 1 && _code[0] == '$')
                {
                    // 添加全局对象
                    this.Codes.Add(new SetObject(_line, _pos, _level, _code.ToString()) { MethodId = _methodId });
                    _code.Clear();
                    _code.Append(chr);
                    return;
                }
                // 兼容全局对象
                if (_code.Length > 1 && _code[0] == '$')
                {
                    // 兼容全局外部对象引用，不处理，直接添加
                    if (_code[1] == '@')
                    {
                        if (_code.Length == 2) throw new SuException($"Unexpected character '{chr}'.");
                        _code.Append(chr);
                        return;
                    }
                    // 添加$对象
                    this.Codes.Add(new SetObject(_line, _pos, _level, "$") { MethodId = _methodId });
                    // 添加子对象
                    this.Codes.Add(new SetChildObject(_line, _pos, _level, _code.ToString(1, _code.Length - 1)) { MethodId = _methodId });
                    _code.Clear();
                    _code.Append(chr);
                    return;
                }
                // 子对象必须是.开头
                if (_code[0] != '.') throw new SuException($"Unexpected character '{chr}'.");
                // 添加子对象
                this.Codes.Add(new SetChildObject(_line, _pos, _level, _code.ToString(1, _code.Length - 1)) { MethodId = _methodId });
                _code.Clear();
                _code.Append(chr);
                return;
            }
            // 添加
            _code.Append(chr);
            _statuses[_level] = SuParserStatus.MethodName;
        }

        // 逗号 ,
        private void ParseComma(char chr)
        {
            var status = _statuses[_level];
            // 如果处于转义中，则抛出异常
            if (status == SuParserStatus.StringEscape) throw new SuException($"Unexpected character '{chr}'.");
            // 处理注释
            if (status.IsNote())
            {
                _cache.Append(chr);
                return;
            }
            // 在字符串中则正常添加内容
            if (status == SuParserStatus.String)
            {
                _code.Append(chr);
                return;
            }
            // 添加未处理的参数
            if (_code.Length > 0)
            {
                // 判断是否为首参数
                var lastCode = this.Codes.Last();
                // 层级有变化，则先添加一个参数添加操作
                if (lastCode.Level != _level)
                {
                    this.Codes.Add(new AddParamter(_line, _pos, _level, _methodId) { MethodId = _methodId });
                }
                // 添加值处理
                this.Codes.Add(new SetParamterValue(_line, _pos, _level, _code.ToString()) { MethodId = _methodId });
                // 清理代码缓存
                _code.Clear();
            }
            // 添加参数添加代码
            this.Codes.Add(new AddParamter(_line, _pos, _level, _methodId));
        }

        // 空格 ' '
        private void ParseSpace(char chr)
        {
            var status = _statuses[_level];
            // 如果处于转义中，则抛出异常
            if (status == SuParserStatus.StringEscape) throw new SuException($"Unexpected character '{chr}'.");
            // 处理注释
            if (status.IsNote())
            {
                _cache.Append(chr);
                return;
            }
            // 在字符串中则正常添加内容
            if (status == SuParserStatus.String)
            {
                _code.Append(chr);
                return;
            }
        }

        /*
         * 格式化脚本
         * 比如 $.Console.Writeline("hello")
         * 会格式化为以下脚本$1:代表源代码中的脚本为第一行
         * L1V0F0:$
         * L1V0F0:.Conole
         * L1V0F0:.Writeline
         * L1V0F1:(
         * L1V1F1:    "hello"
         * L1V0F1:)
         */
        private void ParseToCodes(string script)
        {
            // 初始化
            this.Init();
            this.Codes.Clear();
            // 遍历脚本
            for (int i = 0; i < script.Length; i++)
            {
                _pos++;
                char chr = script[i];
                switch (chr)
                {
                    case '\\': ParseBackslash(chr); break;
                    case '/': ParseSlash(chr); break;
                    case '*': ParseAsterisk(chr); break;
                    case '(': ParseLeftBracket(chr); break;
                    case ')': ParseRightBracket(chr); break;
                    case '.': ParseDot(chr); break;
                    case ',': ParseComma(chr); break;
                    case '$': ParseDollar(chr); break;
                    case '"': ParseQuotation(chr); break;
                    case ' ': ParseSpace(chr); break;
                    case '\r': break;
                    case '\n': ParseWrap(chr); break;
                    default:
                        // 处理注释
                        if (_statuses[_level].IsNote())
                        {
                            _cache.Append(chr);
                            continue;
                        }
                        // 如果有缓存内容，则抛出异常
                        if (_cache.Length > 0) throw new SuException($"Unexpected character '{chr}'.");
                        _code.Append(chr);
                        break;
                }
            }
            if (_level != 0) throw new SuException($"The code has not yet ended.");
            if (_statuses[_level] != SuParserStatus.MethodFinish) throw new SuException($"The code has not yet ended.");
        }

        #endregion

        /// <summary>
        /// 执行语法解析
        /// </summary>
        /// <param name="script"></param>
        public void Parse(string script)
        {
            // 格式化脚本 - 清理所有的注释与无用的换行符
            try
            {
                ParseToCodes(script);
            }
            catch (Exception ex)
            {
                throw new SuException($"Parse line {_line} pos {_pos} error: {ex.Message}");
            }
            // 输出sup文件
            string path = sy.IO.CombinePath(this.Project.IlProject.Folder, this.Project.IlProject.Name + ".sup");
            StringBuilder sb = new StringBuilder();
            foreach (var code in this.Codes)
            {
                sb.AppendLine(code.ToCodeString());
            }
            sy.IO.WriteUtf8FileContent(path, sb.ToString());
            // 生效第一层代码
            using var maker = new SuMaker(this, this.Codes.Where(d => d.Level == 0).ToList());
            maker.Make();
        }
    }
}
