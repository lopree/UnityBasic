using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用于管理相同类型的对象
/// 1. 从对象池拿一个游戏对象
/// 2. 回收某一个游戏对象
/// 3. 回收所有的游戏对象
/// 4. 检查某个对象是否在对象池中
/// </summary>
public class FirstScr
{

    //存放对象的集合
    List<GameObject> pool = new List<GameObject>();

    //要创建的物体的预设体
    private GameObject prefab;

    //自定义构造方法
    public FirstScr(GameObject obj)
    {
        prefab = obj;
    }

    //返回预设体的名字，定义预设体的名字与对象池名字一致，为了方便在池子管理类中找对应的池子
    public string Objectname
    {
        get
        {
            return prefab.name;
        }

    }

    #region 1.从对象池里拿一个对象
    public GameObject SubPoolSpawn()
    {
        GameObject obj = null;
        //
        foreach (GameObject item in pool)
        {
            if (item.activeSelf == false)
            {
                obj = item;
                break;
            }
        }

        if (obj == null)
        {
            obj = GameObject.Instantiate(prefab);
            pool.Add(obj);
        }

        obj.SetActive(true);
        IControl control = obj.GetComponent<IControl>();
        if (control != null)
        {
            control.Spawn();
        }

        return obj;
    }
    #endregion

    #region 2.回收某一个对象
    public void SubPoolUnSpawn(GameObject obj)
    {
        IControl control = obj.GetComponent<IControl>();
        if (control != null)
        {
            control.UnSpawn();
        }
        obj.SetActive(false);
    }
    #endregion

    #region 3.回收所有的

    public void SubPoolUnSpawnAll()
    {
        foreach (GameObject item in pool)
        {
            if (item.activeSelf)
            {
                SubPoolUnSpawn(item);
            }
        }
    }
    #endregion

    #region 检查某个游戏对象是否在对象池中
    public bool SubPoolContains(GameObject obj)
    {
        return pool.Contains(obj);
    }
    #endregion
}


