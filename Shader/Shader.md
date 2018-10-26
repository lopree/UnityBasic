# 版本变化
> Unity 2017 变化
  + 使用内置的UnityObjectToViewPos(*)函数来代替mul(UNITY_MATRIX_MV, *)对顶点进行变换。
> Unity 5.X
  + 矩阵变化：_Object2World被替换成了unity_ObjectToWorld，_World2Object被替换成了unity_WorldToObject（均在UnityShaderVariables.cginc文件中被声明），_LightMatrix0被替换成了unity_WorldToLight（在AutoLight.cginc文件中被声明）。
# 渲染流程（渲染流水线）--GPU
>将三维模型渲染成二维图像。<br>
>***应用阶段(Appliction Stage)*** --> ***几何阶段(Geometry Stage)*** --> ***光栅化阶段(Rasterizer Stage)***

+ 应用阶段：CPU负责，开发者具有绝对控制权。
  + 这个阶段将**渲染图元**传递到下一个阶段，也就是模型位置，灯光，摄像机位置等场景数据需要渲染的集合信息。
+ 几何阶段：GPU负责，开发者无控制权限；将传入的渲染图元中的三维位置坐标转换为屏幕坐标以及每个顶点对应的着色器和深度值等操作，并且将参数传到下个阶段。
+ 光栅化阶段：GPU负责，开发者无控制权限；将上一阶段得到的逐顶点数据插值,然后逐像素处理渲染到屏幕上。
## 应用阶段
+ 把数据加载到显存中
  + 网格和纹理等数据：顶点的位置信息，法线方向,顶点颜色，纹理坐标等。
+ 设置渲染状态
  + 网格需要怎样渲染：使用哪个着色器。
+ 调用Draw call
  + 上面两部完成后，CPU通知GPU可以进行渲染，这个过程被称为**Draw Call**
  + Draw Call指向一个需要渲染的图元列表，不包含任何材质信息（上两步已经完成）。然后GPU开始根据顶点信息等进行渲染流程
### Draw Call
+ 大量的Draw Call导致CPU过载，CPU花费过多的时间在提交Draw Call上（GPU已经完成），从而造成性能瓶颈。
+ 可以使用 **批处理(Batching)** 减少Draw Call。批处理可以将小网格合并成一个大网格，变为一次Draw Call提交。但是即使是相同的网格，使用不同的渲染方式则不会合并为同一个。
>减少Draw Call的方法
1. 避免大量使用小的网格；当不可避免时，考虑这些网格是否可以合并
2. 避免使用过多的材质；不同网格之间可以使用相同的材质。
# Why
>Shader，整个渲染流程的子部分，可以把物体按照自己的意愿渲染到屏幕上。
# Unity Shader分类
> Unity Shader是对渲染流程的再次封装后提供的图像编程接口.
1. 固定管线着色器(Fixed Function Shader)
  + 由于大多数GPU支持可编程渲染管线,这种固定关心啊的编程方式被逐渐抛弃
2. 顶点/片段着色器(Vertex/Fragment Shader)
  + 因为适用范围广,使用CG/HLSL语言编写,被大量采用
3. 表面着色器(Surface Shader)
  + unity自己创建的一种着色代码类型,代码量少但是渲染压力大.本质上是对顶点/片段着色器的更高一级抽象,但是为我们处理了很多光照细节。
## 顶点着色器
## 片元着色器
## 详解
>在Unity中开发者使用ShaderLab编写Unity Shader。<br>
>因为表面着色器的本质依然是`顶点/片段着色器`,因此主要详细解释顶点/片段中的参数

Shader程序的基本结构:<br>
![](https://onevcat.com/assets/images/2013/shader-structure.png)

每个Shader可以有多个子着色器（Subshader）为了适应各种平台, **但是每个Subshader要尽量少的Pass。**
#### 属性
在`Properties{}`中定义着色器属性，在这里定义的属性将被`作为输入提供给所有的子着色器`。每一条属性的定义的语法是这样的：

```_Name("Display Name", Propertytype) = defaultValue[{options}]```

+ _Name: **名字**，通常由下划线开始，可以在shader中访问。
+ Display Name: **显示名字**，在材质面板中可以显示调节这个属性。
+ Propertytype: **属性类型**，每个属性都要规定类型，并赋予默认值
![](http://ov443bcri.bkt.clouddn.com/Shader%E5%B1%9E%E6%80%A7.png)

#### Pass通道
镶嵌在**CGPROGRAM**和**ENDCG**之间，这两个代表在Unity Shader中使用CG/HLSL的语法。
```
#pragma vertex name
#pragma fragment name
```
>定义顶点/片段的函数名字，一般以vert/frag代表。

# Shader语法规范
1. 数值尽可能使用精度低的类型：使用fixed存储颜色和单位矢量；使用half存储更大范围的数据；最后才是float类型。
2. 在DirectX平台上，尽量使用和变量类型相匹配的参数数目对变量进行初始化。
3. 避免在Shader（尤其是片元着色器）中进行大量的计算
4. 慎用分支和循环语句。可以将计算放在上游，例如CPU
  + 分支判断语句中使用的条件变量最好是常数
  + 每个分支中包含的操作指令数尽可能少
  + 分支的嵌套层数尽可能少
