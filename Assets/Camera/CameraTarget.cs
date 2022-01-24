using UnityEngine;

public class CameraTarget : MonoBehaviour, IPause
{
    //�v���C���[��ϐ��Ɋi�[
    public GameObject Player;
 
    //��]������X�s�[�h
    public float rotateSpeed = 3.0f;
 
    //�Ώۂ����Ossfet
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


    //�|�[�Y�ݒ�
    public void Pause()
    {
        IsPause = true;
    }

    public void Resume()
    {
        IsPause = false;
    }
}
