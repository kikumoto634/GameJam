using System.Collections;
using UnityEngine;

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
 
    bool IsShot = false;


    private void Awake()
    {
        this.transform.position = InitialPos;
    }

    private void Update()
    {
        if (IsShot && _rb.IsSleeping())
        {
            transform.LookAt(_target.transform);
            _rb.AddForce(transform.forward * (_power*10), ForceMode.Impulse);
            _rb.AddTorque(Vector3.up * Mathf.PI * (_power*10), ForceMode.Force);
            IsShot = false;
            Debug.Log("�G����");
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
            Debug.Log("enemy���S");
            Life -= 1;
            this.transform.position = InitialPos;
        }
    }

    public void ComTurn()
    {
        StartCoroutine("ComForward");
    }
 
    IEnumerator ComForward()
    {
        yield return new WaitForSeconds(1.0f);
        IsShot = true;
    }
}
