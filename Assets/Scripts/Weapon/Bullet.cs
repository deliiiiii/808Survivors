using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Substance
{
    Weapon weapon;
    [SerializeField]
    float flySpeed = 10f;
    [SerializeField]
    float damage;
    public void Initialize(Weapon f_weapon)
    {
        weapon = f_weapon;
        if(damage <= 0)
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
        GetComponent<Rigidbody2D>().velocity = dir * flySpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanDamage(collision.gameObject))
        {
            collision.GetComponent<Entity>().TakeDamage(damage);
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
