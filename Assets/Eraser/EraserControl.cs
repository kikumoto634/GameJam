using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EraserControl : MonoBehaviour
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

    [SerializeField] private Rigidbody _rb = null;

    [SerializeField] private Slider _slider = null;

    private float DeadArea = -30f;

    [Header("�R���|�[�l���g")]
    public GameObject _computer = null;
    ComMove comMove = null;

    public GameManager _gameManager = null;


    private void Awake()
    {
        this.transform.position = InitialPos;
        _gameManager._player.Add(this.gameObject.name);
    }

    private void Start()
    {
        _power = 0;
        IsChange = false;
        _slider.value = 0;

        comMove = _computer.GetComponent<ComMove>();
    }

    private void Update()
    {
        if(IsShot && _gameManager.IsPlayerTurn)Shot();

        //��~��IsClick��false
        if (_rb.IsSleeping() && !IsShot)
        {
            IsShot = true;
            _power = 0.0f;
            _slider.value = 0;

            _gameManager.IsPlayerTurn = false;
            _gameManager.IsEnemyTurn = true;
            //Debug.Log("player�Đݒ�");
        }


        //0.1�ő��x����
        _slider.value = _power * 0.05f;
    }

    private void LateUpdate()
    {
        Dead();
    }

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

    void Dead()
    {
        if(transform.position.y <= DeadArea)
        {
            //Debug.Log("player���S");
            Life -= 1;
            
            if(Life <= 0)
            {
                _gameManager._player.Remove(this.gameObject.name);
                this.gameObject.SetActive(false);
            }

            this.transform.position = InitialPos;
        }
    }
}
