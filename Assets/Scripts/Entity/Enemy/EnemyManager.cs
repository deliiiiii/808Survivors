using Services;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField,SerializeReference]
    List<Enemy> enemies = new();
    [SerializeReference]public List<EntityData> datas = new();
    
    public void Initialize()
    {
        enemies.Clear();
        for (int i=0;i<transform.childCount;i++)
        {
            if (!transform.GetChild(i).GetComponent<Enemy>())
                continue;
            enemies.Add(transform.GetChild(i).GetComponent<Enemy>());
        }
        //datas.Add(new EntityData());
        //datas.Add(new EnemyData());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 playerPos = GameManager.Player.transform.position;
            Vector3 randomPos = playerPos + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
            enemies[0].GetComponent<ObjectPool>().MyInstantiate(randomPos,Quaternion.identity);

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            JsonTool.SaveAsJson(enemies[0], "eeenemy_0.json", JsonTool.PolyMorphicSettings);
            //JsonTool.SaveAsJson(datas, "eeenemyData.json", JsonTool.PolyMorphicSettings);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            enemies[0] = JsonTool.LoadFromJson<Enemy>("eeenemy_0.json", JsonTool.PolyMorphicSettings);
            //datas = JsonTool.LoadFromJson<List<EntityData>>("eeenemyData.json", JsonTool.PolyMorphicSettings);

        }
        //if(Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    //  enemies[0].entityData = JsonTool.LoadFromJson<EntityData>("eeenemyData.json", JsonTool.PolyMorphicSettings);
        //    var data = (EnemyData)datas[1];
        //    Debug.Log(data.attachDamage);
        //}
    }
}
