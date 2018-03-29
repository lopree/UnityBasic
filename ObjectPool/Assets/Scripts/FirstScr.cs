using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScr : MonoBehaviour
{

    #region 对象池
    List<GameObject> pools01;
    Dictionary<int, List<GameObject>> pools02;
    private static FirstScr Singlton;
    #endregion
}
