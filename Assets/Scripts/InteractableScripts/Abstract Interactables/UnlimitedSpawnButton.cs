using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlimitedSpawnButton : Button
{
    public GameObject spawnController;
    private bool on = false;
    
    //button will inverse if the automatic spawning is on or off.
    public override void ButtonAction() {
        on = !on;
         spawnController.GetComponent<SimpleSpawnSystem>().SpawnEnabled = !spawnController.GetComponent<SimpleSpawnSystem>().SpawnEnabled;
        Debug.Log("Automatic Spawning Enabled: " + on);
    }

    
}
