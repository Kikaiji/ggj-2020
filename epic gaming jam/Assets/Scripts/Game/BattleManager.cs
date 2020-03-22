using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// a lot
public enum BattleState { START, PLAYERTURN, ALLYTURN, ENEMYTURN, PROCESSING, WON, LOST}

public class BattleManager : MonoBehaviour
{
    
    int enemyID;
    bool tutorial;
    Stats stats;
    GameManager manager;
    GameObject dCanvas;
     
    ResourceManager rManager;
    PlayerController pcontroller;

    TownManager tManager;
    Database database;
    public bool ally;
    public GameObject allyPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;

    public GameObject AllyAction;
    public GameObject PlayerAction;

    public GameObject enemypositions;
    public GameObject[] enemies = new GameObject[9];
    public GameObject[] turnOrder = new GameObject[11];

    public Transform playerStation;
    public Transform enemyStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHud playerHud;
    public BattleHud enemyHud;

    public Text consoleText;

    public BattleState state;

    void Awake()
    {
        database = GetComponent<Database>();
        rManager = GetComponent<ResourceManager>();
        tManager = GetComponent<TownManager>();
        manager = GetComponent<GameManager>();
        pcontroller = GetComponent<PlayerController>();
    }
    public void Start()
    {
        DontDestroyOnLoad(PlayerAction);
        DontDestroyOnLoad(AllyAction);
        state = BattleState.START;
        tutorial = tManager.tutorial;
        //enemyID = pcontroller.enemyID;
        //enemypositions = GameObject.Find("EnemyGrid");
        //state = BattleState.START;
        SetUpBattle();
    }


    void Update()
    {
        //OnAttackButton();


        if (state == BattleState.PLAYERTURN)
        {
            PlayerAction.gameObject.SetActive(true);
            AllyAction.gameObject.SetActive(false);
            print("i am update, first if statment");
        }
        else if (state == BattleState.ALLYTURN)
        {
            PlayerAction.gameObject.SetActive(false);
            AllyAction.gameObject.SetActive(true);
            print("i am update, second if statment");
            //AllyAttack();


        }
        else
        {
            PlayerAction.gameObject.SetActive(false);
            AllyAction.gameObject.SetActive(false);

        }
    }

    void SetUpBattle()
    {
        Debug.Log("I am in SetUpbattle");
        GameObject playerGO = Instantiate(playerPrefab, playerStation);
        if (tManager.Gardener) { GameObject allyGO = Instantiate(allyPrefab, playerStation); allyGO.transform.position = new Vector3(playerStation.transform.position.x - 3, playerStation.transform.position.y - .5f); }
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab, enemyStation);
        //Enemy enemy = database.FetchEnemyByID(enemyID);
        //database.ConstructAllyDatabase();



        /*enemyUnit.Attack = enemy.Stats.Attack;
        enemyUnit.Defense = enemy.Stats.Defense;
        enemyUnit.Speed = enemy.Stats.Speed;
        enemyUnit.unitName = enemy.Name;
        enemyUnit.MaxHP = enemy.Stats.Health;
        enemyUnit.MaxMP = enemy.Stats.Mana;
        */
        /*
        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
            {
                //enemyGO.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Graphics/EnemySprites/" + enemy.Slug);
            }
        }
        */
        //consoleText.text = "A " + enemyUnit.unitName + " Approaches";

        //playerHud.SetHUD(playerUnit);
        //enemyHud.SetHUD(enemyUnit);
        //yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        print(state);
        PlayerTurn();
        //if( playerUnit.Speed >= enemyUnit.Speed) { PlayerTurn(); }
        //else { EnemyAttack(); }

    }

    /*void SetEnemyPosition(GameObject enemy)
    {
        bool state = false;
        if (!state)
        {
            int rand = Random.Range(0, 9);
            if (enemies[rand] == null)
            {
                enemies[rand] = enemy;
                state = true;
            }
        }
    }
    */

    public void PlayerTurn()
    {
        //PlayerAction.SetActive(true);
        consoleText.text = "Choose an Action: ";
    }

    public void OnAttackButton()
    {
       
        
            PlayerAttack();
            print("I am back in onattackbutton");
            
        
    }

    public void OnAllyAttackButton()
    {


        AllyAttack();
        print("I am back in onattackbutton");


    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

    public void OnAssistButton()
    {
        //if (state != BattleState.PLAYERTURN)
            //return;

        AllyAttack();
    }

    public void OnRunButton()
    {
        
        //if (state != BattleState.PLAYERTURN || (tutorial == true))
           // return;

        PlayerRun();
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            consoleText.text = "You've defeated the "; 
            //yield return new WaitForSeconds(3f);
            //manager.state = GameState.VENTURE;
            //SceneManager.UnloadSceneAsync("BattleScene");
            //SceneManager.UnloadSceneAsync("DungeonScene");
            SceneManager.LoadSceneAsync("TownScene");
           /*(switch (enemyID)
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
                //playerUnit.CurrentMP = playerUnit.MaxMP;
               // playerUnit.CurrentHP = playerUnit.MaxHP;
            }
            //else { playerUnit.CurrentHP += ((playerUnit.MaxHP / 10) * 3); SceneManager.UnloadSceneAsync("BattleScene"); }
            
        }else if (state == BattleState.LOST)
        {
            consoleText.text = "You've been defeated by the " + enemyUnit.unitName + "!";
            playerUnit.CurrentMP = playerUnit.MaxMP;
            playerUnit.CurrentHP = playerUnit.MaxHP;
            //yield return new WaitForSeconds(3f);
            manager.state = GameState.TOWN;
            SceneManager.LoadSceneAsync("TownScene");
            */
        }
    }

    void PlayerAttack()
    {
        Debug.Log("I am in attack function");
        consoleText.text = "You attacked";
        if (state == BattleState.PLAYERTURN)
        {
            
            Debug.Log("I am past PlayerAttack");
            print(state);

        //playerUnit.anim.Play("NomadBattleAnim");
        //bool isdead = enemyUnit.TakeDamage(playerUnit.Attack);
        //yield return new WaitForSeconds(1f);
        //enemyHud.SetHp(enemyUnit.CurrentHP);
        //consoleText.text = /*enemyUnit.unitName +*/ " took " + /*playerUnit.Attack + */ "damage!";
        //yield return new WaitForSeconds(2f);
        //enemyHud.SetHp(enemyUnit.CurrentHP);
        //playerHud.SetMp(playerUnit.CurrentMP);

        //if (isdead)
        //{
        //state = BattleState.WON;
        //StartCoroutine(EndBattle());
        //}
        //else
        //{
        //state = BattleState.ENEMYTURN;
        //StartCoroutine(EnemyAttack());
        // }
        }
        EnemyAttack();

    }




    IEnumerator PlayerHeal()
    {
        if (state == BattleState.PLAYERTURN)
        {
            Debug.Log("I am in playerturn");
            if (playerUnit.CurrentMP < 3)
            {
                state = BattleState.PROCESSING;
                bool y = playerUnit.Heal(playerUnit.Defense, 3);
                if (y) { consoleText.text = "The move was successful!"; }
                //playerHud.SetHp(playerUnit.CurrentHP);
                //.SetMp(playerUnit.CurrentMP);

                yield return new WaitForSeconds(2f);

                state = BattleState.ENEMYTURN;
                EnemyAttack();
            }
        }
    }

    void AllyAttack()
    {
        print("goblin");
        Debug.Log("I am in AllyAttack");
        consoleText.text = "Ally attacked";
        state = BattleState.PLAYERTURN;
        

        /*
        if (state == BattleState.PLAYERTURN && tManager.Gardener == true)
        {
            state = BattleState.PROCESSING;
            //playerUnit.anim.Play("NomadBattleAnim");
           // bool isdead = enemyUnit.TakeDamage(playerUnit.Attack * 2);
            //playerUnit.TakeMP(4);
            //playerHud.SetMp(playerUnit.CurrentMP);
            //yield return new WaitForSeconds(0.5f);
            //enemyHud.SetHp(enemyUnit.CurrentHP);
            //consoleText.text = enemyUnit.unitName + " took " + playerUnit.Attack + "damage!";
            //yield return new WaitForSeconds(1f);
           // enemyHud.SetHp(enemyUnit.CurrentHP);
            

            if (isdead)
            {
                state = BattleState.WON;
                //StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.ENEMYTURN;
                EnemyAttack();
            }
        }
        */
    }

    void PlayerRun()
    {
        Debug.Log("I am in run function");
        state = BattleState.PROCESSING;
        Debug.Log(state);
        //for (int i = 0; i < rManager.resources.Length; i++)
        //{
            //rManager.resources[i] = (rManager.resources[i] * 3) / 4;
        //}
        consoleText.text = "You ran away!";
        
        //SceneManager.UnloadSceneAsync("DungeonScene");
        //SceneManager.UnloadSceneAsync("BattleScene");
        SceneManager.LoadSceneAsync("TownScene");
    }

    void EnemyAttack()
    {
        Debug.Log("I am in enemyAttack")
;       state = BattleState.PROCESSING;
        consoleText.text = "attacks!";
        //yield return new WaitForSeconds(1f);
        //bool isDead = playerUnit.TakeDamage(enemyUnit.Attack);
        //playerHud.SetHp(playerUnit.CurrentHP);
        //yield return new WaitForSeconds(1f);

        /*if (isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            consoleText.text = "Choose an Option:";
        }
        */
        state = BattleState.ALLYTURN;
        //EndBattle(); 
    }
}
