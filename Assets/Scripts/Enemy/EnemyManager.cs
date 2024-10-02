using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    List<Enemy> enemies = new();
    public void Initialize()
    {
        enemies.Clear();
        for (int i=0;i<transform.childCount;i++)
        {
            if (!transform.GetChild(i).GetComponent<Enemy>())
                continue;
            enemies.Add(transform.GetChild(i).GetComponent<Enemy>());
        }
        foreach(var enemy in enemies)
        {
            enemy.Initialize();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 playerPos = GameManager.Player.transform.position;
            Vector3 randomPos = playerPos + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
            enemies[0].GetComponent<ObjectPool>().MyInstantiate(randomPos,Quaternion.identity);

        }
    }
}
