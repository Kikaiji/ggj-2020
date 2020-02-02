using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    string ToLoad;
    [SerializeField]
    GameState StatetoLoad;
    GameManager manager;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ButtonClick()
    {
        manager.state = StatetoLoad;
        SceneManager.LoadScene(ToLoad);
    }
}
