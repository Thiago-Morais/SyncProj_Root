using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovment : MonoBehaviour
{
    public float speed;
    public bool canJump;
    public float jumpForce;
    public Vector3 direction;
    public float distance;
    public GameObject feet;
    Rigidbody2D rb2D;
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //                  ===== MOVMENT 
        transform.Translate(Input.GetAxis("Horizontal") * speed, 0, 0);

        //                  ===== JUMP 
        if(canJump && Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(transform.up * jumpForce);
        }

        direction = feet.transform.position;
        distance = Vector2.Distance(feet.transform.position, transform.position);
        hit = Physics2D.Raycast(transform.position, feet.transform.localPosition, distance);
        Debug.DrawRay(transform.position, feet.transform.localPosition, Color.blue);
        if(hit == true)
        {
            canJump = true;
            if(hit.transform.name.Contains("Platform"))
            {
                transform.parent = hit.transform;
            }
            else
            {
                transform.parent = null;
            }
        }
        else
            canJump = false;
    }
}
