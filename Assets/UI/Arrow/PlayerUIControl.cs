using UnityEngine;

public class PlayerUIControl : MonoBehaviour, IPause
{
    public EraserControl _player = null;

    public GameObject _arrow1 = null;
    public GameObject _arrow2 = null;

    Animator Arrow1Anim = null;
    Animator Arroe2Anim = null;

    private void Awake()
    {
        Arrow1Anim = _arrow1.GetComponent<Animator>();
        Arroe2Anim = _arrow2.GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        _arrow1.SetActive(_player.IsUI);
        _arrow2.SetActive(_player.IsUI);
    }


    //É|Å[ÉYê›íË
    public void Pause()
    {
        Arrow1Anim.speed = 0;
        Arroe2Anim.speed = 0;
    }

    public void Resume()
    {
        Arrow1Anim.speed = 1;
        Arroe2Anim.speed = 1;
    }
}