using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Substance
{
    [SerializeField]
    float fireCoolDown;
    [SerializeField]
    float lastFireTime;
    Bullet bullet;
    Bullet Bullet
    {
        get
        {
            if(bullet == null)
            {
                if(!transform.Find("Bullet"))
                {
                    Debug.LogError(name + " has No Bullet");
                    return null;
                }    
                return bullet = transform.Find("Bullet").GetComponent<Bullet>();
            }
            return bullet;
        }
        set{
            bullet = value;
        }
    }

    

    public override void Initialize()
    {
        Bullet.Initialize(this);
    }
    public virtual void TryFire()
    {
        if (Time.time - lastFireTime > fireCoolDown)
        {
            Fire();
        }
    }
    public virtual void Fire()
    {
        lastFireTime = Time.time;
        GameObject g = Bullet.GetComponent<ObjectPool>().MyInstantiate();
        g.GetComponent<Bullet>().Fire();
    }

    
}
