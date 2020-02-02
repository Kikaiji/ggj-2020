using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleManager : MonoBehaviour
{
    int enemyID;
    bool tutorial;
    GameManager manager;
    GameObject dCanvas;
    ResourceManager rManager;
    PlayerController pcontroller;

    TownManager tManager;

    public bool ally;
    public GameObject allyPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;

    public Transform playerStation;
    public Transform enemyStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHud playerHud;
    public BattleHud enemyHud;

    public Text consoleText;

    public BattleState state;
    // Start is called before the first frame update
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }
    public void Start()
    {

        rManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tManager = GameObject.Find("TownManager").GetComponent<TownManager>();
        tutorial = tManager.tutorial;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pcontroller = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyID = pcontroller.enemyID;
        state = BattleState.START;
        Debug.Log(SceneManager.GetSceneAt(1).name);
        StartCoroutine(SetUpBattle());
    }

    void Update()
    {
        print(enemyID);
        if (Input.GetKeyDown(KeyCode.A)) { playerUnit.anim.speed = 1; }
    }

    IEnumerator SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerStation);
        if (tManager.Gardener) { GameObject allyGO = Instantiate(allyPrefab, playerStation); allyGO.transform.position = new Vector3(playerStation.transform.position.x - 3, playerStation.transform.position.y - .5f); }
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = new GameObject();
        switch (enemyID)
        {
            case 0:
                enemyGO = Instantiate(enemyPrefab, enemyStation);
                break;
            case 1:
                enemyGO = Instantiate(enemyPrefab2, enemyStation);
                break;
            case 2:
                enemyGO = Instantiate(enemyPrefab3, enemyStation);
                break;
        }
        
        enemyUnit = enemyGO.GetComponent<Unit>();

        consoleText.text = "A " + enemyUnit.unitName + " Approaches";

        playerHud.SetHUD(playerUnit);
        enemyHud.SetHUD(enemyUnit);
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        if( playerUnit.Speed >= enemyUnit.Speed) { PlayerTurn(); }
        else { EnemyAttack(); }
        
    }

    void PlayerTurn()
    {
        consoleText.text = "Choose an Action: ";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

    public void OnAssistButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(AllyAttack());
    }

    public void OnRunButton()
    {
        if (state != BattleState.PLAYERTURN || (tutorial == true))
            return;

        StartCoroutine(PlayerRun());
    }

    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            consoleText.text = "You've defeated the " + enemyUnit.unitName + "!";
            yield return new WaitForSeconds(3f);
            manager.state = GameState.VENTURE;
            switch (enemyID)
            {
                case 0:
                    tManager.Slime = true;
                    break;
                case 1:
                    tManager.Ghost = true;
                    break;
                case 2:
                    tManager.Minotaur = true;
                    break;
            }
            rManager.resources[4] += 1;
            if (tutorial) {
                tManager.tutorial = false;
                tutorial = false;
                SceneManager.UnloadSceneAsync("BattleScene");
                SceneManager.UnloadSceneAsync("DungeonScene");
                SceneManager.LoadSceneAsync("TownScene");
                playerUnit.CurrentMP = playerUnit.MaxMP;
                playerUnit.CurrentHP = playerUnit.MaxHP;
            }
            else { playerUnit.CurrentHP += ((playerUnit.MaxHP / 10) * 3); SceneManager.UnloadSceneAsync("BattleScene"); }
            
        }else if (state == BattleState.LOST)
        {
            consoleText.text = "You've been defeated by the " + enemyUnit.unitName + "!";
            playerUnit.CurrentMP = playerUnit.MaxMP;
            playerUnit.CurrentHP = playerUnit.MaxHP;
            yield return new WaitForSeconds(3f);
            manager.state = GameState.TOWN;
            SceneManager.LoadSceneAsync("TownScene");
        }
    }

    IEnumerator PlayerAttack()
    {
        if (state == BattleState.PLAYERTURN)
        {
            playerUnit.anim.Play("NomadBattleAnim");
            bool isdead = enemyUnit.TakeDamage(playerUnit.Attack);
            yield return new WaitForSeconds(1f);
            enemyHud.SetHp(enemyUnit.CurrentHP);
            consoleText.text = enemyUnit.unitName + " took " + playerUnit.Attack + "damage!";
            yield return new WaitForSeconds(2f);
            enemyHud.SetHp(enemyUnit.CurrentHP);
            playerHud.SetMp(playerUnit.CurrentMP);

            if (isdead)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyAttack());
            }
        }
    }

    IEnumerator PlayerHeal()
    {
        if (state == BattleState.PLAYERTURN)
        {
            bool y = playerUnit.Heal(playerUnit.Defense, 3);
            if (y) { consoleText.text = "The move was successful!"; }
            playerHud.SetHp(playerUnit.CurrentHP);
            playerHud.SetMp(playerUnit.CurrentMP);

            yield return new WaitForSeconds(2f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyAttack());
        }
    }

    IEnumerator AllyAttack()
    {
        print("goblin");
        if (state == BattleState.PLAYERTURN && tManager.Gardener == true)
        {
            playerUnit.anim.Play("NomadBattleAnim");
            bool isdead = enemyUnit.TakeDamage(playerUnit.Attack * 2);
            playerUnit.TakeMP(4);
            playerHud.SetMp(playerUnit.CurrentMP);
            yield return new WaitForSeconds(0.5f);
            enemyHud.SetHp(enemyUnit.CurrentHP);
            consoleText.text = enemyUnit.unitName + " took " + playerUnit.Attack + "damage!";
            yield return new WaitForSeconds(1f);
            enemyHud.SetHp(enemyUnit.CurrentHP);
            

            if (isdead)
            {
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyAttack());
            }
        }
    }

    IEnumerator PlayerRun()
    {
        for(int i = 0; i < rManager.resources.Length; i++)
        {
            rManager.resources[i] = (rManager.resources[i] * 3) / 4;
        }
        consoleText.text = "You ran away!";
        yield return new WaitForSeconds(2f);
        SceneManager.UnloadSceneAsync("DungeonScene");
        SceneManager.UnloadSceneAsync("BattleScene");
        SceneManager.LoadSceneAsync("TownScene");
    }

    IEnumerator EnemyAttack()
    {
        consoleText.text = enemyUnit.unitName + " attacks!";
        yield return new WaitForSeconds(1f);
        bool isDead = playerUnit.TakeDamage(enemyUnit.Attack);
        playerHud.SetHp(playerUnit.CurrentHP);
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            consoleText.text = "Choose an Option:";
        }
    }
}
