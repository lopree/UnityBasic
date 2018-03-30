using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 用于管理存储各种类型的对象的对象池管理类
/// 以key_Value的形式进行存储
/// 1. 单例
/// 2. 添加对象池的方法
/// 3. 获取对象池中的对象
/// 4. 回收游戏对象
/// </summary>
public class PoolManager
{
    #region 单例
    private static PoolManager instance;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {

                instance = new PoolManager();
            }
            return instance;
        }
    }
    #endregion
}
