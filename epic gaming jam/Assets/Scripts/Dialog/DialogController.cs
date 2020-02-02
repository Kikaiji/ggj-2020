using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private static DialogController instance = null;

    public Text text;
    public TownManager townManager;

    private IList<DialogEvent> dialogEvents;
    private int dialogIndex;
    private bool waitingForKeypress = false;
    private bool runningDialog = false;
    private bool skipDialog = false;
    private GameObject activePortrait;

    public GameObject nomadPortrait;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        //if (!townManager.tutorial && townManager.gameState == 0)
        print(townManager.Slime);
        if(townManager.Slime == false)
        {
            print("execute");
            string filePath = "Assets/Dialog/IntroDialogue_01.txt";
            LoadDialog(filePath);
            ExecuteDialog(dialogEvents[0]);
        }
    }

    public void LoadDialog(string filePath)
    {
        dialogIndex = 0;
        dialogEvents = DialogController.ParseFile(filePath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (waitingForKeypress)
            {
                waitingForKeypress = false;
                if (activePortrait != null)
                {
                    activePortrait.SetActive(false);
                }
                dialogIndex++;

                if (dialogIndex < dialogEvents.Count)
                {
                    ExecuteDialog(dialogEvents[dialogIndex]);
                }
                else
                {
                    Hide();
                }
            } 
            else if (runningDialog)
            {
                skipDialog = true;
            }
        }
    }

    public static IList<DialogEvent> ParseFile(string filepath)
    {
        IList<DialogEvent> dialogEvents = new List<DialogEvent>();
        StreamReader reader = new StreamReader(filepath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (line.Contains(":"))
            {
                string[] person = line.Split(':');
                dialogEvents.Add(new Dialog(person[0], line));
            }
            else if (line.StartsWith("#"))
            {
                dialogEvents.Add(new Action(line.Substring(1)));
            }
            else if (line.StartsWith("("))
            {

            }
            else if (line.StartsWith("*"))
            {

            }
            else
            {
                Debug.Log("Can't parse line " + line); 
            }

            //Debug.Log(line);
        }
        reader.Close();
        return dialogEvents;
    }

    void ExecuteDialog(DialogEvent dialogEvent)
    {
        if (dialogEvent is Dialog)
        {
            StartCoroutine(RunDialog(dialogEvent as Dialog)); 
        }
    }

    IEnumerator RunDialog(Dialog dialog)
    {
        runningDialog = true;
        activePortrait = portraitForName(dialog.person);
        if (activePortrait != null)
        {
            activePortrait.SetActive(true);
        }

        for (int j = 0; j < dialog.dialog.Length; j++)
        {
            if (skipDialog)
            {
                j = dialog.dialog.Length - 1;
                skipDialog = false;
            }
            text.text = dialog.dialog.Substring(0, j);
            yield return new WaitForSeconds(0.05f);
        }
        waitingForKeypress = true;
        runningDialog = false;
    }

    GameObject portraitForName(string name)
    {
        switch (name)
        {
            case "Mysterious voice":
                return null;
            case "Nomad":
                return nomadPortrait;
            default:
                return null;
        }
    }

    void Hide()
    {
        this.gameObject.SetActive(false);
    }
}

public class DialogEvent
{

}

public class Dialog : DialogEvent
{
    public string person;
    public string dialog;
    public Dialog(string person, string dialog)
    {
        this.person = person;
        this.dialog = dialog;
    }
}

public class Action : DialogEvent
{
    public string action;
    public Action(string action)
    {
        this.action = action;
    }
}