using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("General Values")]
    public float speed = 1.75f;
    public bool isPlayer2;
    public PlayerMovement otherPlayer; //keep a reference to the other player

    [Header("Health/Stamina")]
    public float staminaDrainRate = 20.0f;
    public Text staminaValueText;


    Vector2 moveDirection;
    Rigidbody2D rb;
    float xInput, yInput;

    //health values
    float stamina = 100.0f;
    int health = 5;

    bool isDead = false; //used mostly for effects


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
        if (isAttached)
        {
            if (!otherPlayer.isAttached && Vector2.Distance(transform.position, otherPlayer.transform.position) >= 3.45f)
            {
                //drain our stamina
                stamina -= staminaDrainRate * Time.deltaTime;
            }
            else if (stamina < 100.0f)
            {
                stamina += 2.0f * staminaDrainRate * Time.deltaTime;
            }

            if (stamina <= 0.0f)
            {

                Detach();
            }
        }
        

        staminaValueText.text = Mathf.FloorToInt(stamina).ToString();
        
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

            if (Input.GetKeyDown(KeyCode.O))
            {
                if (isAttached)
                {
                    Detach();
                }
                else
                {
                    Reattach();
                }
            }
        }
        else
        {
            //alternatively, what if we moved this one with the mouse?
            //this would prevent getting confused, which i sure am.
            xInput = Input.GetAxis("HorizontalP2");
            yInput = Input.GetAxis("VerticalP2");

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isAttached)
                {
                    Detach();
                }
                else
                {
                    Reattach();
                }
            }
        }
    }

    private void Detach()
    {
        isAttached = false;
        rb.gravityScale = 1.0f;
    }

    private void Reattach()
    {
        isAttached = true;
        rb.gravityScale = 0.0f;
    }
}
