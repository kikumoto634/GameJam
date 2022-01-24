using System.Collections;
using UnityEngine;
using System.Linq;
public class ComMove : MonoBehaviour
{
    [Header("�c�@")]
    public int Life = 3;

    [Header("�p���[")]
    [SerializeField]private float _power = 20f;
    [SerializeField]private Rigidbody _rb = null;

    [SerializeField] private GameObject _target = null;

    [Header("�����ʒu")]
    [SerializeField] private Vector3 InitialPos = default;

    private float DeadArea = -30f;
 
    bool IsShot = true;
    bool IsDown = false;

    public GameManager _gameManager = null;

    //�L���b�V��
    Transform _thisTransPos = default;

    private void Awake()
    {
        _thisTransPos = this.gameObject.transform;
        this.gameObject.transform.position = InitialPos;
        _gameManager._enemys.Add(this.gameObject.name);
    }

    private void Update()
    {
        //Debug.Log("enemy:"+_gameManager._enemys.IndexOf(this.gameObject.name));
        //Debug.Log("enemy_i:"+_gameManager.enemy_i);
        if (_gameManager._enemys.IndexOf(this.gameObject.name) == _gameManager.enemy_i && _gameManager.IsEnemyTurn && IsShot)
        {
            Debug.Log("enemy"+_gameManager.enemy_i+":�U��");
            transform.LookAt(_target.transform);
            _rb.AddForce(transform.forward * (_power*10), ForceMode.Impulse);
            _rb.AddTorque(Vector3.up * Mathf.PI * (_power*10), ForceMode.Force);
            IsShot = false;
        }

        //��~�A�^�[���ړ�
        if (!IsShot && _rb.IsSleeping())
        {
            StartCoroutine("Pause");
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

    IEnumerator Pause()
    {
        //Debug.Log("0.5�b�ҋ@");
        //�w��b�ҋ@����
        yield return new WaitForSeconds(0.5f);

        //IsDown = false;
        //Debug.Log("enemy:"+_gameManager.enemy_i+"��~");
        _gameManager.enemy_i = _gameManager.enemy_i + 1;
        IsShot = true;
    }
}
