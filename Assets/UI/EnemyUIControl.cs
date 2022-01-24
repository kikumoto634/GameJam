using UnityEngine;

public class EnemyUIControl : MonoBehaviour
{
    [SerializeField] private Camera _uiCamera = null;

    //キャッシュ
    Quaternion _thisTransRotate = default;

    private void Awake()
    {
        _thisTransRotate = this.gameObject.transform.rotation;
    }

    void LateUpdate() {
        //　カメラと同じ向きに設定
        _thisTransRotate = _uiCamera.transform.rotation;
    }
}
