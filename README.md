# 舒雅脚本(Suyaa.Script)

一个基于.Net Standard 2.1并与C#语言高度兼容的功能性函数式脚本语言

A functional scripting language engine based on. Net Standard 2.1 and highly compatible with the C # language

![license](https://img.shields.io/github/license/suyaas/suyaa.script)
![codeSize](https://img.shields.io/github/languages/code-size/suyaas/suyaa.script)
![lastCommit](https://img.shields.io/github/last-commit/suyaas/suyaa.script)

## 函数式脚本

脚本整体就是一个函数，但函数的参数可以是另一个函数定义，由此自由展开形成丰富的功能实现；

## 脚本示例

以下为一个计算累加并返回结果的简单脚本：

```
return(
    # while 循环 #
    let(num, 100)
    let(a, 1)
    let(sum, 0)
    while(equal(compare(a, num), -1), @(
        let(sum, !(sum + a))
        let(a, !(a + 1))
    ))
    sum
)
```

## 一、过程函数

为了能让脚本工作更为顺利，引擎中内置了一些基础函数；

### 1.1 逐句执行函数(step/@)

使用此函数可以让函数中的参数依次执行：

```
@(func1, func2, ... , fnucN)
step(func1, func2, ... , fnucN)
```

### 1.2 执行返回函数(return)

使用此函数可以让函数中的参数依次执行后返回最后一个参数的结果：

```
return(func1, func2, ... , fnucN, var)
```

## 二、值操作函数

### 2.1 赋值函数(let)

使用此函数可以让后参数的内容赋值给前函数：

```
let(var, value)
```

### 2.2 运算函数(+-*/)与四则混合运算语法糖(calculate/!)

使用此类函数可以进行数值运算并返回结果：

```
+(num1, num2, ... , numN)
-(num1, num2, ... , numN)
*(num1, num2, ... , numN)
/(num1, num2, ... , numN)
```

使用calculate/!函数定义可触发四则混合运算解析，如以下第一句和第二句脚本将会被解析为第三句：

```
calculate((a + 1) * 2)
!((a + 1) * 2)
*(+(a, 1), 2)
```

### 2.3 字符串连接函数(string/$)

使用此函数可以让函数中的参数依次拼接为一个新的字符串：

```
string(var1, var2, ... , varN)
$(var1, var2, ... , varN)
```

### 2.4 相等判断函数(equal)

使用相等判断函数可以对比两个参数值是否相等并返回结果：

```
equal(var1, var2)
```

### 2.5 比较函数(compare)

使用相等判断函数可以对比两个数字参数值，前函数较大则返回1，相同则返回0，较小则返回-1：

```
compare(var1, var2)
```

### 2.6 取反函数(not)

使用取反函数可以取参数的反结果：

```
not(var1)
```

### 2.7 同时成立函数(and)

使用同时成立函数将对比所有参数值，均为真时才返回为真：

```
and(var1, var2, ... , varN)
```

### 2.8 单一成立函数(or)

使用单一成立函数将对比所有参数值，只需有一个为真时即返回为真：

```
or(var1, var2, ... , varN)
```

## 三、控制函数

### 3.1 判断函数(if)

使用判断函数可以通过判断首参数的值来决定执行第二个参数还是第三个参数中的函数：

```
if(var, funcTrue, funcFalse)
```

### 3.2 判断循环函数(while)

使用判断循环函数可以通过循环判断首参数的值来循环执行第二个参数中的函数：

```
while(var, func)
```

### 3.3 规律循环函数(for)

使用规律循环函数可以通过设置初始值、终点值、每次变化值来循环执行参数中的函数：

```
for(var, start, end, change, func)
```

### 3.4 依次循环函数(foreach)

使用依次循环函数可以通过遍历列表/字典来循环执行参数中的函数：

```
foreach(var, list, func)
```

## 四、注释关键字(#)

使用#来定义注释或结束注释定义，如：

```
# 这是整行注释
let(a, 1)
# 这是部分注释 # let(a, 1)
```