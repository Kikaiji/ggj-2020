using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private static DialogController instance = null;

    public GameObject image;

    public Text text;

    public TownManager townManager;
    public string file;

    private IList<DialogEvent> dialogEvents;
    private int dialogIndex;
    private bool waitingForKeypress = false;
    private bool runningDialog = false;
    private bool skipDialog = false;
    public GameObject activePortrait;

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
        if(townManager.Slime == false)
        {
            string filePathSuffix = "/Dialog/IntroDialogue_01.txt";
            string filePath = Application.streamingAssetsPath + filePathSuffix;

            image.SetActive(true);
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

    public static List<DialogEvent> ParseFile(string filepath)
    {
        List<DialogEvent> dialogEvents = new List<DialogEvent>();
        StreamReader reader = new StreamReader(filepath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            if (line.Contains(":"))
            {
                string[] person = line.Split(':');
                dialogEvents.Add(new Dialogs(person[0], line));
            }
            else if (line.StartsWith("#"))
            {
                dialogEvents.Add(new Action(line.Substring(1)));
            }
            else if (line.StartsWith("("))
            {
                //used as test suite intro_dialog_test.txt. Currently not used
            }
            else if (line.StartsWith("*"))
            {
                //used as test suite i intro_dialog_test.txt. Currently not used
            }
            else
            {
                Debug.Log("Can't parse line " + line); 
            }

            
        }
        //print(dialogEvents.);
        reader.Close();
        return dialogEvents;
    }

    void ExecuteDialog(DialogEvent dialogEvent)
    {
        if (dialogEvent is Dialogs)
        {
            StartCoroutine(RunDialog(dialogEvent as Dialogs)); 
        }
    }

    IEnumerator RunDialog(Dialogs dialog)
    {
        runningDialog = true;
        activePortrait = portraitForName(dialog.Person);
        if (activePortrait != null)
        {
            activePortrait.SetActive(true);
        }

        
        for (int j = 0; j <= dialog.Dialog.Length; j++)
        {
            if (skipDialog)
            {
                j = dialog.Dialog.Length - 1;
                skipDialog = false;
            }
            text.text = dialog.Dialog.Substring(0, j);
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
        image.SetActive(false);
        this.gameObject.SetActive(false);
    }
}

public class DialogEvent
{

}

public class Dialogs : DialogEvent
{

    private string person;
    private string dialog;


    public Dialogs(string person, string dialog)
    {
        this.person = person;
        this.dialog = dialog;
    }

    //Create getter and setters for variable Person
    public string Person
    {
        get
        {
            //Some other code
            return person;
        }
        set
        {
            //Some other code
            person = value;
        }
    }

    //Create getter and setters for variable Dialog
    public string Dialog
    {
        get
        {
            //Some other code
            return dialog;
        }
        set
        {
            //Some other code
            dialog = value;
        }
    }
}

public class Action : DialogEvent
{
    private string action;
    public Action(string action)
    {
        this.action = action;
    }
}