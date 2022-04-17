using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Main : MonoBehaviour
{
    [SerializeField] Toggle _toggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("ToggleUI")]
    private void ToggleUI()
    {
        _toggle.isOn = !_toggle.isOn;
        //_toggle.isOn = true;
    }
}
