using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SubstanceData", menuName = "ScriptableObjects/SubstanceData")]
[Serializable]
public class SubstanceData:ScriptableObject
{
    public enum DestroyType
    {
        Never,
        OutOfView,
        OutOfDistance,
    };
    public DestroyType destroyType = DestroyType.Never;
    public float destroyDistance;
}

[RequireComponent(typeof(ObjectPool))]
public class Substance : MonoBehaviour
{
    public SubstanceData substanceData;
    public virtual void Initialize(){}
    
    [SerializeField]
    
    protected virtual void Update()
    {
        TryDestroy();
    }
    void TryDestroy()
    {
        switch (substanceData.destroyType)
        {
            case SubstanceData.DestroyType.OutOfView:
                if (!IsInView())
                {
                    GetComponent<ObjectPoolObject>().MyDestroy(gameObject);
                }
                break;
            case SubstanceData.DestroyType.OutOfDistance:
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
        return Vector3.Distance(transform.position, Camera.main.transform.position) < substanceData.destroyDistance;
    }
}
