using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerMovement playerMovement;

    

    Animator anim;

    bool isAttached = true;

    bool isMoving = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isAttached && !playerMovement.isAttached)
        {
            //send trigger to switch to falling state
            isAttached = false;
            anim.SetTrigger("Detach");
        }

        else if(!isAttached && playerMovement.isAttached)
        {
            //send trigger to switch to climbing state
            isAttached = true;
            anim.SetTrigger("Reattach");
        }

        if(isMoving && playerMovement.moveDirection == Vector2.zero)
        {
            isMoving = false;
            anim.SetBool("Moving", false);
        }

        else if(!isMoving && playerMovement.moveDirection != Vector2.zero)
        {
            isMoving = true;
            anim.SetBool("Moving", true);
        }
    }

    public void TakeDamageTrigger()
    {
        anim.SetTrigger("TakeDamage");
    }
}
