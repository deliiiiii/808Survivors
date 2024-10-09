using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
[Serializable]
public class WeaponData : SubstanceData
{
    public float fireCoolDown;
}
public class Weapon : Substance
{
    WeaponData weaponData => substanceData as WeaponData;
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
        if (Time.time - lastFireTime > weaponData.fireCoolDown)
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
