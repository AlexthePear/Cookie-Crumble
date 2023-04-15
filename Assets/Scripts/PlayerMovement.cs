using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //https://www.youtube.com/watch?v=f473C43s8nE&list=PLh9SS5jRVLAleXEcDTWxBF39UjyrFc6Nb&index=7
    //Here is the link to the video that our current movement system is based off of
    //He also has other movement videos like wall run and I think wall jumping idk.
    //The link also includes the playlist with his other movement systems.
    //if we want to use a different movement system we can

    /* 
        additional notes
         -we need to add a dash system to player movement i.e. dash in any wasd direction
         -Ignorable for now but crouch is scuffing camera view since it scales every aspect of the player not just the sphere
         -slope movement is scuffed
         -I want to rework this with movement more like apex because I think that it fits this game better
         -also I want to only be able to sprint in the forward direction
         -no sprinting backwards
     */
    [Header("Keybinds")]

    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCD;
    public float airMultiplier;
    public bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool grounded;
    
    
    public Transform orientation;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState { 
        walking, 
        sprinting, 
        crouching,
        air
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        SpeedControl();
        StateHandler();

        //ground check
        grounded = GroundCheck();
        //applies drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    private void FixedUpdate()
    {
        PlayerMove();
    }
    //grabs player input and stores in variables
    public void PlayerInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded) {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCD);
        }

        // Start crouch
        //ok so the way it works rn is that it takes the
        //current scale and changes the y scale to a new scale
        // this will need to change once we have a working model.
        if (Input.GetKeyDown(crouchKey)) {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            //this line of code is because when we first shrink the y scale the player will remain floating in the air
            // this pushes the player fast into the ground.
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        //stop Crouch
        //by returning character to original scale;
        if (Input.GetKeyUp(crouchKey)) {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }
    private void StateHandler() {
        //Mode - Crouching
        if (Input.GetKey(crouchKey)) {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        //Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        //Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
            //Mode - air
        }
        else {
            state = MovementState.air;
        }

    }
    private void PlayerMove() {
        //uses the inputs to determine the direction
        //orientation to make direction relative to where it the character is facing.
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private bool GroundCheck() {
        Debug.DrawRay(transform.position, Vector3.down * playerHeight - new Vector3(0, 0.15f, 0));
        return Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.15f, ground);
    }

    private void Jump() {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() {
        readyToJump = true;
    }
}
