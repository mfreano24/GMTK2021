using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //TODO: Call to GameManager here to handle death
            GameManager.Instance.PlayerDeath();
        }
        else if (collision.CompareTag("Boulder"))
        {
            Debug.Log("Got the boulder!");
            BoulderManager.Instance.ReturnBoulder(collision.gameObject);
        }
    }



    
}
