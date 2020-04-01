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
    string enemyUnitName;
    int enemyUnitAttack;

    /* This is the currently the main Database script within the game. It takes JSON data that is located in the IO folder of the project.
     * It then parses that data and creates three seperate enemy objects. However I have run out of time in translating those objects into 
     * game objects. Curruntly there are ConstructEnemyName() and ConstructEnemyAttack() which returns the LAST enemy name and enemy attack
     * as the for loop overwrites it until the last enemy is pasted. There is a way to get the stats of each of the enemies as demonstarted in
     * each method. 
     */



    // Start is called before the first frame update
    void Start()
    {

        /* The  code inside start is only to demonstate that the JSON has been parsed, and to print it to the cosnole log. 
         * It serves no function inside the game
         */


        //locate and read all text from the JSON file located in file streamingassets/IO/enemies.JSON
        pathEnemies = Application.streamingAssetsPath + "/IO/enemies.json";
        jsonStringEnemies = File.ReadAllText(pathEnemies);
        pathAllies = Application.streamingAssetsPath + "/IO/ally.json";
        jsonStringAllies = File.ReadAllText(pathAllies);

        //Create two new object arrys from its JSON representation.
        Enemies enemiesArray = JsonUtility.FromJson<Enemies>(jsonStringEnemies);
        Allies alliesArray = JsonUtility.FromJson<Allies>(jsonStringAllies);
        foreach (Enemy enemy in enemiesArray.enemies)
        {
            //Create an advnaced for loop that demonstates the JSON has been paresd 
            Debug.Log("Found enemy: " + enemy.title + " " + enemy.attack);
        }
        foreach (Ally ally in alliesArray.allies)
        {
            //Create an advnaced for loop that demonstates the JSON has been paresd 
            Debug.Log("Found ally: " + ally.title + " " + ally.level);
        }

    }


    //The ConstructEnemyName() which parses the JSON data and retuns just the last enemy name in the JSON
    public string ConstructEnemyName()
    {
        pathEnemies = Application.streamingAssetsPath + "/IO/enemies.json";
        jsonStringEnemies = File.ReadAllText(pathEnemies);
        Enemies enemiesArray = JsonUtility.FromJson<Enemies>(jsonStringEnemies);
        foreach (Enemy enemy in enemiesArray.enemies)
        {
            //Create an advnaced for loop that demonstates the JSON has been paresd 
            Debug.Log("Found enemy: " + enemy.title + " " + enemy.attack);
            enemyUnitName = enemy.title;

            //If you want a workaround to access the other varaibles of the other enemy objects use the below equation 
            //enemyUnitName = enemiesArray.enemies[0].title;


        }
        return enemyUnitName;

    }

    //The ConstructEnemyAttack() which parses the JSON data and retuns just the last enemy attack in the JSON
    public int ConstructEnemyAttack()
    {
        pathEnemies = Application.streamingAssetsPath + "/IO/enemies.json";
        jsonStringEnemies = File.ReadAllText(pathEnemies);
        Enemies enemiesArray = JsonUtility.FromJson<Enemies>(jsonStringEnemies);
        foreach (Enemy enemy in enemiesArray.enemies)
        {
            //Create an advnaced for loop that demonstates the JSON has been paresd 
            Debug.Log("Found enemy: " + enemy.title + " " + enemy.attack);
            enemyUnitAttack = enemy.attack;

            //If you want a workaround to access the other varaibles of the other enemy objects use the below equation 
            //enemyUnitAttack = enemiesArray.enemies[0].attack;


        }
        return enemyUnitAttack;

    }



    [System.Serializable]
    public class Enemy
    {
        //these variables are all case sensitive as they must match the strings in the JSON File.
        public int id;
        public string title;
        public int attack;
        public int defence;
        public int speed;
        public int health;
        public int mana;
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
        //these variables are all case sensitive as they must match the strings in the JSON File.
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
