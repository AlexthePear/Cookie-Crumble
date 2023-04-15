using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpawnSystem : MonoBehaviour
{/*
    This is gunna be my starter spawning system for the time being
    plan is to have game objects squares of spawn boxes
    and to spawn zombies in a randomly selected square in timed intervals.
    few other things that are needed for this to be working
    1. Spawn cap
    if we have a round system
    2. need amount of zombies killed  
    3. increase spawn cap after killing all zombies
    4. proceed to next round
    5. also we need to make it easy for multiple types of enemys
    
  */
    public float spawnTimer;
    public int mobcap;
    //eventually change this to an array to hold different types of enemys
    public GameObject spawnee;
    private GameObject[] enemies;
    private GameObject[] enemyRespawns;
    private bool readyToSpawn = true;
    public bool SpawnEnabled = false;
    

    void Start()
    {
        readyToSpawn = true;
        enemyRespawns = GameObject.FindGameObjectsWithTag("EnemyRespawns");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // this only spawns about 10 enemies now
        // get rid of the mobcap part if you want to test unlimted respawns;
        if (readyToSpawn && enemies.Length < mobcap && SpawnEnabled)
        {
            Spawn();
        }
    }

    
    public void Spawn()
    {
        
        GameObject spawnZone = randomSpawnZone();
        Instantiate(spawnee, GetRandomPointInBounds(spawnZone.GetComponent<Collider>().bounds, spawnZone.transform), Quaternion.identity);
        if (SpawnEnabled)
        {
            readyToSpawn = false;
            Invoke("SpawnTimerReset", spawnTimer);
        }
    }
    
    private void SpawnTimerReset()
    {
        readyToSpawn = true;
    }
    //returns a random point withing the bounds of a selected spawnbox
    private Vector3 GetRandomPointInBounds(Bounds bounds, Transform obj)
    {
        //we want our enemy to spawn at the floor and currently that floor is y = 0;


        
        //dont need a random y bound since it all spawns on the ground level anyways.
        
        float minX = bounds.size.x * -0.5f;
        //float minY = bounds.size.y * -0.5f;
        float minZ = bounds.size.z * -0.5f;

        Debug.Log(minX);
        return obj.position + (new Vector3(Random.Range(minX, -minX), 0 , Random.Range(minZ, -minZ)));
    }
    private GameObject randomSpawnZone()
    {
        //not sure how well this thing works need to check
        //unsure of how it rounds I know that it does round
        //with integers it should work like Random.Range(inclusive, exclusive);
        int x = Random.Range(0, enemyRespawns.Length);
        return enemyRespawns[x];
    }
}
