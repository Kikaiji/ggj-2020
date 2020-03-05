using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTile : MonoBehaviour
{
    //not finished, the script for the tiles that summon enemies
    [SerializeField]
    bool set;
    [SerializeField]
    public int enemyID;
    [SerializeField]
    public int enemyID1;
    [SerializeField]
    public int enemyID2;
    //determines the enemies summoned, set in editor
    //see enemies.json for what id is what
    private void Start()
    {
        if(set == false)
        {
            if (enemyID == -1)
            { //enemyID = Random.Range()
            }
            if (enemyID1 == -1)
            { //enemyID1 = Random.Range()
            }
            if (enemyID2 == -1)
            { //enemyID2 = Random.Range()
            }
        }

    }

}
