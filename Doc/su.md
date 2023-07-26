# 苏语言(su lang)

基于.Net CLR构建的函数式编程语言

## 基础语法

### 外部对象(@)

使用@表示外部对象，如：

```
@System.Console.WriteLine("Hello")
```

### 程序集($)

使用$描述程序全局作用域变量及操作入口，使用$进行代码的引导及操作入口，如：

```
$.Use($Console, @System.Console)
$Console.WriteLine("Hello")
$.Console.WriteLine("Hello")
```

### 作用域对象(#)

使用#描述局部变量，如：

```
$.Var(#name: string)
$.Set(#name, "123")
```

