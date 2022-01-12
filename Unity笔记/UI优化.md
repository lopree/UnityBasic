# 目录：
- 1. UI优化
    - 1.1. 合理分配图集
    - 1.2. Resources文件夹中只保存prefab文件
- 2. CPU优化
- 3. GPU优化
## 1. UI优化

### 1.1. 合理分配图集

- 同一个UI界面的图片尽可能放到一个图集中以降低drawcall
- 公用的图片放到一个或共享的图集中,例如:通用的弹框和按钮.<br>
相同功能的图片放到一个图集中,例如:装备图标和英雄头像;这样可以降低切换界面的加载速度
- 不同格式的图片放到不同的图集中,例如:透明(带Alpha)和不透明(不带Alpha)的图片,这样可以减少图片的存储空间和占用内存.
- 关卡内的UI资源不要与外围系统的UI资源混用;在关卡内,需要加载大量的角色及场景资源,内存紧张,一般在进入关卡时,都会手动释放外围系统的资源以便增加可用内存.如果战斗内的UI与外围共用的UI在一个图集内,释放将不会成功.对于这些共用的UI需要特殊处理.
- ***删除失效的UI节点和动画,而不是disable它们***

### 1.2. Resources文件夹中只保存prefab文件

随着项目版本的迭代,可能会导致部分资源(动画,贴图这些非Prefab文件)失效,如果这些文件放在Resource文件夹下,在打包时,unity会将Resource目录下文件全部打包成一个大的AssetBundle包(非resource目录下的文件只有在引用倒是才会被打包),从而出现包的冗余,增加不必要的存储空间和内存占用.

## 2. CPU优化

使用`Profiler`定位到性能热点,找到消耗最高的函数,然后想方法降低消耗.

- 使用尽可能少的UI元素;在制作UI时,一定要仔细检查UI层级,杀出不必要的UI元素,这样可以减少深度排序的时间以及Rebuild的时间
- 减少Rebuild的频率,将动态UI元素与静态UI元素分离.放到特定的Canvas中
- 谨慎使用UI元素的enable和disable,因为它们会触发耗时较高的rebuild,替换方案之一是enable和disableUI元素的canvasrender或者Canvas.
- 谨慎使用Text的Best Fit选项,虽然这个选项可以动态的调整字体大小以适应UI布局而不会超框，但其代价是很高的，Unity会为用到的该元素所用到的所有字号生成图元保存在atlas里，不但增加额外的生成时间，还会使得字体对应的atlas变大。
- 谨慎使用Canvas的Pixel Perfect选项，该选项会使得ui元素在发生位置变化时，造成layout Rebuild。（比如ScrollRect滚动时，如果开启了Canvas的pixel Perfect，会使得Canvas.SendWillRenderCanvas消耗较高）

## 3. GPU优化

一般来说，造成GPU性能瓶颈主要有两个原因：复杂的vertext或pixel shader计算以及overdraw造成过多的像素填充。在默认情况下UGUI中所有UI元素使用都使用UI/Defaut shader，因此在优化时可优先考虑解决Overdraw问题。Overdraw主要是因为大量UI元素的重叠引起的，查看overdraw比较简单，在scene窗口中选择overdraw模式，场景中越亮的地方表示overdraw越高。

为了降低overdraw,可以做如下优化：

禁用不可见的UI，比如当打开一个系统时如果完全挡住了另外一个系统，则可以将被遮挡住的系统禁用。
不要使用空的Image,在Unity中，RayCast使用Graphi作为基本元素来检测touch,在笔者参与的项目中，很多同学使用空的image并将alpha设置为0来接收touch事件，这样会产生不必要的overdraw。通过如下类NoDrawingRayCast来接收事件可以避免不必要的overdraw。


