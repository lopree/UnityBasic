# Canvas 与 EventSystem
>Canva是一个画布,就像我们绘图用的图纸一样,所有控件必须在Canvas中才能绘制出来,在其他地方则不能被绘制

## Canvas的基本属性

![主界面]( http://img.blog.csdn.net/20160913232126692)

### Canvas组件的渲染模式
#### Render Mode 组件:
>属性设置的画布的渲染模式,其中有三种渲染模式:

 1. Screen Space - OverLay: 这种模式下是直接在屏幕上渲染显示画布的内容,<br>即使画布不在摄像机范围内,
在这种模式下内部是继承了一个摄像机的,但我们无法操控这个摄像机<br>在不同的屏幕分辨率下画布会自动适配屏幕的分辨率大小.<br> 例如,当禁用摄像机时,图像依然显示在屏幕上<br>
![摄像机禁用](http://img.blog.csdn.net/20160913234928810)
 2. Screen Space - Camera:这种模式下,是将画布放置在距离摄像机一定距离的事业中,画布的内容都是通过摄像机来绘制,此时画布会跟随着摄像机运动.当摄像机被禁用时画布也就不会显示出来<br>
这种模式下当摄像机事业大小改变或者屏幕大小改变,画布也会自动去适配<br>
![两个属性](http://img.blog.csdn.net/20160914000812943)<br>
RenderCamera:设置选用的摄像机<br>
Plane Distance: 设置摄像机距离画布的距离<br>
在这种模式下我们可以在画布与摄像机之间添加3D模型,或者3D特效
 3. World Space 模式:这种模式下画布会被当做世界空间中的一个模型来处理,它不会跟随摄像机的移动,超出摄像机事业则不会再被显示出来,这种模式下可以手动的设置画布的位置,以及画布的大小,画布不会自动适配
#### Convas Scaler组件:
>用于设置处于不同组件下Canvas画布中的元素缩放模式<br>
>UI Scaler Mode: 设置UI的缩放模式:

   ![截图](http://img.blog.csdn.net/20160916122625311)
1. Constant Pixel Size: 无论处于什么分辨率Canvas下的UI控件都会保持原来大小不变
2. Scale With Screen Size ：在当前模式下，Canvas画布下的UI控件会随着不同的分辨率而进行一定的缩放，以达到合理的大小。<br>
在这种模式下有一个选项：

      ![案例](http://img.blog.csdn.net/20160916151636165)
3. Reference Resolution ：设置当前窗口的分辨率，通常设置自己需要运行游戏的分辨率。
4. Match : 设置缩放的方向比例，当值为零的时候则只在宽度改变是进行缩放，当为1时则只在改变高度的时候进行缩放。
#### Graphic Raycaster组件：
>射线检测组件，其作用是用于获取用户选中的uGUI控件，当禁用这个组件的时候用户在无法获取到控件对控件进行操作，其属性为：

      ![](http://img.blog.csdn.net/20161129222642294)

1.Ignore Reversed Graphics ：是否忽略控件的正面和反面方向，都接受射线的检测，勾选则是。

2.Blocking Objects：屏蔽指定类型的(物理)对象，使它们不参与射线检测。渲染模式不为ScreenSpaceOverlay时起作用。

可选值为：
+ None：不屏蔽任何物理对象
+ Two D：屏蔽2D物理对象(即具有2D碰撞体的对象)
+ Three D：屏蔽3D物理对象(即具有3D碰撞体的对象)
+ All：屏蔽所有物体对象

3.Blocking Mask：使屏蔽对象中的指定层不参与射线检测。渲染模式不为ScreenSpaceOverlay时，且Blocking Objects不为None时起作用。
