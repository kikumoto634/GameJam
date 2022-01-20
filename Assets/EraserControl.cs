using UnityEngine;
using UnityEngine.UI;

public class EraserControl : MonoBehaviour
{
    [SerializeField] private float ForcePower = 10f;
    private float _power;
    public bool IsChange = false;

    public Rigidbody _rb = null;

    public Slider _slider = null;


    private void Start()
    {
        _power = 0;
        IsChange = false;
        _slider.value = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(!IsChange && _power < ForcePower)
            {
                _power += 0.1f;
                if(_power >= ForcePower) IsChange = true;
            }
            else if(IsChange && _power > 0.0f)
            {
                _power -= 0.1f;
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
            Debug.Log("”­ŽË");
        }

        Debug.Log(_power);
        //0.1‚Å‘¬“x§ŒÀ
        _slider.value = _power * 0.1f;
    }
}
