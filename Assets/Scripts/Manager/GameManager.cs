using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;
    public Player Player
    {
        get
        {
            if(player == null)
            {
                player = FindObjectOfType<Player>();
            }
            return player;
        }
    }

    private void Awake()
    {
        EnemyManager.Instance.Initialize();
        WeaponManager.Instance.Initialize();
    }
}
