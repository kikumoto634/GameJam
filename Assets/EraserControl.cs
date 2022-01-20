using UnityEngine;

public class EraserControl : MonoBehaviour
{
    public Rigidbody _rb = null;

    [SerializeField] private float ForcePower = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _rb.AddForce(transform.forward * ForcePower, ForceMode.Impulse);
            Debug.Log("”­ŽË");
        }
    }
}
