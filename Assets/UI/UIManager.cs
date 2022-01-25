using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IPause
{
    [SerializeField] Image _image = default;
    [SerializeField] Animator _anim = default;

    void IPause.Pause()
    {
        _anim?.Play("Blink");
    }

    void IPause.Resume()
    {
        _anim?.Play("Default");
    }
}
