using UnityEngine;

public class CameraTarget : MonoBehaviour, IPause
{
    //プレイヤーを変数に格納
    public GameObject Player;
 
    //回転させるスピード
    public float rotateSpeed = 3.0f;
 
    //対象からのOssfet
    private Vector3 PlayerPos = default;

    bool IsPause = false;

    void Update () 
    {
        if(!IsPause)
        {
            transform.position += Player.transform.position - PlayerPos;
            PlayerPos = Player.transform.position;
            transform.RotateAround(PlayerPos, Vector3.up, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime);
        }
    }


    //ポーズ設定
    public void Pause()
    {
        IsPause = true;
    }

    public void Resume()
    {
        IsPause = false;
    }
}
