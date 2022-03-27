using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropBehaviour : MonoBehaviour
{
    public GameObject _towerPrefab;

    public GameObject _tower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (_tower != null && Input.GetMouseButtonUp(0))
        {
            _tower.GetComponent<MovingWithPointer>().allowMoveWithPointer = false;
        }
    }

    public void Handle_OnPointerDown()
    {
        Debug.Log("Handle_OnPointerDown()");
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        _tower = Instantiate(_towerPrefab, pos, Quaternion.identity);
        _tower.GetComponent<MovingWithPointer>().allowMoveWithPointer = true;
    }

    public void Handle_OnPointerExit()
    {
        Debug.Log("Handle_OnPointerExit()");

    }
}
