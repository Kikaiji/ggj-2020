using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

//creates the enemy and ally databases for battle
[System.Serializable]
public class Database : MonoBehaviour
{
    // Start is called before the first frame update
    private IList<Enemy> eDatabase;
    private JsonData enemyData;
    private IList<Ally> aDatabase;
    private JsonData allyData;
   
    void Start()
    {
        GameObject.DontDestroyOnLoad(this);
        enemyData = JsonMapper.ToObject(File.ReadAllText(Application.streamingAssetsPath + "/IO/enemies.json"));
        allyData = JsonMapper.ToObject(File.ReadAllText(Application.streamingAssetsPath + "/IO/ally.json"));
        ConstructEnemyDatabase();
        ConstructAllyDatabase();
        print(allyData);
        

    }


    public Enemy FetchEnemyByID(int id)
    {
        
        for(int i = 0; i < eDatabase.Count; i++)
        {
            if (eDatabase[i].ID == id) return eDatabase[i];
        }
        return null;
    }
    public Ally FetchAllyByID(int id)
    {
        for (int i = 0; i < aDatabase.Count; i++)
        {
            if (aDatabase[i].ID == id) return aDatabase[i];
        }
        return null;
    }

    public void ConstructEnemyDatabase()
    {
         
        for (int i = 0; i < enemyData.Count; i++)
        {
            Stats stats = new Stats((int)enemyData[i]["stats"]["attack"],
                (int)enemyData[i]["stats"]["defence"],
                (int)enemyData[i]["stats"]["speed"],
                (int)enemyData[i]["stats"]["health"],
                (int)enemyData[i]["stats"]["mana"]);

            eDatabase.Add(new Enemy(
                (int)enemyData[i]["id"],
                enemyData[i]["title"].ToString(),
                stats,
                (int)enemyData[i]["stage"],
                (int)enemyData[i]["level"],
                (int)enemyData[i]["type"],
                enemyData[i]["slug"].ToString()
                ));
        }
        print(enemyData[0]);
    }
    public void ConstructAllyDatabase()
    {
        for (int i = 0; i < allyData.Count; i++)
        {
            Ability ability = new Ability(
                (int)allyData[i]["ability"]["power"],
                (int)allyData[i]["ability"]["cost"],
                allyData[i]["ability"]["name"].ToString(),
                (int)allyData[i]["ability"]["type"]);

            Stats stats = new Stats(
                (int)enemyData[i]["stats"]["attack"],
                (int)enemyData[i]["stats"]["defence"],
                (int)enemyData[i]["stats"]["speed"],
                (int)enemyData[i]["stats"]["health"],
                (int)enemyData[i]["stats"]["mana"]);


            aDatabase.Add(new Ally(
                (int)enemyData[i]["id"],
                enemyData[i]["title"].ToString(),
                stats,
                ability,
                (int)enemyData[i]["stage"],
                (int)enemyData[i]["level"],
                (int)enemyData[i]["type"],
                enemyData[i]["slug"].ToString()
                )) ;
        }
        print(enemyData[0]);
    }
}

public class Stats
{
    public int Attack;
    public int Defense;
    public int Speed;
    public int Health;
    public int Mana;
    public Stats(int attack, int defense, int speed, int health, int mana)
    {
        this.Attack = attack;
        this.Defense = defense;
        this.Speed = speed;
        this.Health = health;
        this.Mana = mana;
    }

}

public class Ability
{
    public int Power;
    public int Cost;
    public string Name;
    public int Type;
    public Ability(int power, int cost, string name, int type)
    {
        this.Power = power;
        this.Cost = cost;
        this.Name = name;
        this.Type = type;
    }

}

[System.Serializable]
public class Enemy
{
    public int ID;
    public string Name;
    public Stats Stats;
    public int Stage;
    public int Level;
    public int Type;
    public string Slug;

    public Enemy(int id, string title, Stats stats, int stage, int level, int type, string slug)
    {
        this.ID = id;
        this.Name = title;
        this.Stats = stats;
        this.Stage = stage;
        this.Level = level;
        this.Type = type;
        this.Slug = slug;
    }

    public Enemy()
    {
        this.ID = -1;
    }
}

[System.Serializable]
public class Ally
{

    public int ID;
    public string Name;
    public Stats Stats;
    public Ability Ability;
    public int Stage;
    public int Level;
    public int Type;
    public string Slug;

    public Ally(int id, string title, Stats stats, Ability ability, int stage, int level, int type, string slug)
    {
        this.ID = id;
        this.Name = title;
        this.Stats = stats;
        this.Ability = ability;
        this.Stage = stage;
        this.Level = level;
        this.Type = type;
        this.Slug = slug;
    }

    public Ally()
    {
        this.ID = -1;
    }
}
