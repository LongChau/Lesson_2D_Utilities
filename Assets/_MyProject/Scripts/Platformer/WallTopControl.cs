using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTopControl : MonoBehaviour
{
    public PlayerControl playerCtrl;
    public float dotVal;
    public CircleCollider2D _col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var direction = playerCtrl.transform.position - transform.position;
        dotVal = Vector2.Dot(transform.up, direction.normalized);
        if (dotVal > 0f)
        {
            //if (direction.magnitude > 1f)
                _col.enabled = false;
        }
        else
        {
            //if (direction.magnitude > 1f)
                _col.enabled = true;
        }
    }
}
