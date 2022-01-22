using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    List<ComMove> _enemys = null;
    List<EraserControl> _player = null;



    private void Awake()
    {
        Application.targetFrameRate = 60;

        _enemys = GameObject.FindObjectsOfType<ComMove>().ToList();
        _player = GameObject.FindObjectsOfType<EraserControl>().ToList();
    }

    private void Update()
    {
        Debug.Log("敵の数"+_enemys.Count);
        Debug.Log("プレイヤーの数"+_player.Count);

        if(_enemys.Count == 0)
        {
            Debug.Log("クリア");
        }

        if(_player.Count == 0)
        {
            Debug.Log("オーバー");
        }
    }
}
