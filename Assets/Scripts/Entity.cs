using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public virtual void Initialize()
    {
        if(health <= 0)
        {
            Debug.LogError(name + " init HP fail");
        }
    }
    #region HP
    [SerializeField]
    float health;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health <= 0)
            {
                health = 0f;
                OnHealthZero();
            }
        }
    }
    protected virtual void OnHealthZero()
    {
        GetComponent<ObjectPoolObject>().MyDestroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
    #endregion
    #region Move
    [SerializeField]
    protected float moveSpeed;
    protected virtual void Move()
    {

    }
    
    protected virtual void Update()
    {
        Move();
    }
    #endregion
}
