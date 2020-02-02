using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
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
