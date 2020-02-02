using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MENU, TOWN, TALK, VENTURE, BATTLE, SCENE}
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameState state;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
