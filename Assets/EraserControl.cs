using UnityEngine;
using UnityEngine.UI;

public class EraserControl : MonoBehaviour
{
    [SerializeField] private float ForcePower = 10f;
    private float _power;
    private bool IsChange = false;

    [SerializeField] private Rigidbody _rb = null;

    [SerializeField] private Slider _slider = null;

    public bool IsClick = false;

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
        if (Input.GetMouseButton(0))
        {
            IsClick = true;

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
            _power = 0;
            IsChange = false;
            comMove.ComTurn();
            Debug.Log("����");
        }

        //��~��IsClick��false
        if (_rb.IsSleeping())
        {
            IsClick = false;

            Debug.Log("IsClick:"+IsClick);
        }


        //Debug.Log(_power);
        //0.1�ő��x����
        _slider.value = _power * 0.05f;
    }
}
