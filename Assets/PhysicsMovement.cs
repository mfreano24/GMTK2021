using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{

    public float speed;
    Vector2 input;
    public bool player2;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void Update()
    {
        input = Vector2.zero;
        if (!player2)
        {
            input += Vector2.right * speed * Input.GetAxis("Horizontal");
            //input += Vector2.up * speed * Input.GetAxis("Vertical");
        }
        else 
        {
            input += Vector2.right * speed * Input.GetAxis("HorizontalP2");
            //input += Vector2.up * speed * Input.GetAxis("VerticalP2");
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        //rb.MovePosition(((Vector2)transform.position) + input);
        rb.velocity = new Vector2(input.x + rb.velocity.x, rb.velocity.y);
        Debug.Log(rb.velocity.y);
    }
}
