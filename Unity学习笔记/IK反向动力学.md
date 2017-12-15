# IK反向动力学
>`前进动力学`: 大多数角色动画都是通过将谷歌的关节角度旋转到预定值来实现;一个子关节的位置由它的父节点来决定;节点链末端的节点位置,是由节点链上各个节点的来决定的.

所谓的逆向动力学(IK),就是***给定末端节点的位置,从而逆向推出节点链上所有其他节点的合理位置***

在Mecanim动画系统中允许开发者通过脚本实现角色模型的逆向动力学,涉及的函数包括:
+ SetIKPosition();
+ SetIKRotation();
+ SetLookAtPositon();
+ SetIKPositionWeight();
+ SetRotationWeight();
