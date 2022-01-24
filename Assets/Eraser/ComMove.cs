using System.Collections;
using UnityEngine;
using System.Linq;
public class ComMove : MonoBehaviour, IPause
{
    [Header("残機")]
    public int Life = 3;

    [Header("パワー")]
    [SerializeField]private float _power = 20f;
    [SerializeField]private Rigidbody _rb = null;

    [SerializeField] private GameObject _target = null;

    [Header("初期位置")]
    [SerializeField] private Vector3 InitialPos = default;

    [Header("死亡位置")]
    private float DeadArea = -30f;
 
    bool IsShot = true;
    //bool IsDown = false;

    [Header("コンポーネント")]
    public GameManager _gameManager = null;

    //ポーズの設定
    Vector3 AngularVelocity = default;
    Vector3 MyVelocity = default;
    bool IsPause = false;

    //キャッシュ
    Transform _thisTransPos = default;

    /// <summary>
    /// 開始初期化
    /// </summary>
    private void Start()
    {
        _thisTransPos = this.gameObject.transform;
        this.gameObject.transform.position = InitialPos;
        _gameManager._enemys.Add(this.gameObject.name);
    }

    private void Update()
    {
        if(!IsPause){
            //Debug.Log("enemy:"+_gameManager._enemys.IndexOf(this.gameObject.name));
            //Debug.Log("enemy_i:"+_gameManager.enemy_i);
            if (_gameManager._enemys.IndexOf(this.gameObject.name) == _gameManager.enemy_i && _gameManager.IsEnemyTurn && IsShot)
            {
                Shot();
            }

            //停止、ターン移動
            if (!IsShot && _rb.IsSleeping())
            {
                StartCoroutine("TurnChange");
            }
        }
    }

    private void LateUpdate()
    {
        if(!IsPause)Dead();
    }


    //消しピン制御
    void Shot()
    {
        Debug.Log("enemy"+_gameManager.enemy_i+":攻撃");
        transform.LookAt(_target.transform);
        _rb.AddForce(transform.forward * (_power*10), ForceMode.Impulse);
        _rb.AddTorque(Vector3.up * Mathf.PI * (_power*10), ForceMode.Force);
        IsShot = false;
    }

    //死亡時制御
    void Dead()
    {
        if(transform.position.y <= DeadArea)
        {
            //Debug.Log("enemy死亡");
            Life -= 1;
            //IsDown = true;

            if(Life <= 0)
            {
                _gameManager._enemys.Remove(this.gameObject.name);
                this.gameObject.SetActive(false);
            }

            _thisTransPos.position = InitialPos;
        }
    }

    //ターン移動時の制御
    IEnumerator TurnChange()
    {
        //Debug.Log("0.5秒待機");
        //指定秒待機する
        yield return new WaitForSeconds(0.5f);

        //IsDown = false;
        //Debug.Log("enemy:"+_gameManager.enemy_i+"停止");
        _gameManager.enemy_i = _gameManager.enemy_i + 1;
        IsShot = true;
    }

    ///<summary>
    ///Pause時の制御
    ///</summary>
    //ポーズ
    public void Pause()
    {
        IsPause = true;
        AngularVelocity = _rb.angularVelocity;
        MyVelocity = _rb.velocity;
        _rb.Sleep();
    }
    //解除
    public void Resume()
    {
        _rb.WakeUp();
        _rb.angularVelocity = AngularVelocity;
        _rb.velocity = MyVelocity;
        IsPause = false;
    }
}
