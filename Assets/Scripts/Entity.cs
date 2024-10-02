using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Entity : MonoBehaviour
{
    public virtual void Initialize()
    {
        if(maxHealth.Value <= 0)
        {
            Debug.LogError(name + " init HP fail");
        }
        curHealth = maxHealth.Value;
    }
    public virtual void OnEnable()
    {
        Initialize();
    }
    #region HP
    [SerializeField]
    BuffFloat maxHealth;
    [SerializeField]
    float curHealth;
    public float Health
    {
        get
        {
            return curHealth;
        }
        set
        {
            curHealth = value;
            if (curHealth > maxHealth.Value)
            {
                curHealth = maxHealth.Value;
            }
            if (curHealth <= 0)
            {
                curHealth = 0f;
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
