using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EraserControl : MonoBehaviour, IPause
{
    [Header("�c�@")]
    public int Life = 3;

    [Header("�ł�")]
    public bool IsShot = true;

    [Header("�p���[")]
    [SerializeField] private float ForcePower = 10f;
    private float _power;
    private bool IsChange = false;

    [Header("�����ʒu")]
    [SerializeField] private Vector3 InitialPos = default;

    [Header("���S�ʒu")]
    private float DeadArea = -30f;

    [Header("�R���|�[�l���g")]
    [SerializeField] private Rigidbody _rb = null;
    [SerializeField] private Slider _slider = null;

    [SerializeField]private GameManager _gameManager = null;
    [SerializeField]private PlayerLifeControl _playerLifeConrol = null;


    //�|�[�Y�̐ݒ�
    Vector3 AngularVelocity = default;
    Vector3 MyVelocity = default;
    bool IsPause = false;

    //�L���b�V��
    Transform _thisTransPos = default;

    private void Awake()
    {
        _thisTransPos = this.gameObject.transform;
        this.gameObject.transform.position = InitialPos;
        _gameManager._player.Add(this.gameObject.name);
    }

    /// <summary>
    /// �J�n������
    /// </summary>
    private void Start()
    {
        _power = 0;
        IsChange = false;
        _slider.value = 0;

        _playerLifeConrol.SetLifeGauge(Life);
    }

    
    private void Update()
    {
        if(!IsPause && _gameManager.IsPlayerTurn){
            if(IsShot)Shot();

            //��~��IsClick��false
            if (_rb.IsSleeping() && !IsShot)
            {
                StartCoroutine("TurnChange");
            }

            //0.1�ő��x����
            _slider.value = _power * 0.05f;
        }
    }

    private void LateUpdate()
    {
        if(!IsPause)Dead();
    }

    //�����s������
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
            //Debug.Log("����");
        }
    }

    //���S��
    void Dead()
    {
        if(transform.position.y <= DeadArea)
        {
            //Debug.Log("player���S");
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

    //�^�[������
    IEnumerator TurnChange()
    {
        //Debug.Log("1�b�ҋ@");
        //�w��b�ҋ@����
        yield return new WaitForSeconds(1);

        IsShot = true;
        _gameManager.IsEnemyTurn = true;
        _power = 0.0f;
        _slider.value = 0;
        _gameManager.IsPlayerTurn = false;
        //Debug.Log("player�Đݒ�");
    }


    ///<summary>
    ///Pause���̐���
    ///</summary>
    //�|�[�Y
    public void Pause()
    {
        IsPause = true;
        AngularVelocity = _rb.angularVelocity;
        MyVelocity = _rb.velocity;
        _rb.Sleep();
    }
    //����
    public void Resume()
    {
        _rb.WakeUp();
        _rb.angularVelocity = AngularVelocity;
        _rb.velocity = MyVelocity;
        IsPause = false;
    }
}
