using UnityEngine;

public class EnemyUIControl : MonoBehaviour
{
    [SerializeField] private Camera _uiCamera = null;

    //�L���b�V��
    Quaternion _thisTransRotate = default;

    private void Awake()
    {
        _thisTransRotate = this.gameObject.transform.rotation;
    }

    void LateUpdate() {
        //�@�J�����Ɠ��������ɐݒ�
        _thisTransRotate = _uiCamera.transform.rotation;
    }
}
