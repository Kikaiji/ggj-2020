using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//moves the resource menu in and out
public class MoveMenu : MonoBehaviour
{
    GameObject parent;
    bool open = false;
    Vector3 pos;
    float speed = 160f;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        pos = parent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        parent.transform.position = Vector3.MoveTowards(parent.transform.position, pos, Time.deltaTime * speed);
    }

    public void OnButtonClick()
    {
        if (!open)
        {
            pos += new Vector3(0, 220);
            open = true;
        }
        else
        {
            pos += new Vector3(0, -220);
            open = false;
        }
    }
}
