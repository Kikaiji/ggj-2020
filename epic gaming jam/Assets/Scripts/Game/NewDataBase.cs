using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NewDataBase : MonoBehaviour
{
    string pathEnemies;
    string jsonStringEnemies; 
    string pathAllies;
    string jsonStringAllies;

    // Start is called before the first frame update
    void Start()
    {
        //locate and read all text from the JSON file located in file streamingassets/IO/enemies.JSON
        pathEnemies = Application.streamingAssetsPath + "/IO/enemies.json";
        jsonStringEnemies = File.ReadAllText(pathEnemies);
        pathAllies = Application.streamingAssetsPath + "/IO/ally.json";
        jsonStringAllies = File.ReadAllText(pathAllies);

        //Create two new object arrys from its JSON representation.
        Enemies enemiesArray = JsonUtility.FromJson<Enemies>(jsonStringEnemies);
        Allies alliesArray= JsonUtility.FromJson<Allies>(jsonStringAllies);
        foreach (Enemy enemy in enemiesArray.enemies)
        {
            //Create an advnaced for loop that demonstates the JSON has been paresd 
            Debug.Log("Found enemy: " + enemy.title + " " + enemy.stats);
        }
        foreach (Ally ally in alliesArray.allies)
        {
            //Create an advnaced for loop that demonstates the JSON has been paresd 
            Debug.Log("Found ally: " + ally.title + " " + ally.level);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public class Enemy
    {
        //these variables are all case sensitive as they must match the strings in the JSON File.
        public int id;
        public string title;
        public int[] stats;
        public int stage;
        public int level;
        public int type;
        public string slug;

    }

    [System.Serializable]
    public class Enemies
    {
        //create an array of Enemy objects
        public Enemy[] enemies;
    }


    [System.Serializable]
    public class Ally
    {

        public int id;
        public string title;
        public int[] stats;
        public int[] ability;
        public int stage;
        public int level;
        public int type;
        public string slug;
    }

    [System.Serializable]
    public class Allies
    {
        //create an array of Ally objects
        public Ally[] allies;
    }

}
