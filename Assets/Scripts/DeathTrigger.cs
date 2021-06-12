using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement pm = collision.GetComponent<PlayerMovement>();
            //TODO: only trigger death if the player that just touched the trigger is above the other player.
            if (collision.transform.position.y >
                pm.otherPlayer.transform.position.y)
            {
                pm.TempDisableInput(100f, true);

                GameManager.Instance.PlayerDeath();
            }
            
        }
        else if (collision.CompareTag("Boulder"))
        {
            Debug.Log("Got the boulder!");
            BoulderManager.Instance.ReturnBoulder(collision.gameObject);
        }
    }



    
}
