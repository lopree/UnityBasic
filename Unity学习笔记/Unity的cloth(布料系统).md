# cloth组件属性介绍
![面板截图](http://img.voidcn.com/vcimg/000/003/063/344_ce3_4b5.jpg)

|名称|作用|
|:--|:--|
|Stretching Stiffness|拉扯硬度,设置布料拉伸程度，取值在0到1之间，值越大越不容易拉伸
|Bending Stiffness|弯曲硬度
|Use Tethers|默认开启, 用于方式过度拉伸
|Use Gravity|是否使用世界重力
|Damping|阻尼会应用于每个布料顶点. 要想打造看上去抖动更小的布料, 可以试试这个
|External Acceleration|常量外力
|Random Acceleration|随机外力
|World Velocity Scale|与World Acceleration Scale共同组成布料的GameObject.transfrom的运动会对物理模拟造成的影响比例
|World Acceleration Scale|与World Velocity Scale共同组成布料的GameObject.transfrom的运动会对物理模拟造成的影响比例
|Friction|当布料碰到在这个列表中存在的Collider时所产生的摩擦力, 这只会影响布料的模拟. 上面说过了布料的物理模拟是单向的
|Collision Mass Scale|
|Use Continuous Collision|使用Continuous Collision, 增加消耗, 减少直接穿透碰撞的几率
|Use Virtual Particles|Add one virtual particle per triangle to improve collision stability
|Solver Frequency|Number of solver iterations per second. 显然是一个优化参数, 默认120很高了
|Sleep Threshold|静止阈值
|Capsule Colliders|要对布料产生交互的胶囊碰撞体
|Sphere Colliders|要对布料产生交互的ClothSphereColliderPairs. 可以理解为他是按照一组来的, 一组中可以只有一个SphereCollider, 也可以有两个, 当有两个的时候, 那么这两个SphereCollider会在布料的碰撞系统中被”焊接”起来. 这样就允许通过两个大小不同的SphereCollider来组合成一个圆锥形状的碰撞体了
