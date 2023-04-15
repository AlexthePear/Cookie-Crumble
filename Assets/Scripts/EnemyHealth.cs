using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    //since our current movement relies on navMesh to disable all movement we simply
    //have to disable the navMeshAgent component when our health depletes
    
    [SerializeField] private int health;
    
    public Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    private bool once = true;
    private Renderer rend;
    
   

    //Time before his body gets deleted
    public float deathTime;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (this.CompareTag("Target"))
            rend = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && once) {
            navMeshAgent.enabled = false;
            rb.freezeRotation = false;
            this.gameObject.layer = 0;
            float ranX = Random.Range(-1, 1);
            float ranZ = Random.Range(-1, 1);
            
            Vector3 deathDir = new Vector3(ranX, 0, ranZ);
            rb.AddForce(deathDir, ForceMode.Impulse);
            Invoke("Death", deathTime);
            once = false;
        }
    }

    public void Damage(int dmg) {
        health = health - dmg;
        Debug.Log("Enemy Health After Shot: " + health);
        
        
    }

    private void Death()
    {
        Destroy(gameObject);

    }

    //this is only for targets
    //targets will turn red when hit to make sure that weapons are working properly
    
}
