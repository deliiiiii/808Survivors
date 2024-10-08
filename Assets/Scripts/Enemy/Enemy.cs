using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
[Serializable] 
public class EnemyData : EntityData
{
    public float attachDamage = 0.1f;
    public override void InitializeData()
    {
        base.InitializeData();
        if (attachDamage <= 0)
        {
            Debug.LogError(GetType().Name + " init attachDamage fail");
        }
    }
}

[RequireComponent(typeof(ObjectPool))]
public class Enemy : Entity, IResetObject
{
    public override void Initialize()
    {
        base.Initialize();
    }
    protected override void Move()
    {
        Vector3 direction = (GameManager.Player.transform.position - transform.position).normalized;
        transform.position += entityData.moveSpeed * Time.deltaTime * direction;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Entity>().TakeDamage(((EnemyData)entityData).attachDamage);
        }
    }
}
