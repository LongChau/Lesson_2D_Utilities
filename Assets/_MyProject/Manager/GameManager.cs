using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerHp = 10;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject gameObj = new GameObject();
                    gameObj.name = "GameManager";
                    _instance = gameObj.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HelloWorld()
    {
        Debug.Log("Hellow world, I'm a Singleton.");
        playerHp += 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
