using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float anglePerSecond; //How many angles we want the player to be able to rotate per second.
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public PlayerAudioClips AudioClips;
    public float FootstepInterval = 0.5f;

    AudioPlayer AudioPlayer;

    AudioPlayHandle RunLoopHandle;
    AudioPlayHandle FlameLoopHandle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        readyToJump = true;

        AudioPlayer = GetComponent<AudioPlayer>();
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        HandleAudio();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;



        // on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        // in air
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        // Veranish code to fix random rotations from small values
        if (moveDirection.sqrMagnitude > 0.0001f)
        {
            moveDirection.y = 0;
            //transform.rotation = Quaternion.LookRotation(moveDirection); // instant snap
            
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, anglePerSecond * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            
        }

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void HandleAudio()
    {
        if (FlameLoopHandle == null || !FlameLoopHandle.IsPlaying)
        {
            FlameLoopHandle = AudioPlayer.PlaySound(AudioClips.StaffFire, 0.25f, true);
        }

        if (Input.GetButton("Jump"))
        {
            FlameLoopHandle.EndLoop();
        }

        Vector3 VelocityXZ = rb.velocity;
        VelocityXZ.y = 0;

        if (VelocityXZ.sqrMagnitude > 1 && grounded)
        {
            if (RunLoopHandle == null || !RunLoopHandle.IsPlaying)
            {
                RunLoopHandle = AudioPlayer.PlaySoundAtInterval(AudioClips.RunningClip, FootstepInterval, 0.25f);
            }
        }
        else if (RunLoopHandle != null && RunLoopHandle.IsPlaying)
        {
            RunLoopHandle.Stop();
        }
    }
}