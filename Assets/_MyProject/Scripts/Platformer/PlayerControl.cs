using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public GameObject stairCheck;
    public GameObject groundCheck;
    public Rigidbody2D rigid;
    public LayerMask mask;
    public bool isOnGround;
    public Vector2 direction;
    public float groundCheckLength = 0.03f;
    public float jumpForce = 4f;
    public float speed = 5f;
    public Vector2 oldVelo;
    public float timeCheckOldVelo;
    //public float stairLength = 0.03f;

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

        if (Time.time > timeCheckOldVelo)
        {
            timeCheckOldVelo = Time.time + 1 / 10;
            oldVelo = rigid.velocity;
        }

        var velo = rigid.velocity;
        velo.x = 0f;
        rigid.velocity = velo;

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
            transform.Translate(direction * speed * Time.deltaTime);

            // Cách 2:
            //Vector2 dir;
            //dir = groundObj.transform.position - transform.position;
            //direction = Vector3.ProjectOnPlane(dir.normalized, groundObj.transform.up);
            //transform.Translate(direction * 5f * Time.deltaTime);
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
            transform.Translate(direction * speed * Time.deltaTime);

            // Cách 2:
            //Vector2 dir;
            //dir = groundObj.transform.position - transform.position;
            //direction = Vector3.ProjectOnPlane(dir.normalized, groundObj.transform.up);
            //transform.Translate(direction * 5f * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    GameObject groundObj;
    private void CheckOnGround()
    {
        var hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, groundCheckLength, mask);
        Debug.DrawRay(groundCheck.transform.position, Vector2.down * groundCheckLength, Color.red);
        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            isOnGround = true;
            rigid.gravityScale = 0f;
            groundObj = hit.collider.gameObject;
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

    //GameObject stairObj;
    //private void CheckOnStair()
    //{
    //    var hit = Physics2D.Raycast(stairCheck.transform.position, Vector2.right, stairLength, mask);
    //    Debug.DrawRay(stairCheck.transform.position, Vector2.down * stairLength, Color.red);
    //    if (hit.collider != null && hit.collider.CompareTag("Stair"))
    //    {
    //        isOnGround = true;
    //        rigid.gravityScale = 0f;
    //        stairObj = hit.collider.gameObject;
    //    }
    //    else
    //    {
    //        stairObj = null;
    //        isOnGround = false;
    //        rigid.gravityScale = 1f;
    //    }
    //}

    [ContextMenu("ChangeScene")]
    private void ChangeScene()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
