using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class TextManager : MonoBehaviour
{
    public Text score_text = null; // Text�I�u�W�F�N�g

    public GameObject Player = null;
    EraserControl eraserControl = null;


    private void Start()
    {
        eraserControl = Player.GetComponent<EraserControl>();
    }

    void Update () {
        // �e�L�X�g�̕\�������ւ���
        score_text.text = "�~" + eraserControl.Life;
    }
}
