using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("各キャラの数、name(自動)")]
    public List<string> _enemys = null;
    public List<string> _player = null;

    [Header("敵のターン番号(自動)")]
    public int enemy_i = 0;

    [Header("各ターン(自動)")]
    public bool IsPlayerTurn = true;
    public bool IsEnemyTurn = false;


    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        //Debug.Log("敵の数"+_enemys.Count);
        //Debug.Log("プレイヤーの数"+_player.Count);

        Turn();

        if(_enemys == null)
        {
            Debug.Log("クリア");
            //SceneManager.LoadScene("Title");
        }

        if(_player == null)
        {   
            Debug.Log("オーバー");
            //SceneManager.LoadScene("Title");
        }
    }

    void Turn()
    {
        //falseでplayer->enemyのturnになる
        if (!IsPlayerTurn && IsEnemyTurn)
        {
            Debug.Log("enemyTurn");

            if(_enemys.Count >= enemy_i+1)
            {
                IsEnemyTurn = true;
            }
            else if(_enemys.Count < enemy_i+1)
            {
                Debug.Log("すべての敵の攻撃終了");
                IsEnemyTurn = false;
                enemy_i = 0;
                IsPlayerTurn = true;
            }
        }
        else if (IsPlayerTurn && !IsEnemyTurn)
        {
            Debug.Log("playerTurn");
        }
    }
}
