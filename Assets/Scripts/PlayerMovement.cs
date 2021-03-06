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
    public GameObject sweatParticle;
    public GameObject dirtParticle;
    public ParticleSystem smokePuff;

    [Header("Health/Stamina")]
    public float passiveStaminaDrainRate = 1.8f;
    public float staminaDrainRate = 6.4f;
    public Image staminaMeter;
    public Gradient staminaGradient;


    
    Rigidbody2D rb;
    float xInput, yInput;

    //health values
    float stamina = 100.0f;
    int health = 5;

    bool isDead = false; //used mostly for effects
    bool inputEnabled = true;


    //stuff to be accessed by other scripts.
    [HideInInspector] public Vector2 moveDirection = Vector2.zero;
    [HideInInspector] public bool isAttached; //is the player attached to the wall?

    ValidClimbSpotFinder validClimbSpotFinder;

    PlayerHealth thisPlayerHealth;
    DistanceIndicator distanceIndicator;
    private void Start()
    {
        validClimbSpotFinder = GetComponent<ValidClimbSpotFinder>();
        distanceIndicator = GetComponent<DistanceIndicator>();
        rb = GetComponent<Rigidbody2D>();
        isAttached = true;
        rb.gravityScale = 0.0f;
        transform.eulerAngles = Vector3.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        thisPlayerHealth = GetComponent<PlayerHealth>();
    }


    private void Update()
    {
        if (inputEnabled)
        {
            GetThisPlayerInput();
        }

        if (isAttached)
        {
            if (!otherPlayer.isAttached && Vector2.Distance(transform.position, otherPlayer.transform.position) >= 3.45f)
            {
                //drain our stamina at the exertive level
                stamina -= staminaDrainRate * Time.deltaTime;
            }
            else 
            {
                stamina -= passiveStaminaDrainRate * Time.deltaTime;
            }

            if (stamina <= 15.0f /*&& !sweatParticle.activeSelf*/)
            {
                var emission = sweatParticle.GetComponent<ParticleSystem>().emission;
                emission.rateOverTime = 9f;
            }
            else if (stamina < 35.0f /*&& sweatParticle.activeSelf*/)
            {
                var emission = sweatParticle.GetComponent<ParticleSystem>().emission;
                emission.rateOverTime = 6f;
            }
            else if (stamina < 50.0f /*&& sweatParticle.activeSelf*/)
            {
                var emission = sweatParticle.GetComponent<ParticleSystem>().emission;
                emission.rateOverTime = 3f;
            }
            else if (stamina < 65.0f /*&& sweatParticle.activeSelf*/)
            {
                var emission = sweatParticle.GetComponent<ParticleSystem>().emission;
                emission.rateOverTime = 1f;
            }

            else if (stamina >= 65.0f /*&& sweatParticle.activeSelf*/)
            {
                var emission = sweatParticle.GetComponent<ParticleSystem>().emission;
                emission.rateOverTime = 0f;
            }

            if (stamina <= 0.0f)
            {
                Detach();
            }
        }
        else if (stamina < 100.0f)
        {
            stamina += 1.2f * staminaDrainRate * Time.deltaTime;
        }
        if (distanceIndicator.dangerLevel > 0.69f) 
        {
            stamina -= staminaDrainRate * Time.deltaTime * distanceIndicator.dangerLevel * 0.8f;
        }
        staminaMeter.fillAmount = stamina / 100.0f;
        staminaMeter.color = staminaGradient.Evaluate((1f -(stamina / 100f)));

    }

    private void FixedUpdate()
    {
        if (isAttached)
        {

            if (!validClimbSpotFinder.CheckPlayerPosition())
            {
                Detach();
            }
            else
            {
                
                rb.velocity = speed * moveDirection;


                //Uncomment this out to prevent players from flinging themselves off cliffs

                //if (!validClimbSpotFinder.CheckPlayerPosition(rb.velocity * Time.fixedDeltaTime))
                //{
                //    rb.velocity = rb.velocity * Vector2.up;
                //}
            }

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
        moveDirection = Vector2.right * xInput + Vector2.up * yInput;
    }

    private void Detach()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        isAttached = false;
        rb.gravityScale = 1.0f;
        dirtParticle.SetActive(false);
        AudioManager.Instance.PlaySound("Detach");
    }

    private void Reattach()
    {
        if (validClimbSpotFinder.CheckPlayerPosition())
        {
            transform.eulerAngles = Vector3.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            isAttached = true;
            rb.gravityScale = 0.0f;
            dirtParticle.SetActive(true);
            smokePuff.Stop();
            smokePuff.Play();
            AudioManager.Instance.PlaySound("Attach");
        }
    }

    public void TempDisableInput(float time, bool disableOther)
    {
        StartCoroutine(DisableInput(time));
        if (disableOther)
        {
            otherPlayer.TempDisableInput(time, false);
        }
    }

    public void ForceDrop(bool disableOther)
    {
        Detach();
        if (disableOther)
        {
            otherPlayer.ForceDrop(false);
        }
    }

    IEnumerator DisableInput(float time)
    {
        inputEnabled = false;
        yield return new WaitForSeconds(time);
        inputEnabled = true;
        //thisPlayerHealth.TempInvincibility(2.5f);
    }
}
