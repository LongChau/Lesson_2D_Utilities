using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public Vector2 offset;
    public GameObject stairCheck;
    public GameObject groundCheck;
    public Rigidbody2D rigid;
    public LayerMask groundMask;
    public bool isOnGround;
    public Vector2 direction;
    public float groundCheckLength = 0.03f;
    public float stairCheckLength = 0.03f;
    public float jumpForce = 4f;
    public float speed = 5f;
    public Vector2 oldVelo;
    public float timeCheckOldVelo;
    //public float stairLength = 0.03f;

    public bool isStartJump;
    public bool isInAir;
    public bool isFallingDown;

    public EPlayerState playerState;
    public EJumpState jumpState;

    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instance.HelloWorld();
        timeCheckOldVelo = 0f;
    }

    [ContextMenu("CallGameManager")]
    void CallGameManager()
    {
        GameManager.Instance.HelloWorld();
    }

    // Update is called once per frame
    void Update()
    {
        CheckOnGround();
        //CheckOnStair();

        CheckVelocity();
        UpdateInput();
        UpdatePlayerState();
        UpdateJumpState();
    }

    private void UpdateInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Cách 1:
            if (isOnGround)
            {
                direction = groundObj.transform.right;
            }
            else
            {
                direction = Vector2.right;
            }
            var scale = transform.localScale;
            scale.x = 1f;
            transform.localScale = scale;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (isOnGround)
            {
                direction = -groundObj.transform.right;
            }
            else
            {
                direction = Vector2.left;
            }
            var scale = transform.localScale;
            scale.x = -1f;
            transform.localScale = scale;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            playerState = EPlayerState.Idle;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            jumpState = EJumpState.StartJump;
        }
    }

    private void UpdateJumpState()
    {
        switch (jumpState)
        {
            case EJumpState.StartJump:
                rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpState = EJumpState.InAir;
                break;
            case EJumpState.InAir:
                if (rigid.velocity.y < 0)
                {
                    jumpState = EJumpState.Falling;
                }
                break;
            case EJumpState.Falling:

                break;
        }
    }

    private void UpdatePlayerState()
    {
        switch (playerState)
        {
            case EPlayerState.Idle:

                break;
            case EPlayerState.Walking:
                break;
        }
    }

    private void CheckVelocity()
    {
        var velo = rigid.velocity;
        velo.x = 0f;
        rigid.velocity = velo;

        //if (rigid.velocity.y > 0)
        //{
        //    jumpState = EJumpState.InAir;
        //}
        //else if (rigid.velocity.y < 0)
        //{
        //    jumpState = EJumpState.Falling;
        //}
    }

    RaycastHit2D hitGround;
    RaycastHit2D hitStair;
    private void CheckOnStair()
    {
        hitStair = Physics2D.Raycast(stairCheck.transform.position, Vector2.down, stairCheckLength, groundMask);
        Debug.DrawRay(stairCheck.transform.position, Vector2.down * stairCheckLength, Color.blue);
        if (hitStair.collider != null)
        {
            if (isStartJump) return;
            isOnGround = true;
            groundObj = hitStair.collider.gameObject;

            var velo = rigid.velocity;
            velo.y = 0f;
            rigid.velocity = velo;

            if (hitStair.collider.CompareTag("Stair"))
            {
                if (Vector2.Distance(stairCheck.transform.position, hitStair.point) <= 0.001f)
                {

                    // Snap to ground.
                    Vector2 pos = transform.position;
                    pos.y = hitGround.point.y;
                    transform.position = pos + offset;

                    isFallingDown = false;
                    isInAir = false;
                }
            }
        }
    }

    GameObject groundObj;
    private void CheckOnGround()
    {
        hitGround = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, groundCheckLength, groundMask);
        Debug.DrawRay(groundCheck.transform.position, Vector2.down * groundCheckLength, Color.red);
        if (hitGround.collider != null)
        {
            groundObj = hitGround.collider.gameObject;

            isOnGround = true;

            var distance = Vector2.Distance(groundCheck.transform.position, hitGround.point);
            if (distance <= 0.2f)
            {
                // Snap to ground.
                if (hitGround.collider.CompareTag("Stair"))
                {
                    Vector2 pos = transform.position;
                    pos.y = hitGround.point.y;
                    transform.position = pos + offset;
                }
                // TODO: Allow to jump here;
            }
        }
        else
        {
            groundObj = null;
            isOnGround = false;
            if (!isWallTop)
                rigid.gravityScale = 1f;
        }
    }

    bool isWallTop;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WallTop"))
        {
            if (!isOnGround && oldVelo.y > rigid.velocity.y)
            {
                Debug.Log("Hit Wall top");
                isWallTop = true;
                rigid.gravityScale = 0;
                var velo = rigid.velocity;
                velo.y = 0f;
                rigid.velocity = velo;
                StartCoroutine(IEClimpTopWall());
            }
        }
    }
    public float timeClimpTopWall = 1f;
    IEnumerator IEClimpTopWall()
    {
        var time = timeClimpTopWall;
        while (time > 0f)
        {
            time -= Time.deltaTime;
            transform.Translate((transform.up + -transform.right) * speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WallTop"))
        {
            Debug.Log("Leave Wall top");
            isWallTop = false;
            rigid.gravityScale = 1;
        }
    }

    public enum EPlayerState
    {
        Idle, 
        Walking,
    }

    public enum EJumpState
    {
        None = 0,
        StartJump = 1,
        InAir = 2,
        Falling = 3
    }
}
