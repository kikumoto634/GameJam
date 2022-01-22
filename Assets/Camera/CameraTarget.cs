using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    //プレイヤーを変数に格納
    public GameObject Player;
 
    //回転させるスピード
    public float rotateSpeed = 3.0f;
 
    //対象からのOssfet
    private Vector3 PlayerPos = default;

    void Update () 
    {
        transform.position += Player.transform.position - PlayerPos;
        PlayerPos = Player.transform.position;
        transform.RotateAround(PlayerPos, Vector3.up, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime);
    }
}
