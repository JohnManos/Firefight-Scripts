using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player_Move_Update : MonoBehaviour {

    public float playerSpeed = 10;
    public int playerJumpPower = 1250;
    public int numberOfJumps = 2; // max number of jumps
    private float moveX = 0;
    private float moveY = 0;
    private int jumpsRemaining; // to allow double jumps
    private int isJumping = 0; // to allow double jumps
    private Collider2D lastTouched = null;
    private bool isMoving = false; // for heavy sword which disallows attack while moving
    private bool isRotating = false; // for swaying column platforms

    private Sprite jumpSprite; // change sprite on ascent
    private Sprite landingSprite; // change sprite on descent

    //[Tooltip("Only change this if your character is having problems jumping when they shouldn't or not jumping at all.")]
    //public float distToGround = 4.2f;
    private bool inControl = true;

    [Tooltip("Everything you jump on should be put in a ground layer. Without this, your player probably* is able to jump infinitely")]
    public LayerMask GroundLayer;
    [Tooltip("Everything you can walljump on should be put in a wall layer. Without this, your player probably* is able to jump infinitely")]
    public LayerMask WallLayer;



    void Start()
    {
        jumpsRemaining = numberOfJumps;
        jumpSprite = (Sprite)Resources.Load<Sprite>("player_jump_sprite_512");
        landingSprite = (Sprite)Resources.Load<Sprite>("player_landing_sprite_512");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        JumpRefresh();
        if (inControl)
        {
            GetInput();
        } //else {Debug.Log("playerSpeed is: " + playerSpeed);}

        PlayerMove();

        if (GetComponent<Weapon_Status>().GetWeapon() == "fireSword")
        {
            jumpSprite = (Sprite)Resources.Load<Sprite>("player_armed_jump_sprite_512");
            landingSprite = (Sprite)Resources.Load<Sprite>("player_armed_landing_sprite_512");        
        }
        else if (GetComponent<Weapon_Status>().GetWeapon() == "fireStaff")
        {
            jumpSprite = (Sprite)Resources.Load<Sprite>("player_staff_jump_sprite_512");
            landingSprite = (Sprite)Resources.Load<Sprite>("player_staff_landing_sprite_512");             
        }
    }

    void JumpRefresh()
    {
        if (IsGrounded())
		{
			jumpsRemaining = numberOfJumps;
            //isJumping = 0;
		}
        if (IsWalled())
        {
            jumpsRemaining = numberOfJumps;
            //isJumping = 1;
        }
    }

    void GetInput()
    {
        //CONTROLS
        //if (inControl)
        //{
        float yVelocity = GetComponent<Rigidbody2D>().velocity.y;
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (moveX != 0 || System.Math.Abs(yVelocity) > 0.05f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
		
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("jumpsRemaining = " + jumpsRemaining + " isJumping = " + isJumping + " IsWalled() = " + IsWalled() + " IsGrounded() = " + IsGrounded());
            if (jumpsRemaining > 0 /*&& (IsGrounded() || IsWalled() || ((isJumping == 1) || (isJumping == 2)))*/)
            {
                Jump();
            }
        }
        //} else {
        //    moveX = 0;
          //  moveY = 0;
        //} 
    }

	void PlayerMove()
	{
		//ANIMATIONS
        if ((moveX != 0) && (playerSpeed != 0) && (IsWalled() == false))
        {
            GetComponent<Animator>().SetBool("IsRunning", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsRunning", false);
        }
        if (GetComponent<Rigidbody2D>().velocity.y > 0.1f /*Input.GetButton("Jump") && (playerSpeed != 0)*/ )
        {
            //GetComponent<Animator>().SetBool("IsJumping", true);
            GetComponent<SpriteRenderer>().sprite = jumpSprite;
            GetComponent<Animator>().enabled = false;
        }
        else if (GetComponent<Rigidbody2D>().velocity.y < -0.3f )
        {
            GetComponent<SpriteRenderer>().sprite = landingSprite;
            GetComponent<Animator>().enabled = false;
        }
        else
        {
            //gameObject.GetComponent<SpriteRenderer>().sprite = ogSprite;
            GetComponent<Animator>().enabled = true;
            //GetComponent<Animator>().SetBool("IsJumping", false);
        }
        
        //PLAYER DIRECTION
        if (moveX < 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //PHYSICS
        if (IsGrounded())
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        } else {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed * 0.67f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
	}

    void Jump()
    {
        //JUMPING CODE
        //GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        //isJumping = 2;
        --jumpsRemaining;
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed * 0.67f, playerJumpPower/gameObject.GetComponent<Rigidbody2D>().mass);
    }

    public bool IsGrounded()
    {   
        float bonus = 0;
        if (isRotating)
        {
           // bonus = .2f;
        }
        RaycastHit2D hitMiddle = Physics2D.Raycast(transform.position, -Vector2.up, gameObject.GetComponent<Collider2D>().bounds.extents.y, GroundLayer);
        Vector3 leftStartPoint = transform.position - new Vector3(gameObject.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
        RaycastHit2D hitLeftSide = Physics2D.Raycast(leftStartPoint, -Vector2.up, gameObject.GetComponent<Collider2D>().bounds.extents.y + bonus, GroundLayer);
        Vector3 rightStartPoint = transform.position + new Vector3(gameObject.GetComponent<Collider2D>().bounds.extents.x, 0, 0);
        RaycastHit2D hitRightSide = Physics2D.Raycast(rightStartPoint, -Vector2.up, gameObject.GetComponent<Collider2D>().bounds.extents.y + bonus, GroundLayer);
        
        if (hitMiddle.collider != null)
        {
            lastTouched = hitMiddle.collider;
            return true;
        }
        if (hitLeftSide.collider != null)
        {
            lastTouched = hitLeftSide.collider;
            return true;
        }
        if (hitRightSide.collider != null) 
        {
            lastTouched = hitRightSide.collider;
            return true;
        }
        return false;
    }

    public bool IsWalled()
    {
        bool retVal = false;
        Vector3 bottomStartPoint = transform.position - new Vector3(0, gameObject.GetComponent<Collider2D>().bounds.extents.y + 0.1f, 0);
        RaycastHit2D hitLeftMiddle = Physics2D.Raycast(transform.position, Vector2.left, gameObject.GetComponent<Collider2D>().bounds.extents.x + 0.1f, WallLayer);
        RaycastHit2D hitLeftBottom = Physics2D.Raycast(bottomStartPoint, Vector2.left, gameObject.GetComponent<Collider2D>().bounds.extents.x + 0.1f, WallLayer);
        RaycastHit2D hitRightMiddle = Physics2D.Raycast(transform.position, Vector2.right, gameObject.GetComponent<Collider2D>().bounds.extents.x + 0.1f, WallLayer);        
        RaycastHit2D hitRightBottom = Physics2D.Raycast(bottomStartPoint, Vector2.right, gameObject.GetComponent<Collider2D>().bounds.extents.x + 0.1f, WallLayer);        
        
        if (hitLeftBottom.collider != null)
        {
            //isJumping = false;
            if (lastTouched != hitLeftBottom.collider)
            {
                retVal = true;
                lastTouched = hitLeftBottom.collider;
            }
        } 
        if (hitLeftMiddle.collider != null)
        {
            //isJumping = false;
            if (lastTouched != hitLeftMiddle.collider)
            {
                retVal = true;
                lastTouched = hitLeftMiddle.collider;
            }
        } 
        if (hitRightBottom.collider != null)
        {
            //isJumping = false;
            if (lastTouched != hitRightBottom.collider)
            {
                retVal = true;
                lastTouched = hitRightBottom.collider;
            }
        }
        if (hitRightMiddle.collider != null)
        {
            //isJumping = false;
            if (lastTouched != hitRightMiddle.collider)
            {
                retVal = true;
                lastTouched = hitRightMiddle.collider;
            }
        }
        
        return retVal;
    }

    public void SetControl(bool b)
    {
        inControl = b;
    }
    public bool GetControl()
    {
        return inControl;
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }
    public void SetIsRotating(bool newIsRotating)
    {
        isRotating = newIsRotating;
    }
}

