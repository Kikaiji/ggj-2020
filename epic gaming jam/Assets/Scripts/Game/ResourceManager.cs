using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    GameObject resourcebox;
    public float[] resources = new float[5];
    // Start is called before the first frame update
    void Start()
    {
        resourcebox = GameObject.Find("ResourceBox");
        DontDestroyOnLoad(gameObject);
        for(int i = 0; i < 3; i++)
        {
            //resources[i] = Random.Range(1, 4);
            resources[i] = 10f;
        }
        for(int i = 0; i < resources.Length; i++)
        {
            resourcebox.transform.GetChild(i).GetComponent<Text>().text = resources[i].ToString();
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
