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
    Database database;
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

    GameObject AllyAction;
    GameObject PlayerAction;

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
    public void Start()
    {
        AllyAction = GameObject.Find("Ally Action Box");
        PlayerAction = GameObject.Find("Player Action Box");
        database = GameObject.Find("Database").GetComponent<Database>();
        rManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        tManager = GameObject.Find("TownManager").GetComponent<TownManager>();
        tutorial = tManager.tutorial;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pcontroller = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyID = pcontroller.enemyID;
        enemypositions = GameObject.Find("EnemyGrid");
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
    }

    void Update()
    {
        if (state == BattleState.PLAYERTURN)
        {
            PlayerAction.SetActive(true);
            AllyAction.SetActive(false);
        }
        else if (state == BattleState.ALLYTURN)
        {
            PlayerAction.SetActive(false);
            AllyAction.SetActive(true);
        }
        else
        {
            PlayerAction.SetActive(false);
            AllyAction.SetActive(false);
        }
    }

    IEnumerator SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerStation);
        if (tManager.Gardener) { GameObject allyGO = Instantiate(allyPrefab, playerStation); allyGO.transform.position = new Vector3(playerStation.transform.position.x - 3, playerStation.transform.position.y - .5f); }
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab);
        enemyUnit = enemyGO.GetComponent<Unit>();
        Enemy enemy = database.FetchEnemyByID(enemyID);

        enemyUnit.Attack = enemy.Stats.Attack;
        enemyUnit.Defense = enemy.Stats.Defense;
        enemyUnit.Speed = enemy.Stats.Speed;
        enemyUnit.unitName = enemy.Name;
        enemyUnit.MaxHP = enemy.Stats.Health;
        enemyUnit.MaxMP = enemy.Stats.Mana;

        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] != null)
            {
                enemyGO.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Graphics/EnemySprites/" + enemy.Slug);
            }
        }

        consoleText.text = "A " + enemyUnit.unitName + " Approaches";

        playerHud.SetHUD(playerUnit);
        enemyHud.SetHUD(enemyUnit);
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        if( playerUnit.Speed >= enemyUnit.Speed) { PlayerTurn(); }
        else { EnemyAttack(); }

    }

    void SetEnemyPosition(GameObject enemy)
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

    void PlayerTurn()
    {
        PlayerAction.SetActive(true);
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
            state = BattleState.PROCESSING;
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
            if (playerUnit.CurrentMP < 3)
            {
                state = BattleState.PROCESSING;
                bool y = playerUnit.Heal(playerUnit.Defense, 3);
                if (y) { consoleText.text = "The move was successful!"; }
                playerHud.SetHp(playerUnit.CurrentHP);
                playerHud.SetMp(playerUnit.CurrentMP);

                yield return new WaitForSeconds(2f);

                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyAttack());
            }
        }
    }

    IEnumerator AllyAttack()
    {
        print("goblin");
        if (state == BattleState.PLAYERTURN && tManager.Gardener == true)
        {
            state = BattleState.PROCESSING;
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
        state = BattleState.PROCESSING;
        for (int i = 0; i < rManager.resources.Length; i++)
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
        state = BattleState.PROCESSING;
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
