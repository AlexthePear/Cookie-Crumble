using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCol : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer rend;
    public float dmgTimer;
    public bool hit = false;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
            rend.material.color = Color.red;
        else
            rend.material.color = Color.white;
    }

    public void Damage(int dmg) {
        if (!hit) {
            hit = true;
            Invoke("resetHit", dmgTimer);
        }
    }

    private void resetHit()
    {
        hit = false;

    }
}
