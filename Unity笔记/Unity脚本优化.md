# 脚本中的优化点

## 1.Hash动作与材质

Unity 底层代码不会使用字符串来访问 Animator、Material 和 Shader 属性。出于提高效率的考虑，所有属性名称都会被哈希转换成属性 ID，用作实际的属性名称。
在 Animator、Material 或 Shader 上使用 Set 或 Get 方法时，我们便可以利用整数值而非字符串。后者还需经过一次哈希处理，并没有整数值那么直接。

>动作

Animator中SetTrigger、SetFloat等方法来控制动画状态机。例如：m_animator.SetTrigger(“Attack”)是用来触发攻击动画。<br>
然而在这个函数内部，“Attack”字符串会被hash成一个整数。<br>
如果我们需要频繁触发攻击动画，我们可以通过Animator.StringToHash来提前进行hash，来避免每次的hash运算。

    private static readonly int s_Attack = Animator.StringToHash(“Attack”);
    m_animator.SetTrigger(s_Attack);
>材质

Material.Set…与Animator类似，Material也提供了一系列的设置方法用于改变Shader。例如：m_mat.SetFloat(“Hue”, 0.5f)是用来设置材质的名为Hue的浮点数。同样的我们可以通过Shader.PropertyToID来提前进行hash。

    private static readonly int s_Hue = Shader.PropertyToID("Hue");
    m_mat.SetFloat(s_Hue, 0.5f);

## 2. 计算距离与向量

**如果需要比较距离，而非计算距离**，用**SqrMagnitude**来替代Magnitude可以避免一次耗时的开方运算。<br>
在进行向量乘法时，有一点需要注意的是乘法的顺序，因为向量乘比较耗时，所以我们应该尽可能的减少向量乘法运算。

    // 耗时：73ms
    for (int i = 0; i < 1000000; i++)
        Vector3 c = 3 * Vector3.one * 2;

    // 耗时：45ms
    for (int i = 0; i < 1000000; i++)
        Vector3 c = 3 * 2 * Vector3.one;
可以看出上述的向量乘法的结果完全一致，但是却有显著的耗时差异，因为后者比前者少了一次向量乘法。所以，应该尽可能合并数字乘法，最后再进行向量乘。

## 3. 删除多余

+ 删除空的方法和脚本
+ 删除Debug.Log()

## 4.隐藏与激活

在场景中有大量物体频繁的激活或隐藏时，不使用SetActive()，在需要隐藏时移到屏幕外 ，显示时再移到屏幕内，即修改transform.position。还有一个方法，把需要隐藏的物体设为一个已隐藏的物体的子物体，因为父物体是未激活状态，子物体会自动隐藏，不过这种方法消耗与SetActive()差不多，不推荐使用。 
SetActive(true)消耗最大，SetActive(false)与transform.parent其次，transform.position消耗最小，占用的时间可以忽略不计。 