using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    //�v���C���[��ϐ��Ɋi�[
    public GameObject Player;
 
    //��]������X�s�[�h
    public float rotateSpeed = 3.0f;
 
    //�Ώۂ����Ossfet
    private Vector3 PlayerPos = default;

    void Update () 
    {
        transform.position += Player.transform.position - PlayerPos;
        PlayerPos = Player.transform.position;
        transform.RotateAround(PlayerPos, Vector3.up, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime);
    }
}
