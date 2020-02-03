using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Vector3 pos;
    public int speed;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
            if(Input.mousePosition.x > transform.position.x) { animator.Play("NomadMapWalkRight"); }
            else if (Input.mousePosition.x < transform.position.x) { animator.Play("NomadMapWalk"); }
            else { animator.Play("NomadMapIdle"); }
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }
}
