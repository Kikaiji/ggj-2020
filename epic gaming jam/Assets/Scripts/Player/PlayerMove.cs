using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//lets the player move about the town map
public class PlayerMove : MonoBehaviour
{
    Vector3 pos;
    public int speed;
    Rigidbody2D rb2d;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pos.x > transform.position.x) { animator.Play("NomadMapWalkRight"); }
        if (pos.x < transform.position.x) { animator.Play("NomadMapWalkAnim"); }
        if (pos.x == transform.position.x) { animator.Play("NomadMapIdle"); }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousepos = Input.mousePosition;
            mousepos = Camera.main.ScreenToWorldPoint(mousepos);
            pos = new Vector3(mousepos.x, mousepos.y, transform.position.z);
            
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
    }
}
