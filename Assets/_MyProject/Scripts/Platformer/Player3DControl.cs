using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DControl : MonoBehaviour
{
    public CharacterController charCtrl;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData data = new PlayerData();
        data.hp = 10;
        var json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("PlayerData", json);

        var jsonData = PlayerPrefs.GetString("PlayerData");
        data = JsonUtility.FromJson<PlayerData>(jsonData);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            charCtrl.SimpleMove(Vector3.right * 5f);
        else if (Input.GetKey(KeyCode.LeftArrow))
            charCtrl.SimpleMove(Vector3.left * 5f);
    }

    [Serializable]
    public class PlayerData
    {
        public int hp;
    }
}
