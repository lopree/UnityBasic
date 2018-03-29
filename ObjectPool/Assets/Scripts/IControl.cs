using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用户管理对象的销毁和创建
/// </summary>
public interface IControl
{
    //从对象池中取对象时的回调方法
    void Spawn();
    //销毁对象到对象池的回调方法
    void UnSpawn();
}
