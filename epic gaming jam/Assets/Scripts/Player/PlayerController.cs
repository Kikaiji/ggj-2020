using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject dCanvas;
    [SerializeField]
    Text eventText;
    public int xpos;
    public int ypos;
    LayerMask layerMask;
    ResourceManager rManager;
    GameManager manager;

    // Start is called before the first frame update
    Vector3 pos;                                // For movement
    float speed = 5f;                         // Speed of movement

    void Start()
    {
        //dCanvas = GameObject.Find("DungeonCanvas");
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        eventText.text = "Started an Expedition!";
        rManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        layerMask = LayerMask.GetMask("Wall", "Tile");
        pos = transform.position;          // Take the initial position
    }

    void FixedUpdate()
    {
        if (manager.state == GameState.VENTURE)
        {
            RaycastHit2D hit;
            if (Input.GetKey(KeyCode.A) && transform.position == pos)
            {        // Left
                hit = Physics2D.Raycast(transform.position, Vector3.left, 1f, layerMask);
                if (hit.collider != null)
                {
                    switch (hit.collider.tag)
                    {
                        case "Wall":
                            break;
                        case "Enemy":
                            pos += Vector3.left;
                            hit.transform.gameObject.SetActive(false);
                            EnemyEvent();
                            break;
                        case "Resource":
                            pos += Vector3.left;
                            hit.transform.gameObject.SetActive(false);
                            ResourceEvent();
                            break;
                        case "Special":
                            pos += Vector3.left;
                            hit.transform.gameObject.SetActive(false);
                            SpecialEvent();
                            break;
                    }
                }
                else { pos += Vector3.left; }
            }
            if (Input.GetKey(KeyCode.D) && transform.position == pos)
            {        // Right
                hit = Physics2D.Raycast(transform.position, Vector3.right, 1f, layerMask);
                if (hit.collider != null)
                {
                    switch (hit.collider.tag)
                    {
                        case "Wall":
                            break;
                        case "Enemy":
                            pos += Vector3.right;
                            hit.transform.gameObject.SetActive(false);
                            EnemyEvent();
                            break;
                        case "Resource":
                            pos += Vector3.right;
                            hit.transform.gameObject.SetActive(false);
                            ResourceEvent();
                            break;
                        case "Special":
                            pos += Vector3.right;
                            hit.transform.gameObject.SetActive(false);
                            SpecialEvent();
                            break;
                    }
                }
                else { pos += Vector3.right; }
            }
            if (Input.GetKey(KeyCode.W) && transform.position == pos)
            {        // Up
                hit = Physics2D.Raycast(transform.position, Vector3.up, 1f, layerMask);
                if (hit.collider != null)
                {
                    switch (hit.collider.tag)
                    {
                        case "Wall":
                            break;
                        case "Enemy":
                            pos += Vector3.up;
                            hit.transform.gameObject.SetActive(false);
                            EnemyEvent();
                            break;
                        case "Resource":
                            pos += Vector3.up;
                            hit.transform.gameObject.SetActive(false);
                            ResourceEvent();
                            break;
                        case "Special":
                            pos += Vector3.up;
                            hit.transform.gameObject.SetActive(false);
                            SpecialEvent();
                            break;
                    }
                }
                else { pos += Vector3.up; }
            }
            if (Input.GetKey(KeyCode.S) && transform.position == pos)
            {        // Down
                hit = Physics2D.Raycast(transform.position, Vector3.down, 1f, layerMask);
                if (hit.collider != null)
                {
                    switch (hit.collider.tag)
                    {
                        case "Wall":
                            break;
                        case "Enemy":
                            pos += Vector3.down;
                            hit.transform.gameObject.SetActive(false);
                            EnemyEvent();
                            break;
                        case "Resource":
                            pos += Vector3.down;
                            hit.transform.gameObject.SetActive(false);
                            ResourceEvent();
                            break;
                        case "Special":
                            pos += Vector3.down;
                            hit.transform.gameObject.SetActive(false);
                            SpecialEvent();
                            break;
                    }
                }
                else { pos += Vector3.down; }
            }
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
            if(manager.state == GameState.VENTURE)
            {
                dCanvas.SetActive(true);
            }
        }
    }

    void EnemyEvent()
    {
        print("enemy event");
        manager.state = GameState.BATTLE;
        dCanvas.SetActive(false);
        SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);
        
    }

    void ResourceEvent()
    {
        print("resource event");
        int resource = Random.Range(0, 3);
        string name = "";
        switch (resource)
        {
            case 0:
                name = " Water";
                break;
            case 1:
                name = " Food";
                break;
            case 2:
                name = " Wood";
                break;
        }
        int number = Random.Range(1, 4);
        rManager.resources[resource] += number;
        eventText.text = "Obtained " + number.ToString() + name + "!";
    }

    void SpecialEvent()
    {
        print("special event");
        string[] text = new string[3];
        for (int i = 0; i < 3; i++)
        {
            int resource = Random.Range(0, 3);
            string name = "";
            switch (resource)
            {
                case 0:
                    name = " Water";
                    break;
                case 1:
                    name = " Food";
                    break;
                case 2:
                    name = " Wood";
                    break;
            }
            int number = Random.Range(1, 4);
            rManager.resources[resource] += number;
            text[i] = "Obtained " + number.ToString() + name + "!";
        }
        eventText.text = text[0] + "\n" + text[1] + "\n" + text[2];
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        print(other.tag);
        switch (other.tag)
        {
            case "Enemy":
                print("enemy");
                break;
            case "Resource":
                print("Resource");
                break;
            case "Special":
                print("Special");
                break;
        }
    }
}

