using System.Collections;
using UnityEngine;
using System.Linq;
public class ComMove : MonoBehaviour
{
    [Header("残機")]
    public int Life = 3;

    [Header("パワー")]
    [SerializeField]private float _power = 20f;
    [SerializeField]private Rigidbody _rb = null;

    [SerializeField] private GameObject _target = null;

    [Header("初期位置")]
    [SerializeField] private Vector3 InitialPos = default;

    private float DeadArea = -30f;
 
    bool IsShot = true;
    bool IsDown = false;

    public GameManager _gameManager = null;


    private void Awake()
    {
        this.transform.position = InitialPos;
        _gameManager._enemys.Add(this.gameObject.name);
    }

    private void Update()
    {
        //Debug.Log("enemy:"+_gameManager._enemys.IndexOf(this.gameObject.name));
        //Debug.Log("enemy_i:"+_gameManager.enemy_i);
        if (_gameManager._enemys.IndexOf(this.gameObject.name) == _gameManager.enemy_i && _gameManager.IsEnemyTurn && IsShot)
        {
            Debug.Log("enemy"+_gameManager.enemy_i+":攻撃");
            transform.LookAt(_target.transform);
            _rb.AddForce(transform.forward * (_power*10), ForceMode.Impulse);
            _rb.AddTorque(Vector3.up * Mathf.PI * (_power*10), ForceMode.Force);
            IsShot = false;
        }

        //停止、ターン移動
        if ((!IsShot && _rb.IsSleeping()) || IsDown)
        {
            IsDown = false;
            //Debug.Log("enemy:"+_gameManager.enemy_i+"停止");
            _gameManager.enemy_i = _gameManager.enemy_i + 1;
            IsShot = true;
        }
    }

    private void LateUpdate()
    {
        Dead();
    }

    void Dead()
    {
        if(transform.position.y <= DeadArea)
        {
            //Debug.Log("enemy死亡");
            Life -= 1;
            IsDown = true;

            if(Life <= 0)
            {
                _gameManager._enemys.Remove(this.gameObject.name);
                this.gameObject.SetActive(false);
            }

            this.transform.position = InitialPos;
        }
    }
}
