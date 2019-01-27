using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeFactory : Singleton<PipeFactory>
{
    public List<PipeSection> pipePrefabs;
    
    // Start is called before the first frame update
    public void SpawnRandomPipe(Transform parent)
    {
        int random = UnityEngine.Random.Range(1, pipePrefabs.Count);
        Instantiate(pipePrefabs[random-1], parent);
    }
}
