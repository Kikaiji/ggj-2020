using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceManager : MonoBehaviour
{
    public float[] resources = new float[3];
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        for(int i = 0; i < resources.Length; i++)
        {
            resources[i] = Random.Range(1, 4);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckForResource(int id, int amount)
    {
        if (resources[id] >= amount) { return true; }
        return false;
    }
    public bool RemoveResource(int id, int amount)
    {
        if(resources[id] >= amount) { resources[id] -= amount; return true; }
        return false;
    }

    bool CreateBuilding(int id)
    {
        return false;
    }
}
