using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletData")]
[Serializable]
public class BulletData : SubstanceData
{
    public float flySpeed = 10f;
    public float damage;
}
public class Bullet : Substance
{
    BulletData bulletData => substanceData as BulletData;
    Weapon weapon;
    
    public void Initialize(Weapon f_weapon)
    {
        weapon = f_weapon;
        if(bulletData.damage <= 0)
        {
            Debug.LogError(name + " init damage fail");
        }
    }
    public virtual void Fire()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        Vector3 dir = (mousePos - GameManager.Player.transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.position = GameManager.Player.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        GetComponent<Rigidbody2D>().velocity = dir * bulletData.flySpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanDamage(collision.gameObject))
        {
            collision.GetComponent<Entity>().TakeDamage(bulletData.damage);
            GetComponent<ObjectPoolObject>().MyDestroy(gameObject);
        }
        if(collision.gameObject.CompareTag("Block"))
        {
            GetComponent<ObjectPoolObject>().MyDestroy(gameObject);
        }
    }

    bool CanDamage(GameObject target)
    {
        if (target.CompareTag("Enemy"))
        {
            return true;
        }
        return false;
    }
}
