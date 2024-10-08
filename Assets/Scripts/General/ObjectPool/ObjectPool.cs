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
    [HelpBox("设置对象池父物体，不设置则在同级Sible中生成新物体作为父物体", HelpBoxType.Info)]
    //[SerializeField]
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
        // 获取当前游戏对象上实现了IResetObject接口的那个组件
        if (g.name.Contains("Enemy"))
        {
            int c = 0;
        }
        Component resetComInG = GetComponentsImplementingInterface(g);
        //将tPrefab上属于resetObject的类的组件赋给g上的resetObject
        if (resetComInG != null)
        {
            Component[] comps = tPrefab.GetComponents<Component>();
            foreach (var comInPrefab in comps)
            {
                if (comInPrefab.GetType() == resetComInG.GetType())
                {
                    //删除g上的组件resetObject
                    Component oldComp = g.GetComponent(comInPrefab.GetType());
                    CopyComponentValues(comInPrefab, oldComp);

                    //if (oldComp != null)
                    //    Destroy(oldComp);
                    //Component newComp = g.AddComponent(comInPrefab.GetType());
                    ////将tPrefab上的组件赋给g上的组件
                    //Type t = resetComInG.GetType();
                    //MemberInfo[] m = t.GetMembers();
                    break;
                }
            }
        }

        g.SetActive(true);
        return g;
    }
    

    private void CopyComponentValues(Component source, Component destination)
    {
        // 获取源组件的类型
        System.Type type = source.GetType();

        // 获取所有字段并复制
        var fields = type.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        foreach (var field in fields)
        {
            field.SetValue(destination, field.GetValue(source));
        }
        var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        foreach (var property in properties)
        {
            if (property.CanWrite) // 确保属性可写
            {
                property.SetValue(destination, property.GetValue(source));
            }
        }
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
    public Component GetComponentsImplementingInterface(GameObject g)
    {
        Component[] rets = g.GetComponents<Component>();
        foreach (var ret in rets)
        {
            Type t = ret.GetType();
            while(t != null)
            {
                Type[] interfaces = t.GetInterfaces();
                foreach (var inter in interfaces)
                {
                    if(inter.Name.Contains("Object"))
                        Debug.Log(inter.Name);
                    if (inter == typeof(IResetObject))
                    {
                        return ret;
                    }
                }
                t = t.BaseType;
            }
        }
        return null;
    }
}
