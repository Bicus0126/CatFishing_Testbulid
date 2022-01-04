using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject RightChild;
    public GameObject LeftChild;
    public GameObject CeilingChild;
    TriggerCheck RightTrigger;
    TriggerCheck LeftTrigger;
    TriggerCheck CeilingTrigger;
    bool RightCheckEmpty;
    bool LeftCheckEmpty;
    bool CeilingCheckEmpty;

    private Rigidbody2D thisRigidbody2D;
    private Animator thisAnimator;
    private SpriteRenderer thisSpriteRenderer;

    
    [ReadOnlyInspecter] public float jumpSpeed = 0f;
    [ReadOnlyInspecter] public int Jumpcounter = 0;
    [ReadOnlyInspecter] public bool jump = false;
    [ReadOnlyInspecter] public bool jumpStart = false;
    [ReadOnlyInspecter] public bool onGround = false;
    public float JumpSpeedStart = 1f;
    public float JumpSpeedMax = 5f;
    public float WaitJumpInterval = 0.5f;
    public float JumpMultiplier = 1f;
    public float MoveSpeed = 10f;
    float horizontalMove = 0f;
    float horizontalDir = 0;
    private Vector2 moveDir = Vector2.zero;
    void Start()
    {
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        thisAnimator = GetComponent<Animator>();
        thisSpriteRenderer = GetComponent<SpriteRenderer>();

        RightTrigger = RightChild.GetComponent<TriggerCheck>();
        LeftTrigger = LeftChild.GetComponent<TriggerCheck>();
        CeilingTrigger = CeilingChild.GetComponent<TriggerCheck>();
        StartCoroutine(JumpCounter());
    }

    // Update is called once per frame
    void Update()
    {
        RightCheckEmpty = RightTrigger.checkEmpty;
        LeftCheckEmpty = LeftTrigger.checkEmpty;
        CeilingCheckEmpty = CeilingTrigger.checkEmpty;

        horizontalDir = Input.GetAxisRaw("Horizontal");     //Left = -1, Right = 1
        horizontalMove = 0f;

        //Debug.Log("Moving = " + (horizontalDir != 0 && (horizontalDir > 0) ? RightCheckEmpty : LeftCheckEmpty));

        if (horizontalDir != 0 && !jump && ((horizontalDir > 0) ? RightCheckEmpty : LeftCheckEmpty))
            horizontalMove = horizontalDir * MoveSpeed;

        if(Input.GetButton("Jump") && onGround)
        {
            jump = true;
        }
        if(Input.GetButtonUp("Jump") && jump)
        {
            jumpStart = true;
        }

        if (horizontalDir != 0)
        {
            thisSpriteRenderer.flipX = horizontalDir < 0;
            thisAnimator.SetFloat("Speed", 1);
        }
        else
            thisAnimator.SetFloat("Speed", 0);

        thisAnimator.SetBool("IsAir", !onGround);
        thisAnimator.SetBool("jump", jump);
        if (!onGround) thisAnimator.SetFloat("Speed_Y", thisRigidbody2D.velocity.y);
    }

    void FixedUpdate()
    {
        moveDir = thisRigidbody2D.velocity;

        if (jumpStart && onGround)
        {
            jumpSpeed = JumpSpeedStart + Jumpcounter * JumpMultiplier;
            jumpSpeed = (jumpSpeed > JumpSpeedMax)? JumpSpeedMax : jumpSpeed;
            moveDir.y = jumpSpeed;
            jump = false;
            jumpStart = false;
        }

        moveDir.x = horizontalMove;

        thisRigidbody2D.velocity = moveDir;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Cat on " + other.gameObject.name);
        onGround = true;
    }

    void OnTriggerExit2D()
    {
        onGround = false;
        jump = false;
    }

    IEnumerator JumpCounter()
    {
        WaitForSeconds JumpWait = new WaitForSeconds(WaitJumpInterval);
        Jumpcounter = 0;
        while(true)
        {
            Debug.Log("JumpCounting...");
            if (jump)
            {
                Jumpcounter++;
                yield return JumpWait;
            }
            else
            {
                Jumpcounter = 0;
                yield return new WaitUntil(() => jump);
            }
        }
    }
}
