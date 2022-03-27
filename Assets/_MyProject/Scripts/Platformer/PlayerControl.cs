using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject stairCheck;
    public GameObject groundCheck;
    public Rigidbody2D rigid;
    public LayerMask mask;
    public bool isOnGround;
    public Vector2 direction;
    public float groundCheckLength = 0.03f;
    //public float stairLength = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckOnGround();

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
            transform.Translate(direction * 5f * Time.deltaTime);

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
            transform.Translate(direction * 5f * Time.deltaTime);

            // Cách 2:
            //Vector2 dir;
            //dir = groundObj.transform.position - transform.position;
            //direction = Vector3.ProjectOnPlane(dir.normalized, groundObj.transform.up);
            //transform.Translate(direction * 5f * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rigid.AddForce(Vector2.up * 4f, ForceMode2D.Impulse);
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
            rigid.gravityScale = 1f;
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
}
