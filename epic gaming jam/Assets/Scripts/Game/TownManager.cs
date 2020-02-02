using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour
{
    StatManager sManager;
    public int switches;
    public bool tutorial;
    public GameObject SlimeO;
    public GameObject Goblin;
    public bool Slime = false;
    public bool Gardener;
    public GameObject GhostO;
    public bool Minotaur;
    public bool Ghost;
    public int gameState;
    public bool hasSeenDialog = false;
    public int townLevel;
    int prevGameState;
    GameObject townSprite;
    ResourceManager rManager;
    // Start is called before the first frame update
    void Start()
    {
        sManager = GameObject.Find("StatManager").GetComponent<StatManager>();
        tutorial = true;
        rManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        DontDestroyOnLoad(gameObject);
        townSprite = GameObject.Find("TownSprite");
        gameState = 0;
        prevGameState = gameState;
        townSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Graphics/TownStates/nomadvillage_state" + gameState);
    }

    // Update is called once per frame
    void Update()
    {
        if(prevGameState != gameState)
        {
            townSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Graphics/TownStates/nomadvillage_state" + gameState);
            prevGameState = gameState;
        }
        
        if (Slime)
        {
            if (SlimeO != null && SlimeO.activeSelf == false) { SlimeO.SetActive(true); switches += 1; }
            
        } else { if(SlimeO != null) SlimeO.SetActive(false); }
        if(Gardener)
        {
            if (Goblin != null && Goblin.activeSelf == false) { Goblin.SetActive(true); switches += 1; }
        } else { if(Goblin!= null) Goblin.SetActive(false); }
        if (Ghost)
        {
            if (GhostO != null && GhostO.activeSelf == false) { GhostO.SetActive(true); switches += 1;  }
        }
        else { if (GhostO != null) GhostO.SetActive(false); }

        if (Minotaur == true && (gameState == 1))
        {
            gameState += 1;
        }
    }

    public bool BuildGarden()
    {
        if(rManager.CheckForResource(0, 6) && rManager.CheckForResource(1, 6) && rManager.CheckForResource(2, 12) && gameState < 1){
            rManager.RemoveResource(0, 6);
            rManager.RemoveResource(1, 6);
            rManager.RemoveResource(2, 12);
            gameState += 1;
            RunDialogue();
            Gardener = true;
            sManager.playerStats[0] += 15;
            sManager.playerStats[1] = sManager.playerStats[0];
            return true;
        }
        return false;
    }

    void RunDialogue()
    {

    }
}
