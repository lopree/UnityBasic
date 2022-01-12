# Unity与Winform的结合使用
> 由于Unity5.0之后放弃支持webplayer,在集成WinForm和Unity之间有以下两种思路:

***

## 1. 将winform的窗体打包成`动态链接库`,然后在unity中调用自己封装好的dll.(现使用)
>  使用Winform做好界面,然后打包`动态链接库(也就是常用的例如Litjson的C#封装的dll文件)`,导入到unity中调用.关于Winform和Unity之间的通信可以使用`委托`,或者是winform中引用unityEngine.dll后直接使用`sendmessage`这个方法.

关于win32dll的特点:<br>
引用百度百科的话来说:[DLL文件百科地址](https://baike.baidu.com/item/DLL%E6%96%87%E4%BB%B6)<br>
Win32 DLL与 Win16 DLL有很大的区别，这主要是由操作系统的设计思想决定的。一方面，在Win16 DLL中程序入口点函数和出口点函数（LibMain和WEP）是分别实现的；而在Win32 DLL中却由同一函数DLLMain来实现。无论何时，当一个进程或线程载入和卸载DLL时，都要调用该函数

涉及到的Untiy插件开发知识:
1. 托管插件（Managed plugins）
  + 通常情况下，我们直接使用脚本实现相关功能，然后由Unity编译成目标可执行文件。但有 时我们想在外部把脚本编译成DLL，然后放在Unity中使用。这个DLL就是这里所说的托管式 插件，兼容.NET二进制。
2. 原生插件
  + 原生插件一般采用C,C++,Objective-C等等编写，Unity允许游戏代码来访问这些原生插件中的函数， 允许Unity和一些中间件库或者已有的C/C++进行整合和。

## 2. 将winform嵌入到unity打包的exe中(推荐)
>这种方法的优势就是展现层面使得3D场景和业务系统界面不在孤立,并且winform的开发与unity湘湖独立,只需要提前约定好通信协议.<br>
通信方式:`win32窗口句柄`,`内存共享`,`socket通信`<br>
采用`socket`方案解决两者之间的通信问题,因为socket使用广泛.其中winfor作为服务器,unity周围客户端<br>
**嵌入只是形式,通信才是核心**
