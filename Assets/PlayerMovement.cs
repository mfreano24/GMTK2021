using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("General Values")]
    public float speed = 1.75f;
    public bool isPlayer2;
    public PlayerMovement otherPlayer; //keep a reference to the other player

    [Header("Health/Stamina")]
    public float staminaDrainRate = 20.0f;

    Vector2 moveDirection;
    Rigidbody2D rb;
    float xInput, yInput;

    //health values
    float stamina = 100.0f;
    int health = 5;

    //stuff to be accessed by other scripts.
    [HideInInspector] public bool isAttached; //is the player attached to the wall?



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isAttached = true;
        rb.gravityScale = 0.0f;

    }


    private void Update()
    {
        GetThisPlayerInput();
        /*
        if (!otherPlayer.isAttached)
        {
            //drain our stamina
            stamina -= 2.0f * staminaDrainRate;
        }
        else if(stamina < 100.0f)
        {
            stamina += 2.0f * staminaDrainRate * Time.deltaTime;
        }
        */
    }

    private void FixedUpdate()
    {
        if (isAttached)
        {
            //Debug.Log("whuh");
            moveDirection = Vector2.right * xInput + Vector2.up * yInput;

            rb.velocity = speed * moveDirection;
        }
    }

    void GetThisPlayerInput()
    {
        if (!isPlayer2)
        {
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isAttached)
                {
                    isAttached = false;
                    rb.gravityScale = 1.0f;
                }
                else
                {
                    isAttached = true;
                    rb.gravityScale = 0.0f;
                }
            }
        }
        else
        {
            xInput = Input.GetAxis("HorizontalP2");
            yInput = Input.GetAxis("VerticalP2");

            if (Input.GetKeyDown(KeyCode.O))
            {
                if (isAttached)
                {
                    isAttached = false;
                    rb.gravityScale = 1.0f;
                }
                else
                {
                    isAttached = true;
                    rb.gravityScale = 0.0f;
                }
            }
        }
    }
}
