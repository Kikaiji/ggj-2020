using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//going to be used to set the players stats, and modify them depending on level
public class StatManager : MonoBehaviour
{
    public int[] playerStats = new int[7];

    void Start()
    {
        playerStats[0] = 20;
        playerStats[1] = 20;
        playerStats[2] = 10;
        playerStats[3] = 10;
        playerStats[4] = 5;
        playerStats[5] = 5;
        playerStats[6] = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerStats()
    {
       
    }
}
