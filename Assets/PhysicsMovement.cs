using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{

    public float speed;
    Vector2 input;
    public bool player2;
    // Start is called before the first frame update
    void Start()
    {

    }


    public void Update()
    {
        input = Vector2.zero;
        if (!player2)
        {
            input += Vector2.right * speed * Input.GetAxis("Horizontal");
            input += Vector2.up * speed * Input.GetAxis("Vertical");
        }
        else 
        {
            input += Vector2.right * speed * Input.GetAxis("HorizontalP2");
            input += Vector2.up * speed * Input.GetAxis("VerticalP2");
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        gameObject.GetComponent<Rigidbody2D>().MovePosition(((Vector2)transform.position) + input);
    }
}
