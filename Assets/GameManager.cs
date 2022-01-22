using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<ComMove> _enemys = null;
    public List<EraserControl> _player = null;



    private void Awake()
    {
        Application.targetFrameRate = 60;

        _enemys = GameObject.FindObjectsOfType<ComMove>().ToList();
        _player = GameObject.FindObjectsOfType<EraserControl>().ToList();
    }

    private void Update()
    {
        Debug.Log("�G�̐�"+_enemys.Count);
        Debug.Log("�v���C���[�̐�"+_player.Count);

        if(_enemys.Count == 0)
        {
            Debug.Log("�N���A");
            SceneManager.LoadScene("Title");
        }

        if(_player.Count == 0)
        {
            
            Debug.Log("�I�[�o�[");
            SceneManager.LoadScene("Title");
        }
    }
}
