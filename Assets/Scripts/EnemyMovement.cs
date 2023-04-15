using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //commenting was old code before navMesh so probably gunna delete after some more testing with navMesh
    //public float moveSpeed, groundDrag;


    //public Transform orientation;
    //for time being gunna manually input the player until we start work on multiplayer in which case 
    //public GameObject Player;

    //for unity's built in pathfinding system.
    [SerializeField] private Transform targetPositionTransform;
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    Rigidbody rb;

    //Vector3 moveDirection;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        targetPositionTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        navMeshAgent.destination = targetPositionTransform.transform.position;


        //SpeedControl();
        //playerDetection();
        //rb.drag = groundDrag;
    }
    /*
    private void FixedUpdate()
    {
        //EnemyMove();
    }

    //until we have animations we have models and animations we can just have the enemy model to always face the player and move forward towards the player.

    // once obstacles are added pathfinding will need to be installed.
    
    private void EnemyMove() {
        transform.LookAt(Player.transform);
        orientation.LookAt(Player.transform);

        moveDirection = orientation.forward;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    //This will be needed for multiplayer to find the location of the nearest player. We will probably need pathfinding at this point so it will go after the player with the shortest path.
    //can also include things like zombie agression so that a zombie will try to attack the player that shot it last.
    private void playerDetection(){ 
    
    }
    */
}
