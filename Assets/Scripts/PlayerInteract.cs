using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    public KeyCode interactKey = KeyCode.F;
    public float interactRange;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused) { 
        Interactable inter = getInteractables();
        if (Input.GetKeyDown(interactKey) && inter != null)
        {
            if (IsTargetVisible(cam, inter.gameObject))
            {
                inter.Interact();
            }
        }
        }
    }
    public Interactable getInteractables() {
        
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
               // Debug.Log(collider);
                if (collider.TryGetComponent(out Interactable interactable)) {
                    return interactable;
                   
                }
            }
        return null;

        
    }

    //Apparently this checks if the object is visible by the camera
    //I have no idea how it actually works though cuz I copied of the internet
    //I did this to add percision to an interact button
    //so that it is impossible to hit a button while looking at it from behind
    //if we want even more precision we can use a rayCast instead
    //this way the player has to be directly aiming at the object they want to interact with
    //we can also include different types of precision needed by the interactable.
    bool IsTargetVisible(Camera c, GameObject go)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = go.transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }
}


