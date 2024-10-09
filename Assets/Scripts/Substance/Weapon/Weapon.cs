using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
[Serializable]
public class WeaponData : SubstanceData
{
    public float fireCoolDown;
    public bool hasBullet;
    public float weaponDamage;
}
public class Weapon : Substance
{
    protected WeaponData weaponData => substanceData as WeaponData;
    [SerializeField]
    float lastFireTime;
    Bullet bullet;
    Bullet Bullet
    {
        get
        {
            if(bullet == null)
            {
                if(!transform.Find("Bullet") && weaponData.hasBullet)
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
        base.Initialize();
        if(weaponData.hasBullet)
            Bullet.Initialize(this);
    }
    #region Fire
    public void TryFire()
    {
        if (Time.time - lastFireTime > weaponData.fireCoolDown)
        {
            Fire();
        }
    }
    public void Fire()
    {
        lastFireTime = Time.time;
        if (weaponData.hasBullet)
            BulletFire();
        else
            WeaponFire();
    }
    
    public virtual void BulletFire()
    {
        GameObject g = Bullet.GetComponent<ObjectPool>().MyInstantiate();
        g.GetComponent<Bullet>().Fire();
    }
    public virtual void WeaponFire()
    {
        
    }
    #endregion
}
