13.问题可能有：
	1.CPU的drawcall太多，指令传递速度跟不上。
	2.模型顶点太多，面数太多。
	3.后处理计算复杂度过高，延迟大。
	4.从内存访问数据延迟过高。
	5.使用太多if switch等分支语句，使用太多for循环语句。
	6.因为绘制着色了深度测试后裁剪的片元导致性能浪费。
优化方法：
1减少CPU和GPU的数据变换
合批（Batch）
减少顶点数、三角形数
视锥裁剪
BVH
Portal
BSP
OSP
避免每帧提交Buffer数据
CPU版的粒子、动画会每帧修改、提交数据，可移至GPU端。
减少渲染状态设置和查询
例如：glGetUniformLocation会从GPU内存查询状态，耗费很多时间周期。
避免每帧设置、查询渲染状态，可在初始化时缓存状态。
启用GPU Instance
开启LOD
避免从显存读数据
2减少过绘制
避免Tex Kill操作
避免Alpha Test
避免Alpha Blend
开启深度测试
Early-Z
层次Z缓冲（Hierarchical Z-Buffering，HZB）
开启裁剪：
背面裁剪
遮挡裁剪
视口裁剪
剪切矩形（scissor rectangle）
控制物体数量
粒子数量多且面积小，由于像素块机制，会加剧过绘制情况
植物、沙石、毛发等也如此
3Shader优化
避免if、switch分支语句
避免for循环语句，特别是循环次数可变的
减少纹理采样次数
禁用clip或discard操作
减少复杂数学函数调用
