using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWithPointer : MonoBehaviour
{
    public bool allowMoveWithPointer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMoveWithPointer)
        {
            //Debug.Log(Input.mousePosition);
            // Convert vị trí chuột (pixel-coordinate) sang world space (không gian của scene)
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            transform.position = pos;
        }
    }
}
