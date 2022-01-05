using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    GameObject Canvas;
    GameObject PowerBar;
    Slider PBar;
    [ReadOnlyInspecter] public GameObject FillObject;
    Image FillImage;
    
    public GameObject RightChild;
    public GameObject LeftChild;
    public GameObject GroundChild;
    TriggerCheck RightTrigger;
    TriggerCheck LeftTrigger;
    TriggerCheck GroundTrigger;
    bool RightCheckEmpty;
    bool LeftCheckEmpty;

    private Rigidbody2D thisRigidbody2D;
    private Animator thisAnimator;
    private SpriteRenderer thisSpriteRenderer;

    
    [ReadOnlyInspecter] public float jumpSpeed = 0f;
    [ReadOnlyInspecter] public float Jumpcounter = 0;
    [ReadOnlyInspecter] public bool jump = false;
    [ReadOnlyInspecter] public bool jumpStart = false;
    [ReadOnlyInspecter] public bool onGround = false;
    public float JumpSpeedMin = 1f;
    public float JumpSpeedMax = 5f;
    public float WaitJumpInterval = 0.04f;
    public float JumpMultiplier = 1f;
    public float MoveSpeed = 10f;
    float horizontalMove = 0f;
    float horizontalDir = 0;
    private Vector2 moveDir = Vector2.zero;
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        PowerBar = Canvas.transform.Find("PowerBar").gameObject;
        PowerBar.transform.SetParent(Canvas.transform);
        PowerBar.SetActive(false);
        PBar = PowerBar.GetComponent<Slider>();
        FillObject = PBar.transform.Find("Fill Area").GetChild(0).gameObject;
        FillImage = FillObject.GetComponent<Image>();

        thisRigidbody2D = GetComponent<Rigidbody2D>();
        thisAnimator = GetComponent<Animator>();
        thisSpriteRenderer = GetComponent<SpriteRenderer>();

        RightTrigger = RightChild.GetComponent<TriggerCheck>();
        LeftTrigger = LeftChild.GetComponent<TriggerCheck>();
        GroundTrigger = GroundChild.GetComponent<TriggerCheck>();
        StartCoroutine(JumpCounter());
    }

    // Update is called once per frame
    void Update()
    {
        RightCheckEmpty = RightTrigger.checkEmpty;
        LeftCheckEmpty = LeftTrigger.checkEmpty;
        onGround = !GroundTrigger.checkEmpty;

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
            jumpSpeed = JumpSpeedMin + Jumpcounter * JumpMultiplier;
            // jumpSpeed = (jumpSpeed > JumpSpeedMax)? JumpSpeedMax : jumpSpeed;
            moveDir.y = jumpSpeed;
            jump = false;
            jumpStart = false;
        }

        moveDir.x = horizontalMove;

        thisRigidbody2D.velocity = moveDir;
    }

    IEnumerator JumpCounter()
    {
        WaitForSeconds JumpWait = new WaitForSeconds(WaitJumpInterval);
        Jumpcounter = 0;
        Color newColor = Color.green;
        while(true)
        {
            if (jump)
            {
                PowerBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + (Vector3.up*0.2f));
                PowerBar.SetActive(true);
                yield return JumpWait;

                if (Jumpcounter <= (JumpSpeedMax-JumpSpeedMin))
                {
                    Debug.Log("JumpCounting..." + Jumpcounter);
                    Jumpcounter += 0.1f;
                    PBar.value = Jumpcounter / (JumpSpeedMax-JumpSpeedMin);

                    if (PBar.value <= 0.5f) //change color yellow to red
                        newColor.r = PBar.value * 2;
                    else
                        newColor.g = 1 - ((PBar.value - 0.5f) * 2);
                    FillImage.color = newColor;
                }
            }
            else
            {
                Jumpcounter = 0;
                PowerBar.SetActive(false);
                PBar.value = 0f;
                newColor = Color.green;
                FillImage.color = Color.green;
                yield return new WaitUntil(() => jump);
            }
        }
    }
}
