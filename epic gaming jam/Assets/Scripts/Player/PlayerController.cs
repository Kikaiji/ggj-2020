using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int xpos;
    public int ypos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)){Move(0);}
        if (Input.GetKeyDown(KeyCode.RightArrow)) { Move(1); }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { Move(2); }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { Move(3); }
    }

    void Move( int direction )
    {
        switch(direction){
            case 0:
                Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), 1f);
                break;
            case 1:
                transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
                break;
            case 2:
                transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
                break;
            case 3:
                transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
                break;
        }

    }
}
