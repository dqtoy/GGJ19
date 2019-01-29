using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public List<Tile> pipePrefabs;

    public List<Tile> specialTiles;

    public void Awake()
    {
        GameplayManager.Instance.TileFactory = this; // Set myself on GameplayManager
    }
    
    // Start is called before the first frame update
    public void SpawnRandomTile(Transform parent)
    {
        int random = UnityEngine.Random.Range(1, pipePrefabs.Count + 1);
        Instantiate(pipePrefabs[random-1], parent);
    }

    public Tile SpawnTileByName(string name)
    {
        foreach (var prefab in pipePrefabs)
        {
            if (prefab.name == name)
            {
                return Instantiate(prefab);
            }
        }
        
        foreach (var prefab in specialTiles)
        {
            if (prefab.name == name)
            {
                return Instantiate(prefab);
            }
        }

        return null;
    }
}
