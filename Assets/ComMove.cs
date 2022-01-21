using System.Collections;
using UnityEngine;

public class ComMove : MonoBehaviour
{
    [SerializeField]private float _power = 20f;
    [SerializeField]private Rigidbody _rb = null;

    [SerializeField] private GameObject _target = null;
 
 
     public void ComTurn()
    {
         StartCoroutine("ComForward");
    }
 
    IEnumerator ComForward()
    {
        yield return new WaitForSeconds(1.0f);
        transform.LookAt(_target.transform);
        _rb.AddForce(transform.forward * (_power*10), ForceMode.Impulse);
        _rb.AddTorque(Vector3.up * Mathf.PI * (_power*10), ForceMode.Force);
        Debug.Log("“G”­ŽË");
    }
}
