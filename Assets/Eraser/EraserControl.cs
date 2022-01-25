using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EraserControl : MonoBehaviour, IPause
{
    [Header("残機")]
    public int Life = 3;

    [Header("打つ")]
    public bool IsShot = true;

    [Header("パワー")]
    [SerializeField] private float ForcePower = 10f;
    private float _power;
    private bool IsChange = false;

    [Header("UI表示")]
    public bool IsUI = true;

    [Header("初期位置")]
    [SerializeField] private Vector3 InitialPos = default;

    [Header("死亡位置")]
    private float DeadArea = -30f;

    [Header("コンポーネント")]
    [SerializeField] private Rigidbody _rb = null;
    [SerializeField] private Slider _slider = null;

    [SerializeField]private GameManager _gameManager = null;
    [SerializeField]private PlayerLifeControl _playerLifeConrol = null;


    //ポーズの設定
    Vector3 AngularVelocity = default;
    Vector3 MyVelocity = default;
    bool IsPause = false;

    //キャッシュ
    Transform _thisTransPos = default;

    private void Awake()
    {
        _thisTransPos = this.gameObject.transform;
        this.gameObject.transform.position = InitialPos;
        _gameManager._player.Add(this.gameObject.name);
    }

    /// <summary>
    /// 開始初期化
    /// </summary>
    private void Start()
    {
        _power = 0;
        IsChange = false;
        IsUI = true;
        _slider.value = 0;

        _playerLifeConrol.SetLifeGauge(Life);
    }

    
    private void Update()
    {
        if(!IsPause && _gameManager.IsPlayerTurn){
            if(IsShot){
                IsUI = true;
                Shot();
            }

            //停止でIsClickをfalse
            if (_rb.IsSleeping() && !IsShot)
            {
                StartCoroutine("TurnChange");
            }

            //0.1で速度制限
            _slider.value = _power * 0.05f;
        }
    }

    private void LateUpdate()
    {
        if(!IsPause)Dead();
    }

    //消しピン制御
    void Shot()
    {
        if (Input.GetMouseButton(0))
        {
            if(!IsChange && _power < ForcePower)
            {
                _power += 0.2f;
                if(_power >= ForcePower) IsChange = true;
            }
            else if(IsChange && _power > 0.0f)
            {
                _power -= 0.2f;
                if(_power <= 0.0f) IsChange = false;
            }
            else
            {
                _power = 0.1f;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {   
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            _rb.AddForce(cameraForward * (_power*10), ForceMode.Impulse);
            _rb.AddTorque(Vector3.up * Mathf.PI * (_power*10), ForceMode.Force);
            IsShot = false;
            IsUI = false;
            Debug.Log("発射");
        }
    }

    //死亡時
    void Dead()
    {
        if(transform.position.y <= DeadArea)
        {
            //Debug.Log("player死亡");
            Life -= 1;
            _playerLifeConrol.SetLifeGauge2(1);
            
            if(Life <= 0)
            {
                _gameManager._player.Remove(this.gameObject.name);
                this.gameObject.SetActive(false);
            }

            _thisTransPos.position = InitialPos;
        }
    }

    //ターン制御
    IEnumerator TurnChange()
    {
        //Debug.Log("1秒待機");
        //指定秒待機する
        yield return new WaitForSeconds(1);

        IsShot = true;
        _gameManager.IsEnemyTurn = true;
        _power = 0.0f;
        _slider.value = 0;
        _gameManager.IsPlayerTurn = false;
        //Debug.Log("player再設定");
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
