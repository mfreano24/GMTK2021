using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //die lol
            Debug.Log("You fell (and died)!");
            //TODO: Call to GameManager here to handle deth
            //GameManager.Instance.PlayerDeath();
        }
    }
}
