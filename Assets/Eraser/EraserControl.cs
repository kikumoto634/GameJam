using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EraserControl : MonoBehaviour
{

    public bool IsShot = true;

    [SerializeField] private float ForcePower = 10f;
    private float _power;
    private bool IsChange = false;

    [SerializeField] private Rigidbody _rb = null;

    [SerializeField] private Slider _slider = null;

    public GameObject _computer = null;
    ComMove comMove = null;


    private void Start()
    {
        _power = 0;
        IsChange = false;
        _slider.value = 0;

        comMove = _computer.GetComponent<ComMove>();
    }

    private void Update()
    {
        if(IsShot)Shot();

        //í‚é~Ç≈IsClickÇfalse
        if (_rb.IsSleeping() && !IsShot)
        {
            IsShot = true;
            _power = 0.0f;
            _slider.value = 0;
            comMove.ComTurn();
            Debug.Log("playerçƒê›íË");
        }


        //Debug.Log(_power);
        //0.1Ç≈ë¨ìxêßå¿
        _slider.value = _power * 0.05f;
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
            Debug.Log("î≠éÀ");
        }
    }
}
