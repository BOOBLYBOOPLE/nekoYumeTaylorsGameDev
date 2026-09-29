using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [Header("Moving")]
    public float holder;
    [SerializeField, Range(0f, 100f)] public float maxSpeed = 6f;
    [SerializeField, Range(0f, 100f)] public float maxAcceleration = 2f;
    [SerializeField, Range(0f, 100f)] public float maxAirAcceleration = 1f;
    private float maxSpeedChange;
    private float acceleration;
    public Animator anim;

    [Header("Flying")]
    [SerializeField, Range(0f, 100f)] public float flySpeed = 9f;
    [SerializeField, Range(0f, 100f)] public float flyAcceleration = 3f;
    [SerializeField, Range(0f, 4f)] public float rate;
    private Vector2 flyVelocity;
    public static bool active;

    [Header("Physics")]
    public Vector2 dir;
    public GameObject feet;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D rb;
    private Ground ground;
    private bool onGround;
    private bool onSlope;

    [Header("Jumping")]
    [SerializeField, Range(0f, 10f)] public float jumpHeight = 10f;
    [SerializeField, Range(0, 5)] public int maxAirJumps;
    [SerializeField, Range(0f, 5f)] public float downwardMovement = 4f;
    [SerializeField, Range(0f, 5f)] public float upwardMovement = 2f;
    [SerializeField, Range(0f, 0.5f)] private float coyoteTime = 0.25f;
    [SerializeField, Range(0f, 0.3f)] private float jumpBufferTime = 0.1f;
    private int jumpPhase;
    private float defaultGravityScale;
    private float coyoteCounter;
    private float jumpBufferCounter;
    private bool desiredJump;
    private bool isFlying;
    private bool isJumping;

    public Sprite fly;
    public Sprite flying;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        anim = GetComponent<Animator>();
        isFlying = false;
        active = false;
        
        //init jump vars
        jumpBufferCounter = 0;
        coyoteCounter = 0;
        jumpPhase = 0;
        holder = maxSpeed;
        defaultGravityScale = 1f;
    }
    void Update()
    {
        //input
        dir.x = input.RetrieveMoveInput();
        desiredJump |= input.RetrieveJumpInput();
        if (input.RetrieveModeSwitch())
        {
            if (!isFlying)
                isFlying = true;
            else if (isFlying)
            {
                isFlying = false;
                desiredJump = false;
            }
        }
        //velocity
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Image>().sprite = isFlying ? flying : fly;
        desiredVelocity = new Vector2(dir.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);
        anim.SetBool("isJump", onGround ? false : true);
    }
    private void FixedUpdate()
    {
        inMotion();
        upChecks();
        StatsController.energy -= 2 * (Time.deltaTime * 1.75f);
        rb.linearVelocity = velocity;
    }
    public void inMotion()
    {
        onGround = ground.GetOnGround();
        onSlope = ground.GetOnSlope();
        velocity = rb.linearVelocity;
 
        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange += acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        gameObject.GetComponent<SpriteRenderer>().flipX = dir.x >= 0f ? false : true;
        anim.SetBool("isrun", dir.x != 0f ? true : false);
    }
    public void flyAction()
    {
        StatsController.energy -= rate * (Time.deltaTime * 1.9f);
        rb.gravityScale = -1;
        flyAcceleration = flyAcceleration + Time.deltaTime / 10;
        float flySpeedChange = flyAcceleration * Time.deltaTime;
        flyVelocity = new Vector2(0f, 1f) * Mathf.Max(flySpeed, 0f);
        velocity.y = Mathf.MoveTowards(velocity.y, flyVelocity.y, flySpeedChange);
    }
    public void jumpAction()
    {
        rb.gravityScale = 0f;
        rb.linearVelocityY = 0f;
        if (coyoteCounter > 0f || jumpPhase < maxAirJumps && isJumping)
        {
            jumpPhase += 1;
            jumpBufferCounter = 0;
            coyoteCounter = 0;
            isJumping = true;

            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            if(velocity.y > 0f)
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            velocity.y += jumpSpeed;
        }
    }
    public void upChecks()
    {
        if (!isFlying)
        {
            //if grounded & falling coyote counter to coyote time or else coundown
            if (onGround && rb.linearVelocityY <= 0)
            {
                jumpPhase = 0;
                coyoteCounter = coyoteTime;
                velocity.y = 0f;
            }
            else
                coyoteCounter -= Time.deltaTime;

            if (desiredJump)
            {
                SoundController.instance.PlayJump();
                onGround = false;
                onSlope = false;
                isJumping = true;
                desiredJump = false;
                jumpBufferCounter = jumpBufferTime;
            }
        }
        if (rb.linearVelocityY < -4f)
            rb.linearVelocityY = -4f;
        //spagetthi inputs
        //when not pressing space
        if(!desiredJump && jumpBufferCounter > 0)
            jumpBufferCounter -= Time.deltaTime;
        //buffers when spam space
        if (jumpBufferCounter > 0 && !isFlying) 
            jumpAction();
        //CHANGE TO COOLDOWN
        if (isFlying && input.RetrieveJumpHoldInput() && StatsController.energy > 0)
        { 
            flyAction();
            rb.gravityScale = 0f;
        }
        if (input.RetrieveJumpHoldInput() && rb.linearVelocityY > 0f && !isFlying)
        {
            rb.gravityScale = upwardMovement;
        }

        else if (!input.RetrieveJumpHoldInput() && !onGround)
            rb.gravityScale = downwardMovement;
        else if (onGround)
            rb.gravityScale = 0;
        else if (rb.linearVelocityY == 0f)
            rb.gravityScale = defaultGravityScale;
    }
}
