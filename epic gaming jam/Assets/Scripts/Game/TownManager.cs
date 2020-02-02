using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    public GameObject SlimeO;
    public GameObject Goblin;
    public bool Slime = false;
    public bool Gardener;
    public int gameState;
    int prevGameState;
    GameObject townSprite;
    ResourceManager rManager;
    // Start is called before the first frame update
    void Start()
    {
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
            SlimeO.SetActive(true);
        } else { Goblin.SetActive(false); }
        if(Gardener)
        {
            Goblin.SetActive(true);
        } else { Goblin.SetActive(false); }

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
            return true;
        }
        return false;
    }

    void RunDialogue()
    {

    }
}
