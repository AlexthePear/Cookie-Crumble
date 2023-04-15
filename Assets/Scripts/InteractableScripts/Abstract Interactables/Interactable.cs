using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    
    public void Interact() {
        Debug.Log("Interacted with " + this.gameObject);
        Main();
    }

    public abstract void Main();
}
