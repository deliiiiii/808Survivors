using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SanitatorData", menuName = "ScriptableObjects/SanitatorData")]
public class SanitatorData : WeaponData
{

}
public class Sanitator : Weapon
{
    protected SanitatorData sanitatorData => substanceData as SanitatorData;
    public override void WeaponFire()
    {
        List<Enemy> enemiesInScreen = EnemyManager.Instance.GetCurEnemies(it => it.IsInScreen && it.CurHealth >= it.enemyData.maxHealth/2f);
        foreach (var enemy in enemiesInScreen)
        {
            enemy.TakeDamage(weaponData.weaponDamage);
        }
    }
}
