using System.Collections;
using UnityEngine;
using System.Linq;
public class ComMove : MonoBehaviour, IPause
{
    [Header("�c�@")]
    public int Life = 3;

    [Header("�p���[")]
    [SerializeField]private float _power = 20f;
    [SerializeField]private Rigidbody _rb = null;

    [SerializeField] private GameObject _target = null;

    [Header("�����ʒu")]
    [SerializeField] private Vector3 InitialPos = default;

    [Header("���S�ʒu")]
    private float DeadArea = -30f;
 
    bool IsShot = true;
    //bool IsDown = false;

    [Header("�R���|�[�l���g")]
    public GameManager _gameManager = null;

    //�|�[�Y�̐ݒ�
    Vector3 AngularVelocity = default;
    Vector3 MyVelocity = default;
    bool IsPause = false;

    //�L���b�V��
    Transform _thisTransPos = default;

    /// <summary>
    /// �J�n������
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

            //��~�A�^�[���ړ�
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


    //�����s������
    void Shot()
    {
        Debug.Log("enemy"+_gameManager.enemy_i+":�U��");
        transform.LookAt(_target.transform);
        _rb.AddForce(transform.forward * (_power*10), ForceMode.Impulse);
        _rb.AddTorque(Vector3.up * Mathf.PI * (_power*10), ForceMode.Force);
        IsShot = false;
    }

    //���S������
    void Dead()
    {
        if(transform.position.y <= DeadArea)
        {
            //Debug.Log("enemy���S");
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

    //�^�[���ړ����̐���
    IEnumerator TurnChange()
    {
        //Debug.Log("0.5�b�ҋ@");
        //�w��b�ҋ@����
        yield return new WaitForSeconds(0.5f);

        //IsDown = false;
        //Debug.Log("enemy:"+_gameManager.enemy_i+"��~");
        _gameManager.enemy_i = _gameManager.enemy_i + 1;
        IsShot = true;
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
