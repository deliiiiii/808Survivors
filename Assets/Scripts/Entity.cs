using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData")]
[Serializable]
public class EntityData : ScriptableObject
{
    public float maxHealth;
    public float moveSpeed;
    public virtual void InitializeData()
    {
        if (maxHealth <= 0)
        {
            Debug.LogError(GetType().Name + " init HP fail");
        }
    }
}


public abstract class Entity : MonoBehaviour
{
    [SerializeReference]
    protected EntityData entityData;
    
    public BuffFloat maxHealthBuff;
    public virtual void Initialize()
    {
        entityData.InitializeData();
        curHealth = entityData.maxHealth;
        maxHealthBuff = new(entityData.maxHealth);
    }
    public virtual void OnEnable()
    {
        Initialize();
    }
    public float MaxHealth => maxHealthBuff.Value;
    float curHealth;
    public float CurHealth
    {
        get
        {
            return curHealth;
        }
        set
        {
            if (value > MaxHealth)
            {
                curHealth = MaxHealth;
            }
            else if (value <= 0)
            {
                curHealth = 0f;
                OnHealthZero();
            }
            else
                curHealth = value;
        }
    }
    protected virtual void OnHealthZero()
    {
        GetComponent<ObjectPoolObject>().MyDestroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        CurHealth -= damage;
    }

    #region Move
    
    protected virtual void Move()
    {

    }
    
    protected virtual void Update()
    {
        Move();
    }
    #endregion
}
