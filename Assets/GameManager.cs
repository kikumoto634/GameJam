using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool _hideMouse = false;

    //�Q�[���I�[�o�[
    [SerializeField] UnityEngine.Events.UnityEvent _onGameStart = null;
    //�Q�[���N���A
    [SerializeField] UnityEngine.Events.UnityEvent _onGameOver = null;

    //���ׂĂ̓G�I�u�W�F�N�g
    List<ComMove> _enemies = null;

    //�A�v��������
    private void Start()
    {
        if (_hideMouse)
        {
            Cursor.visible = false;
        }

        _enemies = GameObject.FindObjectsOfType<ComMove>().ToList();
    }

    //�Q�[��������
    void StartGame()
    {
        _onGameStart.Invoke();

        //life������

        //�c�@������

        //�v���C���[�ʒu������
    }

    //Restart
    void Restart()
    {
        Debug.Log("Restart");
        StartGame();
    }

    //�Q�[���I�[�o�[
    void Gameover()
    {
        Debug.Log("�Q�[���I�[�o�[");
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
