using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjectPool))]
public class Substance : MonoBehaviour,IResetObject
{
    public virtual void Initialize(){}
    [SerializeField]
    float destroyDistance;
    public enum DestroyType
    {
        Never,
        OutOfView,
        OutOfDistance,
    };
    [SerializeField]
    DestroyType destroyType = DestroyType.Never;
    protected virtual void Update()
    {
        TryDestroy();
    }
    void TryDestroy()
    {
        switch (destroyType)
        {
            case DestroyType.OutOfView:
                if (!IsInView())
                {
                    GetComponent<ObjectPoolObject>().MyDestroy(gameObject);
                }
                break;
            case DestroyType.OutOfDistance:
                if (!IsInDistance())
                {
                    GetComponent<ObjectPoolObject>().MyDestroy(gameObject);
                }
                break;
        }
    }

    bool IsInView()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        return viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1;
    }
    bool IsInDistance()
    {
        return Vector3.Distance(transform.position, Camera.main.transform.position) < destroyDistance;
    }
}
