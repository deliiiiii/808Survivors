using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField]
    List<Tile> mapTiles = new();
    List<Vector2> generatedTiles = new();
    Vector2 screenSize;
    public void Initialize()
    {
        mapTiles.Clear();
        generatedTiles.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            mapTiles.Add(transform.GetChild(i).GetComponent<Tile>());
        }
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    public void ClearAllTiles()
    {
        foreach (var tile in mapTiles)
        {
            tile.GetComponent<ObjectPool>().MyDestroyAll();
        }
        generatedTiles.Clear();
    }
    private void Update()
    {
        TryGenerateTile();
    }
    public void TryGenerateTile()
    {
        //在周围5x5的网格中生成地图块
        Vector2Int playerGridIndex = GetPlayerGridIndex();
        for (int i = playerGridIndex.x - 2; i <= playerGridIndex.x + 2; i++)
        {
            for (int j = playerGridIndex.y - 2; j <= playerGridIndex.y + 2; j++)
            {
                Vector2Int gridIndex = new (i, j);
                if (generatedTiles.Contains(gridIndex))
                    continue;
                generatedTiles.Add(gridIndex);
                foreach(var tile in mapTiles)
                {
                    for(int k=0;k< tile.CountInGrid; k++)
                    {
                        Vector2 position = new(i * screenSize.x + Random.Range(-screenSize.x / 2, screenSize.x / 2), j * screenSize.y + Random.Range(-screenSize.y / 2, screenSize.y / 2));
                        tile.GetComponent<ObjectPool>().MyInstantiate(position, Quaternion.identity);
                    }
                }
            }
        }
    }
    Vector2Int GetPlayerGridIndex()
    {
        Vector2 playerPosition = GameManager.Player.transform.position;
        int gridX = Mathf.FloorToInt(playerPosition.x / screenSize.x);
        int gridY = Mathf.FloorToInt(playerPosition.y / screenSize.y);
        return new Vector2Int(gridX, gridY);
    }
}