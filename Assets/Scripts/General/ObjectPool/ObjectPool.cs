using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
public interface IResetObject
{
}
public class ObjectPool : MonoBehaviour
{
    GameObject tPrefab;
    //[HelpBox("对象池父物体,在同级Sible中生成新物体作为父物体", HelpBoxType.Info)]
    Transform objectPoolParent;
    int initCount = 16;
    int poolCount => AllObject.Count;
    List<GameObject> allObject;
    List<GameObject> AllObject
    {
        get
        {
            if (allObject == null)
            {
                Initialize();
            }

            return allObject;
        }
        set
        {
            allObject = value;
        }
    }
    Stack<GameObject> availableObject;
    public Stack<GameObject> AvailableObject
    {
        get
        {
            if (availableObject == null)
            {
                Initialize();
            }

            return availableObject;
        }
        set
        {
            availableObject = value;
        }
    }
    public void Initialize()
    {
        allObject = new();
        availableObject = new();
        tPrefab = gameObject;
        //if(objectPoolParent == null)
        {
            GameObject g = transform.parent.Find(transform.parent.name + "Pool")?.gameObject;
            if (g == null)
            {
                g = new GameObject(transform.parent.name + "Pool");
                g.transform.parent = transform.parent;
                g.transform.SetSiblingIndex(0);
            }
            objectPoolParent = g.transform;
        }
        MyCreateNew(poolCount);
    }
    void MyCreateNew(int newCount)
    {
        for (int i = 0; i < newCount; i++)
        {
            GameObject g = Instantiate(tPrefab, Vector3.zero, Quaternion.identity, objectPoolParent);
            g.SetActive(false);
            g.AddComponent<ObjectPoolObject>();
            g.GetComponent<ObjectPoolObject>().pool = this;
            AllObject.Add(g);
            AvailableObject.Push(g);
        }
    }
    public GameObject MyInstantiate()
    {
        return MyInstantiate(Vector3.zero, Quaternion.identity);
    }
    public GameObject MyInstantiate(Vector3 fPos, Quaternion fRot, Transform fParent = null)
    {
        if (AvailableObject.Count == 0)
        {
            MyCreateNew((int)(poolCount == 0 ? initCount : poolCount * 0.5f));
        }
        GameObject g = AvailableObject.Pop();
        g.transform.SetPositionAndRotation(fPos, fRot);
        if (fParent != null)
            g.transform.parent = fParent;
        else
            g.transform.parent = objectPoolParent;

        g.SetActive(true);
        return g;
    }
    public void MyDestroy(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("MyDestroy null " + tPrefab.name + " !");
            return;
        }
        obj.SetActive(false);
        AvailableObject.Push(obj);
    }
    public void MyDestroyAll()
    {
        foreach (var obj in AllObject)
        {
            if (!obj.activeSelf)
                continue;
            obj.SetActive(false);
            AvailableObject.Push(obj);
        }
    }
}
