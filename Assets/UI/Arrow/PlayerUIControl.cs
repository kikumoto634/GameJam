using UnityEngine;

public class PlayerUIControl : MonoBehaviour
{
    public GameManager _gameManager = null;

    public GameObject Arrow1 = null;
    public GameObject Arrow2 = null;

    private void Awake()
    {
        Arrow1.SetActive(true);
        Arrow2.SetActive(true);
    }

    private void LateUpdate()
    {
        Arrow1.SetActive(_gameManager.IsPlayerTurn);
        Arrow2.SetActive(_gameManager.IsPlayerTurn);
    }
}