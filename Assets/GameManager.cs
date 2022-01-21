using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool _hideMouse = false;

    //ゲームオーバー
    [SerializeField] UnityEngine.Events.UnityEvent _onGameStart = null;
    //ゲームクリア
    [SerializeField] UnityEngine.Events.UnityEvent _onGameOver = null;

    //すべての敵オブジェクト
    List<ComMove> _enemies = null;

    //アプリ初期化
    private void Start()
    {
        if (_hideMouse)
        {
            Cursor.visible = false;
        }

        _enemies = GameObject.FindObjectsOfType<ComMove>().ToList();
    }

    //ゲーム初期化
    void StartGame()
    {
        _onGameStart.Invoke();

        //life初期化

        //残機初期化

        //プレイヤー位置初期化
    }

    //Restart
    void Restart()
    {
        Debug.Log("Restart");
        StartGame();
    }

    //ゲームオーバー
    void Gameover()
    {
        Debug.Log("ゲームオーバー");
        _onGameOver.Invoke();
    }

    private void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        Cursor.visible = true;
    }
}
