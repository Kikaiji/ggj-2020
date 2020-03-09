using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//manages the townstates, says when the slime gardener etc need to be active.

//probably needs cleaning up
public class TownManager : MonoBehaviour
{
    private static TownManager playerInstance;
        
    DialogController dController;
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
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
    void Start()
    {
        sManager = GameObject.Find("StatManager").GetComponent<StatManager>();
        tutorial = true;
        rManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        townSprite = GameObject.Find("TownSprite");
        gameState = 0;
        prevGameState = gameState;
        townSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/TownStates/nomadvillage_state" + gameState);
        if(Slime == false)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //checks if each thing is active, tries to make sure that the references are still about
        //print(SceneManager.GetActiveScene().name);
        if (townSprite == null) { townSprite = GameObject.Find("TownSprite"); }
        if (SlimeO == null && SceneManager.GetActiveScene().name == "TownScene") { SlimeO = GameObject.Find("Slime");  }
        if (Goblin == null && SceneManager.GetActiveScene().name == "TownScene") { Goblin = GameObject.Find("Goblin");  }
        if (GhostO == null && SceneManager.GetActiveScene().name == "TownScene") { GhostO = GameObject.Find("Ghost"); }
        if(SceneManager.GetActiveScene().name == "TownScene") townSprite.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/TownStates/nomadvillage_state" + gameState);
        
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
        //very temporary, for the build menu button, only builds the garden. will need to fileIO this in the future
        if(rManager.CheckForResource(0, 6) && rManager.CheckForResource(1, 6) && rManager.CheckForResource(2, 12) && gameState < 1){
            rManager.RemoveResource(0, 6);
            rManager.RemoveResource(1, 6);
            rManager.RemoveResource(2, 12);
            gameState += 1;
            //RunDialogue();
            Gardener = true;
            sManager.playerStats[0] += 15;
            sManager.playerStats[1] = sManager.playerStats[0];
            return true;
        }
        return false;
    }
}
