using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}
public class BattleManager : MonoBehaviour
{

    GameManager manager;
    GameObject dCanvas;

    TownManager tManager;

    public bool ally;
    public GameObject allyPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

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
        tManager = GameObject.Find("TownManager").GetComponent<TownManager>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        state = BattleState.START;
        Debug.Log(SceneManager.GetSceneAt(1).name);
        StartCoroutine(SetUpBattle());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { playerUnit.anim.speed = 1; }
    }

    IEnumerator SetUpBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerStation);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab, enemyStation);
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

    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            consoleText.text = "You've defeated the " + enemyUnit.unitName + "!";
            yield return new WaitForSeconds(3f);
            manager.state = GameState.VENTURE;
            tManager.Slime = true;
            SceneManager.UnloadSceneAsync("BattleScene");
        }else if (state == BattleState.LOST)
        {
            consoleText.text = "You've been defeated by the " + enemyUnit.unitName + "!";
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
            playerUnit.Heal(playerUnit.Defense, 3);

            playerHud.SetHp(playerUnit.CurrentHP);
            playerHud.SetMp(playerUnit.CurrentMP);

            yield return new WaitForSeconds(2f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyAttack());
        }
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
            StartCoroutine(PlayerAttack());
        }
    }
}
