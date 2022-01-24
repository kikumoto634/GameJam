using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IPause
{
    [SerializeField] Text _text = default;
    [SerializeField] string _pauseMessage = "PAUSE";
    [SerializeField] Animator _anim = default;

    void IPause.Pause()
    {
        _text.text = _pauseMessage;
        _anim?.Play("Blink");
    }

    void IPause.Resume()
    {
        _text.text = "";
        _anim?.Play("Default");
    }
}
