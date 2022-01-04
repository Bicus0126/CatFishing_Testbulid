using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D thisRigidbody2D;
    private Animator thisAnimator;
    private SpriteRenderer thisSpriteRenderer;

    public float JumpForce = 3000.0f;
    public bool isGrounded = false;
    public float MoveSpeed = 0.01f;
    private Vector2 moveDir;

    [ReadOnlyInspecter] public GameObject groundedObject;
    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        thisAnimator = GetComponent<Animator>();
        thisSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        thisAnimator.SetBool("inGround", isGrounded);
        Right();
        Left();
        Up();

        //Auto_Right();
        //Auto_Up();

    }

    void Auto_Right()
    {

        moveDir.x = MoveSpeed * 0.1f;
        thisAnimator.SetFloat("MoveSpeed", 1);
        thisSpriteRenderer.flipX = true;

        moveDir.y = thisRigidbody2D.velocity.y;
        thisRigidbody2D.velocity = moveDir;
    }


    void Auto_Up()
    {
        if (isGrounded)
        {
            moveDir.y = MoveSpeed * 1.3f;
            //thisRigidbody2D.AddForce(Vector2.up * JumpForce * 0.03f);
            thisAnimator.SetTrigger("Jump");
        }
        else
        {
            moveDir.y = thisRigidbody2D.velocity.y;
        }

        moveDir.x = thisRigidbody2D.velocity.x;
        thisRigidbody2D.velocity = moveDir;
    }

    void Right()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir.x = MoveSpeed;
            thisAnimator.SetFloat("MoveSpeed", 1);
            thisSpriteRenderer.flipX = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveDir = Vector2.zero;
            thisAnimator.SetFloat("MoveSpeed", 0);
            thisSpriteRenderer.flipX = true;
        }

        moveDir.y = thisRigidbody2D.velocity.y;
        thisRigidbody2D.velocity = moveDir;
    }
    void Left()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir.x = -MoveSpeed;
            thisAnimator.SetFloat("MoveSpeed", 1);
            thisSpriteRenderer.flipX = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveDir = Vector2.zero;
            thisAnimator.SetFloat("MoveSpeed", 0);
            thisSpriteRenderer.flipX = false;
        }

        moveDir.y = thisRigidbody2D.velocity.y;
        thisRigidbody2D.velocity = moveDir;
    }

    void Up()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            thisRigidbody2D.AddForce(Vector2.up * JumpForce * 0.115f);
            thisAnimator.SetTrigger("Jump");
        }

        moveDir.y = thisRigidbody2D.velocity.y;
        thisRigidbody2D.velocity = moveDir;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Blocks"))
        {
            foreach (ContactPoint2D element in other.contacts)
            {
                if (element.normal.y < 0.25f)
                    Debug.Log("Zero");
                else if (element.normal.y > 0.25f)
                {
                    groundedObject = other.gameObject;
                    Debug.Log("One");
                    isGrounded = true;
                    break;
                }

            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == groundedObject)
        {
            thisAnimator.SetTrigger("Jump");
            groundedObject = null;
            isGrounded = false;
        }
    }
}
