using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject[] hearts;
    public PlayerAnimation thisAnimator;
    public SpecialAnimations specialAnimations;
    PlayerMovement thisMovement;
    int healthRemaining = 3;

    bool isInvincible = false;

    private void Awake()
    {
        thisMovement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boulder"))
        {
            if (!isInvincible)
            {
                Debug.Log("Smacked");
                thisAnimator.TakeDamageTrigger();
                hearts[--healthRemaining].SetActive(false);
                if (healthRemaining == 0)
                {
                    thisMovement.TempDisableInput(100f, true);
                    thisMovement.ForceDrop(true);
                    Debug.Log("you died");
                    GameManager.Instance.PlayerDeath();
                }
                else
                {
                    TempInvincibility(3.25f);
                    thisMovement.TempDisableInput(0.75f, false);
                }

                //boulders can get stuck on our heads, lets fix that
                if (collision.gameObject.transform.position.x >= transform.position.x)
                {
                    //if the boulder is to the right of the player
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(5.0f *
                        (Vector2.right + Vector2.up), ForceMode2D.Impulse);
                }
                else
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(5.0f *
                        (Vector2.left + Vector2.up), ForceMode2D.Impulse);
                }
            }
            
            
        }
    }

    public void TempInvincibility(float time)
    {
        StartCoroutine(Invincibility(time));
        specialAnimations.CallDamageFlash(time, 2.2f, 4f);
    }

    IEnumerator Invincibility(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }


}
