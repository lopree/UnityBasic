# Shader分类
>主要参考的博客[猫都能学会的Unity Shader入门指南](https://onevcat.com/2013/07/shader-tutorial-1/)以及书籍`《Unity Shader入门精要》`

1. 固定管线着色器(Fixed Function Shader)
  + 由于大多数GPU支持可编程渲染管线,这种固定关心啊的编程方式被逐渐抛弃
2. 顶点/片段着色器(Vertex/Fragment Shader)
  + 因为适用范围广,使用CG/HLSL语言编写,被大量采用
3. 表面着色器(Surface Shader)
  + unity自己创建的一种着色代码类型,代码量少但是渲染压力大.本质上是对顶点/片段着色器的更高一级抽象

Shader程序的基本结构:<br>
![](https://onevcat.com/assets/images/2013/shader-structure.png)

## 案例
### Unity中标准表面着色器代码示例
![](http://ov443bcri.bkt.clouddn.com/%E8%A1%A8%E9%9D%A2%E7%9D%80%E8%89%B2%E5%99%A8.png)
### 顶点/片段着色器代码示例
![](http://ov443bcri.bkt.clouddn.com/%E9%A1%B6%E7%82%B9:%E7%89%87%E6%AE%B5%E7%9D%80%E8%89%B2%E5%99%A8.png)

### 详解
>因为表面着色器的本质依然是`顶点/片段着色器`,因此主要详细解释顶点/片段中的参数

#### 属性
在`Properties{}`中定义着色器属性，在这里定义的属性将被`作为输入提供给所有的子着色器`。每一条属性的定义的语法是这样的：

```_Name("Display Name", type) = defaultValue[{options}]```
