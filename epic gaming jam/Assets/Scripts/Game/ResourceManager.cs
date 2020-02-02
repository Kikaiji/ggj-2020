using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourceManager : MonoBehaviour
{

    private static ResourceManager playerInstance;
    [SerializeField]
    GameObject resourcebox;
    float sum;
    TownManager tmanager;
    public float[] resources = new float[5];
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }
    void Start()
    {
        tmanager = GameObject.Find("TownManager").GetComponent<TownManager>();
        resourcebox = GameObject.Find("ResourceBox");
        DontDestroyOnLoad(gameObject);
        for(int i = 0; i < 3; i++)
        {
            //resources[i] = Random.Range(1, 4);
            resources[i] = 12f;
        }
        for(int i = 0; i < resources.Length; i++)
        {
            resourcebox.transform.GetChild(i).GetComponent<Text>().text = resources[i].ToString();
        }
        //for(int i = 0; i < )
        resourcebox.transform.GetChild(resources.Length).GetComponent<Text>().text = "5"; 
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
