using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolObject : MonoBehaviour
{
    [HideInInspector]
    public ObjectPool pool;
    public void MyDestroy(GameObject obj)
    {
        pool.MyDestroy(obj);
    }
}
