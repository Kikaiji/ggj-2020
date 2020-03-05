using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    //calls the build garden function from the townmanager
    TownManager tManager;

    // Start is called before the first frame update
    void Start()
    {
        tManager = GameObject.Find("TownManager").GetComponent<TownManager>();
    }
    public void OnButtonclick()
    {
        tManager.BuildGarden();
    }
}
