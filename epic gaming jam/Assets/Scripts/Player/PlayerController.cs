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
        Vector3 up = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        Vector3 right = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
        Vector3 down = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        Vector3 left = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
        switch (direction){
            case 0:
                if (Physics2D.Linecast(transform.position, up, 1 << LayerMask.NameToLayer("Wall")))
                {
                    break;
                    //transform.position = Vector3.MoveTowards(transform.position, up, 1f);
                }
                else
                {
                    transform.position = up;
                }
                break;
            case 1:
                if (Physics2D.Linecast(transform.position, right, 1 << LayerMask.NameToLayer("Wall")))
                {
                    break;
                }
                else
                {
                    transform.position = right;
                }
                break;
            case 2:
                if (Physics2D.Linecast(transform.position, down, 1 << LayerMask.NameToLayer("Wall")))
                {
                    break;
                }
                else
                {
                    transform.position = down;
                }
                break;
            case 3:
                if (Physics2D.Linecast(transform.position, left, 1 << LayerMask.NameToLayer("Wall")))
                {
                    break;
                }
                else
                {
                    transform.position = left;
                }
                break;
        }

    }
}
