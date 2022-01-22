using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class TextManager : MonoBehaviour
{
    public Text score_text = null; // Textオブジェクト

    public GameObject Player = null;
    EraserControl eraserControl = null;


    private void Start()
    {
        eraserControl = Player.GetComponent<EraserControl>();
    }

    void Update () {
        // テキストの表示を入れ替える
        score_text.text = "×" + eraserControl.Life;
    }
}
