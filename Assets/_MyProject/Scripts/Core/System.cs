using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Lesson2D
{
    public class System : MonoBehaviour
    {
        [SerializeField] Player _player;

        // Start is called before the first frame update
        void Start()
        {

        }

        [ContextMenu("Save")]
        void Save()
        {
            //PlayerPrefs.SetString("PLAYER_NAME", _player.playerName);
            //PlayerPrefs.SetInt("SCORE", _player.score);
            //PlayerPrefs.SetInt("TIME_PLAYED", _player.timePlayed);

            string json = JsonUtility.ToJson(_player);
            PlayerPrefs.SetString("PLAYER_DATA", json);
        }

        [ContextMenu("Load")]
        void Load()
        {
            //    var playerName = PlayerPrefs.GetString("PLAYER_NAME");
            //    var playerScore = PlayerPrefs.GetInt("SCORE");
            //    var timePlayed = PlayerPrefs.GetInt("TIME_PLAYED");

            //    Debug.Log($"Name: {playerName}, score: {playerScore}, time: {timePlayed}");

            string json = PlayerPrefs.GetString("PLAYER_DATA");
            _player = JsonUtility.FromJson<Player>(json);
        }
    }

    [Serializable]
    public class Player
    {
        public string playerName;
        public int score;
        public int timePlayed;
    }
}
