using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void OnGamePlay()
    {
        SceneManager.LoadScene("Play");
    }
}
