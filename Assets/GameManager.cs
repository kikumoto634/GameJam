using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("�e�L�����̐��Aname(����)")]
    public List<string> _enemys = null;
    public List<string> _player = null;

    [Header("�G�̃^�[���ԍ�(����)")]
    public int enemy_i = 0;

    [Header("�e�^�[��(����)")]
    public bool IsPlayerTurn = true;
    public bool IsEnemyTurn = false;


    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        //Debug.Log("�G�̐�"+_enemys.Count);
        //Debug.Log("�v���C���[�̐�"+_player.Count);

        Turn();

        if(_enemys == null)
        {
            Debug.Log("�N���A");
            //SceneManager.LoadScene("Title");
        }

        if(_player == null)
        {   
            Debug.Log("�I�[�o�[");
            //SceneManager.LoadScene("Title");
        }
    }

    void Turn()
    {
        //false��player->enemy��turn�ɂȂ�
        if (!IsPlayerTurn && IsEnemyTurn)
        {
            Debug.Log("enemyTurn");

            if(_enemys.Count >= enemy_i+1)
            {
                IsEnemyTurn = true;
            }
            else if(_enemys.Count < enemy_i+1)
            {
                Debug.Log("���ׂĂ̓G�̍U���I��");
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
