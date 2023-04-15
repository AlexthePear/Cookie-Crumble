using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnButton : Button
{
    public GameObject spawnController;

    
    //spawns a single enemy
    public override void ButtonAction()
    {
        spawnController.GetComponent<SimpleSpawnSystem>().Spawn();
        Debug.Log("Enemy Spawned");
    }
}
